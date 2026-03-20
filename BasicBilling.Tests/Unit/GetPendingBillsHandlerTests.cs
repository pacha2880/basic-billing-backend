using AutoMapper;
using BasicBilling.Application.Clients.Queries;
using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Enums;
using BasicBilling.Domain.Interfaces;
using Moq;

namespace BasicBilling.Tests.Unit;

public class GetPendingBillsHandlerTests
{
    private readonly Mock<IClientRepository> _mockClientRepo = new();
    private readonly Mock<IBillRepository> _mockBillRepo = new();
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly GetPendingBillsHandler _handler;

    public GetPendingBillsHandlerTests()
    {
        _handler = new GetPendingBillsHandler(
            _mockClientRepo.Object,
            _mockBillRepo.Object,
            _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidClient_ReturnsBillDtoList()
    {
        // Arrange
        var bills = new List<Bill>
        {
            new Bill { Id = 1, ClientId = 100, ServiceType = ServiceType.Water, BillingPeriod = "202501", Amount = 72m, Status = BillStatus.Pending },
            new Bill { Id = 2, ClientId = 100, ServiceType = ServiceType.Electricity, BillingPeriod = "202501", Amount = 137m, Status = BillStatus.Pending }
        };

        var billDtos = new List<BillDto>
        {
            new BillDto { Id = 1, ClientId = 100, ServiceType = "Water", BillingPeriod = "202501", Amount = 72m, Status = "Pending", CreatedAt = DateTime.UtcNow },
            new BillDto { Id = 2, ClientId = 100, ServiceType = "Electricity", BillingPeriod = "202501", Amount = 137m, Status = "Pending", CreatedAt = DateTime.UtcNow }
        };

        _mockClientRepo.Setup(r => r.ExistsAsync(100)).ReturnsAsync(true);
        _mockBillRepo.Setup(r => r.GetPendingBillsByClientAsync(100)).ReturnsAsync(bills.AsReadOnly());
        _mockMapper.Setup(m => m.Map<IEnumerable<BillDto>>(It.IsAny<IReadOnlyCollection<Bill>>()))
            .Returns(billDtos);

        var query = new GetPendingBillsQuery(100);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task Handle_ClientNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockClientRepo.Setup(r => r.ExistsAsync(999)).ReturnsAsync(false);

        var query = new GetPendingBillsQuery(999);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _handler.Handle(query, CancellationToken.None));
        Assert.Equal("Client not found.", ex.Message);
    }
}
