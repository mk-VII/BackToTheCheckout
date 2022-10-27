using Kata09.model.discounts.@base;

namespace Kata09.model.discounts;

public class BulkDiscount : GroupDiscount
{
    private readonly double _updatedGroupPrice;

    public BulkDiscount(int groupSize, double updatedGroupPrice)
        : base(groupSize)
    {
        _updatedGroupPrice = updatedGroupPrice;
    }

    public override double CalculateDiscount(IEnumerable<Item> items) =>
        GroupItemsForDiscount(items)
            .Sum(group =>
                group.Sum(item => item.Price) - _updatedGroupPrice
            );
}