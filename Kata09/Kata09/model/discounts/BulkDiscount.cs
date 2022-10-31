using Kata09.model.discounts.@base;

namespace Kata09.model.discounts;

public class BulkDiscount : GroupDiscount
{
    public BulkDiscount(int groupSize, double updatedGroupPrice)
        : base(groupSize, updatedGroupPrice)
    {
    }

    public override double CalculateDiscount(IEnumerable<Item> items) =>
        GroupItemsForDiscount(items)
            .Sum(group =>
                group.Sum(item => item.Price) - UpdatedPrice
            );
}