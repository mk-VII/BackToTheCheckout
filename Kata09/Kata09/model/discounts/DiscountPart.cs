using Kata09.@enum;

namespace Kata09.model.discounts;

public class DiscountPart
{
    public ItemId ItemId { get; }
    public int RequiredAmount { get; }
    
    public DiscountPart(ItemId itemId, int requiredAmount)
    {
        ItemId = itemId;
        RequiredAmount = requiredAmount;
    }

}