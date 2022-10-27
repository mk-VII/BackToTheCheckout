using Kata09.@enum;
using Kata09.model.interfaces;

namespace Kata09.model;

public class Rule
{
    private ItemId ItemId { get; }

    private IDiscount Discount { get; }

    public Rule(ItemId itemId, IDiscount discount)
    {
        ItemId = itemId;
        Discount = discount;
    }

    public double ApplyRule(IEnumerable<Item> items) => 
        Discount.CalculateDiscount(ItemsWithId(items));

    private IEnumerable<Item> ItemsWithId(IEnumerable<Item> items) =>
        items.Where(item => item.Id == ItemId);
}