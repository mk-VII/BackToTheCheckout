using Kata09.model.interfaces;

namespace Kata09.model.discounts.@base;

public abstract class GroupDiscount : IDiscount
{
    protected readonly int _groupSize;

    protected GroupDiscount(int groupSize)
    {
        _groupSize = groupSize;
    }

    public abstract double CalculateDiscount(IEnumerable<Item> items);
    
    protected IEnumerable<IEnumerable<Item>> GroupItemsForDiscount(IEnumerable<Item> items, bool rangeInclusive = false)
    {
        var groups = new List<IEnumerable<Item>>();

        var array = items.ToArray();

        for (var i = 0; IsInRange(i, array, rangeInclusive); i++)
        {
            groups.Add(array
                .Skip(i * _groupSize)
                .Take(_groupSize)
            );
        }

        return groups;
    }

    private bool IsInRange(int index, IReadOnlyCollection<Item> array, bool rangeInclusive) =>
        rangeInclusive
            ? index <= array.Count / _groupSize
            : index < array.Count / _groupSize;
}