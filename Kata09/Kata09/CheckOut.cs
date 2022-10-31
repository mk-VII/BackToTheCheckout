using Kata09.@enum;
using Kata09.model;
using Kata09.model.rules.@base;

namespace Kata09;

public class CheckOut
{
    private readonly IEnumerable<Rule> _rules;

    private readonly List<Item> _items;

    public double Total { get; private set; }

    public CheckOut()
    {
        _rules = new List<Rule>();
        _items = new List<Item>();
        Total = 0.0d;
    }
    
    public CheckOut(IEnumerable<Rule> rules)
    {
        _rules = rules;
        _items = new List<Item>();
        Total = 0.0d;
    }

    public void Scan(ItemId id)
    {
        _items.Add(Item.Lookup(id));
        
        Total = GetFullItemsPrice() - GetAmountDiscounted();
    }

    private double GetFullItemsPrice() => _items.Sum(item => item.Price);

    private double GetAmountDiscounted() => _rules.Sum(rule => rule.ApplyRule(_items));
}