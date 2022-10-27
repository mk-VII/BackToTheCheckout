using Kata09.model.interfaces;

namespace Kata09.model.discounts;

public class PercentageDiscount : IDiscount
{
    private readonly int _skip;
    private readonly double _percentage;

    public PercentageDiscount(int skip, double percentage)
    {
        _skip = skip;
        _percentage = percentage;
    }

    public double CalculateDiscount(IEnumerable<Item> items)
    {
        var itemsArray = items.ToArray();

        return itemsArray.Length > _skip 
            ? itemsArray
                .Skip(_skip)
                .Sum(CalculateIndividualItemDiscount) 
            : 0;
    }

    private double CalculateIndividualItemDiscount(Item item) =>
        item.Price * _percentage;
}