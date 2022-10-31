namespace Kata09.model.interfaces;

public interface IDiscount
{
    double CalculateDiscount(IEnumerable<Item> items);
    
}