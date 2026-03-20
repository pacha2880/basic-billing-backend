using AutoMapper;
using BasicBilling.Application.DTOs;
using BasicBilling.Application.Payments.Commands;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Enums;
using BasicBilling.Domain.Interfaces;
using Moq;

namespace BasicBilling.Tests.Unit;

public class ProcessPaymentHandlerTests
{
    private readonly Mock<IClientRepository> _mockClientRepo = new();
    private readonly Mock<IBillRepository> _mockBillRepo = new();
    private readonly Mock<IPaymentRepository> _mockPaymentRepo = new();
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly ProcessPaymentHandler _handler;

    public ProcessPaymentHandlerTests()
    {
        _handler = new ProcessPaymentHandler(
            _mockClientRepo.Object,
            _mockBillRepo.Object,
            _mockPaymentRepo.Object,
            _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidPayment_ReturnsPaymentHistoryDto()
    {
        // Arrange
        var bill = new Bill
        {
            Id = 1,
            ClientId = 100,
            ServiceType = ServiceType.Water,
            BillingPeriod = "202501",
            Amount = 72m,
            Status = BillStatus.Pending
        };

        _mockClientRepo.Setup(r => r.ExistsAsync(100)).ReturnsAsync(true);
        _mockBillRepo.Setup(r => r.GetPendingBillAsync(100, ServiceType.Water, "202501")).ReturnsAsync(bill);
        _mockBillRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
        _mockPaymentRepo.Setup(r => r.AddAsync(It.IsAny<Payment>())).Returns(Task.CompletedTask);
        _mockPaymentRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
        _mockMapper.Setup(m => m.Map<PaymentHistoryDto>(It.IsAny<Payment>()))
            .Returns(new PaymentHistoryDto
            {
                BillId = 1,
                ServiceType = "Water",
                BillingPeriod = "202501",
                AmountPaid = 72m,
                PaidAt = DateTime.UtcNow,
                Status = "Paid"
            });

        var command = new ProcessPaymentCommand(100, ServiceType.Water, "202501");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Paid", result.Status);
    }

    [Fact]
    public async Task Handle_ClientNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockClientRepo.Setup(r => r.ExistsAsync(999)).ReturnsAsync(false);

        var command = new ProcessPaymentCommand(999, ServiceType.Water, "202501");

        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Client not found.", ex.Message);
    }

    [Fact]
    public async Task Handle_BillNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockClientRepo.Setup(r => r.ExistsAsync(100)).ReturnsAsync(true);
        _mockBillRepo.Setup(r => r.GetPendingBillAsync(100, ServiceType.Water, "202503")).ReturnsAsync((Bill?)null);
        _mockBillRepo.Setup(r => r.GetBillAsync(100, ServiceType.Water, "202503")).ReturnsAsync((Bill?)null);

        var command = new ProcessPaymentCommand(100, ServiceType.Water, "202503");

        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Bill not found.", ex.Message);
    }

    [Fact]
    public async Task Handle_BillAlreadyPaid_ThrowsInvalidOperationException()
    {
        // Arrange
        var paidBill = new Bill
        {
            Id = 1,
            ClientId = 100,
            ServiceType = ServiceType.Water,
            BillingPeriod = "202501",
            Amount = 72m,
            Status = BillStatus.Paid
        };

        _mockClientRepo.Setup(r => r.ExistsAsync(100)).ReturnsAsync(true);
        _mockBillRepo.Setup(r => r.GetPendingBillAsync(100, ServiceType.Water, "202501")).ReturnsAsync((Bill?)null);
        _mockBillRepo.Setup(r => r.GetBillAsync(100, ServiceType.Water, "202501")).ReturnsAsync(paidBill);

        var command = new ProcessPaymentCommand(100, ServiceType.Water, "202501");

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Bill already paid.", ex.Message);
    }
}
