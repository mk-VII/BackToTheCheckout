using Kata09.model.interfaces;

namespace Kata09.model.rules.@base;

public abstract class Rule
{
    private IDiscount Discount { get; }

    protected Rule(IDiscount discount)
    {
        Discount = discount;
    }

    public double ApplyRule(IEnumerable<Item> items) => 
        Discount.CalculateDiscount(ItemsWithId(items));

    protected abstract IEnumerable<Item> ItemsWithId(IEnumerable<Item> items);
}