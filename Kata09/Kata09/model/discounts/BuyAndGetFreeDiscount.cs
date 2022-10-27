using Kata09.model.discounts.@base;

namespace Kata09.model.discounts;

public class BuyAndGetFreeDiscount : GroupDiscount
{
    private readonly int _buy;

    public BuyAndGetFreeDiscount(int buy, int free)
        : base(buy + free)
    {
        _buy = buy;
    }

    public override double CalculateDiscount(IEnumerable<Item> items)
    {
        return GroupItemsForDiscount(items, true)
            .Sum(group => group
                .Skip(_buy)
                .Sum(item => item.Price)
            );
    }
}