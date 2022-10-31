using Kata09;
using Kata09.@enum;
using Kata09.model.discounts;
using Kata09.model.rules;

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
            new SingleItemRule(new BulkDiscount(3, 130.0d), ItemId.A)
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
            new SingleItemRule(new BulkDiscount(3, 130.0d), ItemId.A)
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
            new SingleItemRule(new BulkDiscount(3, 130.0d), ItemId.A),
            new SingleItemRule(new BulkDiscount(2, 45.0d), ItemId.B)
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
            new SingleItemRule(new BulkDiscount(3, 130.0d), ItemId.A),
            new SingleItemRule(new BulkDiscount(2, 45.0d), ItemId.B)
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
            new SingleItemRule(new PercentageDiscount(0, 0.10d), ItemId.A)
        });

        _checkOut.Scan(ItemId.A);

        Assert.AreEqual(45.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_PercentageDiscountRule_Match_AdditionalItems_OneFullOneDiscounted()
    {
        _checkOut = new CheckOut(new[]
        {
            new SingleItemRule(new PercentageDiscount(1, 0.10d), ItemId.A)
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
            new SingleItemRule(new PercentageDiscount(1, 0.10d), ItemId.A)
        });

        _checkOut.Scan(ItemId.A);

        Assert.AreEqual(50.0d, _checkOut.Total);
    }

    [TestMethod]
    public void Test_MultipleDiscounts_TwoMatches_ReturnsDoubleDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new SingleItemRule(new PercentageDiscount(3, 0.50d), ItemId.C),
            new SingleItemRule(new BulkDiscount(3, 40.0d), ItemId.D)
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
            new SingleItemRule(new BuyAndGetFreeDiscount(1, 1), ItemId.C)
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
            new SingleItemRule(new BuyAndGetFreeDiscount(2, 1), ItemId.D)
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
            new SingleItemRule(new BuyAndGetFreeDiscount(3, 2), ItemId.B)
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
            new SingleItemRule(new BuyAndGetFreeDiscount(3, 2), ItemId.B)
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
            new SingleItemRule(new BuyAndGetFreeDiscount(3, 2), ItemId.B)
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

    [TestMethod]
    public void Test_CompositeDiscount_OneOfEachItem_ReturnsDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new MultiItemRule(new CompositeDiscount(
                new[]
                {
                    new DiscountPart(ItemId.A, 1),
                    new DiscountPart(ItemId.B, 1),
                    new DiscountPart(ItemId.C, 1),
                    new DiscountPart(ItemId.D, 1)
                },
                110.0d))
        });

        _checkOut.Scan(ItemId.A);
        
        Assert.AreEqual(50.0d, _checkOut.Total);

        _checkOut.Scan(ItemId.B);
        
        Assert.AreEqual(80.0d, _checkOut.Total);

        _checkOut.Scan(ItemId.C);
        
        Assert.AreEqual(100.0d, _checkOut.Total);

        _checkOut.Scan(ItemId.D);
        
        Assert.AreEqual(110.0d, _checkOut.Total);
    }
    
    [TestMethod]
    public void Test_CompositeDiscount_DifferentNumbersOfEachItem_ReturnsDiscountedTotal()
    {
        _checkOut = new CheckOut(new[]
        {
            new MultiItemRule(new CompositeDiscount(
                new[]
                {
                    new DiscountPart(ItemId.A, 1),
                    new DiscountPart(ItemId.B, 2),
                    new DiscountPart(ItemId.C, 3),
                    new DiscountPart(ItemId.D, 4)
                },
                200.0d))
        });

        _checkOut.Scan(ItemId.A);
        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.C);
        _checkOut.Scan(ItemId.D);

        Assert.AreEqual(115.0d, _checkOut.Total);

        _checkOut.Scan(ItemId.B);
        _checkOut.Scan(ItemId.C);
        _checkOut.Scan(ItemId.D);

        Assert.AreEqual(180.0d, _checkOut.Total);
        
        _checkOut.Scan(ItemId.C);
        _checkOut.Scan(ItemId.D);

        Assert.AreEqual(215.0d, _checkOut.Total);
        
        _checkOut.Scan(ItemId.D);

        Assert.AreEqual(200.0d, _checkOut.Total);
    }
}