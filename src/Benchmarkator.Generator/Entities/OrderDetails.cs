using System;
using System.Collections.Generic;

namespace Benchmarkator.Generator.Entities;

public class OrderDetails
{
    public Order Order { get; init; } = null!;
    public UserDetails Customer { get; init; } = null!;
    public string Note { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IReadOnlyCollection<OrderItem> Items { get; init; } = null!;

    public class OrderItem
    {
        public Guid Id { get; init; }
        public string ItemName { get; init; } = null!;
        public string Description { get; init; } = null!;
    }
}
