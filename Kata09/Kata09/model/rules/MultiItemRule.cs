using Kata09.@enum;
using Kata09.model.discounts;
using Kata09.model.rules.@base;

namespace Kata09.model.rules;

public class MultiItemRule : Rule
{
    private readonly IEnumerable<ItemId> _itemIds;

    public MultiItemRule(CompositeDiscount discount) 
        : base(discount)
    {
        _itemIds = discount.DiscountDictionary.Keys;
    }

    protected override IEnumerable<Item> ItemsWithId(IEnumerable<Item> items) =>
        items.Where(item => _itemIds.Contains(item.Id));
}