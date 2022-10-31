using Kata09.@enum;
using Kata09.model.interfaces;
using Kata09.model.rules.@base;

namespace Kata09.model.rules;

public class SingleItemRule : Rule
{
    private ItemId ItemId { get; }
    
    public SingleItemRule(IDiscount discount, ItemId itemId) 
        : base(discount)
    {
        ItemId = itemId;
    }

    protected override IEnumerable<Item> ItemsWithId(IEnumerable<Item> items) =>
        items.Where(item => item.Id == ItemId);
}