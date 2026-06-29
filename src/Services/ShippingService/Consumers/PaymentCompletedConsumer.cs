using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using ShippingService.Contracts;
using ShippingService.DTOs;
using ShippingService.Services;

namespace ShippingService.Consumers
{
    public class PaymentCompletedConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentCompletedConsumer> _logger;

        private IConnection? _connection;
        private IChannel? _channel;

        public PaymentCompletedConsumer(
            IServiceScopeFactory scopeFactory,
            IConfiguration configuration,
            ILogger<PaymentCompletedConsumer> logger)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"],
                Port = int.Parse(_configuration["RabbitMQ:Port"]!),
                UserName = _configuration["RabbitMQ:Username"],
                Password = _configuration["RabbitMQ:Password"]
            };

            _connection = await factory.CreateConnectionAsync(stoppingToken);

            _channel = await _connection.CreateChannelAsync(
                cancellationToken: stoppingToken);

            var queueName = _configuration["RabbitMQ:QueueName"]!;

            await _channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    _logger.LogInformation(
                        "PaymentCompletedEvent received: {Message}",
                        message);

                    var paymentEvent =
                        JsonSerializer.Deserialize<PaymentCompletedEvent>(message);

                    if (paymentEvent == null)
                    {
                        await _channel.BasicNackAsync(
                            eventArgs.DeliveryTag,
                            false,
                            false);

                        return;
                    }

                    using var scope = _scopeFactory.CreateScope();

                    var shippingService =
                        scope.ServiceProvider.GetRequiredService<IShippingService>();

                    var request = new CreateShippingRequest
                    {
                        OrderId = paymentEvent.OrderId,
                        TransactionId = paymentEvent.TransactionId,
                        CustomerEmail = paymentEvent.CustomerEmail,
                        ShippingAddress = paymentEvent.ShippingAddress
                    };

                    await shippingService.CreateShippingAsync(request);

                    await _channel.BasicAckAsync(eventArgs.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error while processing PaymentCompletedEvent");

                    await _channel!.BasicNackAsync(
                        eventArgs.DeliveryTag,
                        false,
                        true);
                }
            };

            await _channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken);

            _logger.LogInformation(
                "ShippingService is listening to RabbitMQ queue: {QueueName}",
                queueName);
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
