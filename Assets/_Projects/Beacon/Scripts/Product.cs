using UnityEngine;

public class Product : MonoBehaviour
{
    private double _price;
    private string _name;
    private int _quantity;

    public bool ApplyDiscount(int precent)
    {
        if (precent < 0)
            return false;

        const int FullPercent = 100;
        double discount = _price * (double)precent / FullPercent;

        _price -= discount;
        return true;
    }

    public bool Eat(string productName)
    {
        if (productName != _name)
            return false;

        if (_quantity <= 0)
            return false;

        _quantity--;

        Debug.Log($"Съеден продукт: {_name}. Осталось: {_quantity}");

        return true;
    }
}