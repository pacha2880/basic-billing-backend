using AutoMapper;
using BasicBilling.Application.Bills.Commands;
using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Enums;
using BasicBilling.Domain.Interfaces;
using Moq;

namespace BasicBilling.Tests.Unit;

public class CreateBillHandlerTests
{
    private readonly Mock<IClientRepository> _mockClientRepo = new();
    private readonly Mock<IBillRepository> _mockBillRepo = new();
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly CreateBillHandler _handler;

    public CreateBillHandlerTests()
    {
        _handler = new CreateBillHandler(
            _mockClientRepo.Object,
            _mockBillRepo.Object,
            _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsBillDto()
    {
        // Arrange
        _mockClientRepo.Setup(r => r.ExistsAsync(100)).ReturnsAsync(true);
        _mockBillRepo.Setup(r => r.AddAsync(It.IsAny<Bill>())).Returns(Task.CompletedTask);
        _mockBillRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
        _mockMapper.Setup(m => m.Map<BillDto>(It.IsAny<Bill>()))
            .Returns(new BillDto
            {
                Id = 31,
                ClientId = 100,
                ServiceType = "Water",
                BillingPeriod = "202503",
                Amount = 75.50m,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            });

        var command = new CreateBillCommand(100, ServiceType.Water, "202503", 75.50m);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Pending", result.Status);
        Assert.Equal(100, result.ClientId);
    }

    [Fact]
    public async Task Handle_ClientNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockClientRepo.Setup(r => r.ExistsAsync(999)).ReturnsAsync(false);

        var command = new CreateBillCommand(999, ServiceType.Water, "202503", 75.50m);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Client not found.", ex.Message);
    }
}
