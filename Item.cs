// One item on the shopping list.
class Item
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(string name, int price)
    {
        // Om användaren matar in inget namn men pris
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException ("Du måste ange ett namn på varan");
        }

        // Om användaren matar in negativt tal
        if (price <= 0)
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
