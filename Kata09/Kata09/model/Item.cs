using Kata09.constants;
using Kata09.@enum;

namespace Kata09.model;

public class Item
{
    public ItemId Id { get; }
    public double Price { get; }

    public static Item Lookup(ItemId id) => new(id, GetItemPrice(id));

    private Item(ItemId id, double price)
    {
        Id = id;
        Price = price;
    }

    public static double GetItemPrice(ItemId id) => id switch
    {
        ItemId.A => ItemPrice.A,
        ItemId.B => ItemPrice.B,
        ItemId.C => ItemPrice.C,
        ItemId.D => ItemPrice.D,
        _ => throw new ArgumentOutOfRangeException($"Invalid Item ID")
    };
}