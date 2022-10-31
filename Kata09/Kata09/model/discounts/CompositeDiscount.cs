using Kata09.@enum;
using Kata09.model.discounts.@base;

namespace Kata09.model.discounts;

public class CompositeDiscount : GroupDiscount
{
    public readonly Dictionary<ItemId, int> DiscountDictionary;

    public CompositeDiscount(IEnumerable<DiscountPart> discountParts, double updatedCompositePrice)
        : base(discountParts.Sum(x => x.RequiredAmount), updatedCompositePrice)
    {
        DiscountDictionary = discountParts.ToDictionary(
            x => x.ItemId,
            x => x.RequiredAmount
        );
    }

    public override double CalculateDiscount(IEnumerable<Item> items)
    {
        var discountGroups = GroupItemsForDiscount(items).ToArray();

        return discountGroups.Any()
            ? discountGroups.Sum(x => x.Sum(y => y.Price)) - UpdatedPrice
            : 0;
    }

    protected override IEnumerable<IEnumerable<Item>> GroupItemsForDiscount(IEnumerable<Item> items,
        bool rangeInclusive = false)
    {
        var itemsArray = items.ToArray();

        if (ContainsValidItemsForGroupDiscount(itemsArray))
        {
            return Array.Empty<Item[]>();
        }

        var distinctItemIds = itemsArray
            .Select(x => x.Id)
            .Distinct()
            .ToArray();

        var maximum = int.MaxValue;

        foreach (var itemId in distinctItemIds)
        {
            if (DiscountDictionary.ContainsKey(itemId))
            {
                int newMax;
                if (maximum > (newMax =
                        itemsArray.Count(x => x.Id == itemId) / DiscountDictionary[itemId]))
                {
                    maximum = newMax;
                }
            }
        }

        return distinctItemIds
            .Select(value => Enumerable.Repeat(
                    Item.Lookup(value),
                    DiscountDictionary[value] * maximum)
                .ToArray()
            );
    }

    private bool ContainsValidItemsForGroupDiscount(IReadOnlyCollection<Item> itemsArray) =>
        itemsArray.Count < GroupSize ||
        itemsArray.GroupBy(x => x.Id).Any(x => x.Count() < DiscountDictionary[x.Key]);
}