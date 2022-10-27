using Kata09.constants;
using Kata09.@enum;
using Kata09.model;

namespace Kata09Tests.model;

[TestClass]
public class ItemTests
{
    [TestMethod]
    public void TestCanConstruct_ItemA()
    {
        var item = Item.Lookup(ItemId.A);
        
        Assert.AreEqual(ItemId.A, item.Id);
        Assert.AreEqual(ItemPrice.A, item.Price);
    }
    
    [TestMethod]
    public void TestCanConstruct_ItemB()
    {
        var item = Item.Lookup(ItemId.B);
        
        Assert.AreEqual(ItemId.B, item.Id);
        Assert.AreEqual(ItemPrice.B, item.Price);
    }
    
    [TestMethod]
    public void TestCanConstruct_ItemC()
    {
        var item = Item.Lookup(ItemId.C);
        
        Assert.AreEqual(ItemId.C, item.Id);
        Assert.AreEqual(ItemPrice.C, item.Price);
    }
    
    [TestMethod]
    public void TestCanConstruct_ItemD()
    {
        var item = Item.Lookup(ItemId.D);
        
        Assert.AreEqual(ItemId.D, item.Id);
        Assert.AreEqual(ItemPrice.D, item.Price);
    }
}