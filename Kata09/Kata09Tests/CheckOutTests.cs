using Kata09;
using Kata09.@enum;
using Kata09.model;
using Kata09.model.discounts;

namespace Kata09Tests;

[TestClass]
public class CheckOutTests
{
    private CheckOut _checkOut;

    [TestMethod]
    public void Test_NoRules_ReturnsFullTotal()
    {
        _checkOut = new CheckOut();

        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.C);
        _checkOut.Scan(ItemId.D);

        Assert.AreEqual(115.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_BulkDiscountRule_ExactMatch_ReturnsDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.A, new BulkDiscount(3, 130.0d))
        });

        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.A);

        Assert.AreEqual(130.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_BulkDiscountRule_MoreThanExactMatch_ReturnsDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.A, new BulkDiscount(3, 130.0d))
        });

        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.A);

        Assert.AreEqual(180.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_TwoBulkDiscountRules_ExactMatch_ReturnsDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.A, new BulkDiscount(3, 130.0d)),
            new Rule(ItemId.B, new BulkDiscount(2, 45.0d))
        });

        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.B);

        Assert.AreEqual(175.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_TwoBulkDiscountRules_ExactMatch_AdditionalItems_ReturnsDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.A, new BulkDiscount(3, 130.0d)),
            new Rule(ItemId.B, new BulkDiscount(2, 45.0d))
        });

        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.C);
        _checkOut.Scan(ItemId.D);

        Assert.AreEqual(210.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_PercentageDiscountRule_ExactMatch_ReturnsDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.A, new PercentageDiscount(0, 0.10d))
        });

        _checkOut.Scan(ItemId.A);

        Assert.AreEqual(45.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_PercentageDiscountRule_Match_AdditionalItems_OneFullOneDiscounted()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.A, new PercentageDiscount(1, 0.10d))
        });

        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.A);

        Assert.AreEqual(95.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_PercentageDiscountRule_NoMatch_ReturnsFullTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.A, new PercentageDiscount(1, 0.10d))
        });

        _checkOut.Scan(ItemId.A);

        Assert.AreEqual(50.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_MultipleDiscounts_TwoMatches_ReturnsDoubleDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.C, new PercentageDiscount(3, 0.50d)),
            new Rule(ItemId.D, new BulkDiscount(3, 40.0d))
        });

        _checkOut.Scan(ItemId.C);
        _checkOut.Scan(ItemId.D);
        _checkOut.Scan(ItemId.C);
        _checkOut.Scan(ItemId.D);
        _checkOut.Scan(ItemId.C);
        _checkOut.Scan(ItemId.D);
        _checkOut.Scan(ItemId.C);

        Assert.AreEqual(110.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_BuyOneGetOneFree_ExactMatch_ReturnDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.C, new BuyAndGetFreeDiscount(1, 1))
        });

        _checkOut.Scan(ItemId.C);

        Assert.AreEqual(20.0d, _checkOut.Total);

        _checkOut.Scan(ItemId.C);

        Assert.AreEqual(20.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_BuyTwoGetOneFree_ExactMatch_ReturnDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.D, new BuyAndGetFreeDiscount(2, 1))
        });

        _checkOut.Scan(ItemId.D);
        _checkOut.Scan(ItemId.D);

        Assert.AreEqual(30.0d, _checkOut.Total);

        _checkOut.Scan(ItemId.D);

        Assert.AreEqual(30.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_BuyThreeGetTwoFree_ExactMatch_ReturnDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.B, new BuyAndGetFreeDiscount(3, 2))
        });

        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.B);

        Assert.AreEqual(90.0d, _checkOut.Total);

        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.B);

        Assert.AreEqual(90.0d, _checkOut.Total);
    }
    
    [TestMethod]
    public void Test_BuyThreeGetTwoFree_FourItems_ReturnsDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.B, new BuyAndGetFreeDiscount(3, 2))
        });

        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.B);

        Assert.AreEqual(90.0d, _checkOut.Total);

        _checkOut.Scan(ItemId.B);

        Assert.AreEqual(90.0d, _checkOut.Total);
    }
    
    [TestMethod]
    public void Test_BuyThreeGetTwoFree_LastItemAdded_ReturnsDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new Rule(ItemId.B, new BuyAndGetFreeDiscount(3, 2))
        });

        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.B);

        Assert.AreEqual(90.0d, _checkOut.Total);

        _checkOut.Scan(ItemId.B);

        Assert.AreEqual(90.0d, _checkOut.Total);
        
        _checkOut.Scan(ItemId.B);

        Assert.AreEqual(90.0d, _checkOut.Total);
    }
}