using Kata09.model.interfaces;

namespace Kata09.model.discounts.@base;

public abstract class GroupDiscount : IDiscount
{
    protected readonly int GroupSize;
    protected readonly double UpdatedPrice;

    protected GroupDiscount(int groupSize, double updatedPrice = 0.0d)
    {
        GroupSize = groupSize;
        UpdatedPrice = updatedPrice;
    }

    public abstract double CalculateDiscount(IEnumerable<Item> items);

    protected virtual IEnumerable<IEnumerable<Item>> GroupItemsForDiscount(IEnumerable<Item> items,
        bool rangeInclusive = false)
    {
        var itemsArray = items.ToArray();

        var chunks = new List<IEnumerable<Item>>();

        for (var i = 0; IsInRange(itemsArray, i, rangeInclusive); i++)
        {
            chunks.Add(itemsArray
                .Skip(i * GroupSize)
                .Take(GroupSize)
            );
        }

        return chunks;
    }

    private bool IsInRange(IReadOnlyCollection<Item> array, int index, bool rangeInclusive) =>
        rangeInclusive
            ? index <= array.Count / GroupSize
            : index < array.Count / GroupSize;
}