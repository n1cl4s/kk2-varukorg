// One item on the shopping list.
class Item
{
    
    public string Name { get; private set; }
    public int Price { get; private set; }

    public Item(string name, int price)
    {
        // Måste ange namn
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException ("Du måste ange ett namn på varan");
        }

        // Får inte vara ett negativt värde
        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Priset kan inte vara negativt");
        }
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
