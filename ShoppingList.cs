// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    // Om items.txt inte kan läsas så är budgeten 500. Annars läses den från items.txt. Detta för programmet ska kunna köras utan items.txt
    private int budget = 500;
    
    // Låter Program.cs läsa budgeten men inte ändra den.
    public int Budget => budget;

    // Kollar antalet varor i listan
    public int Count => items.Count;


    public ShoppingList(string path)
    {
        this.path = path;
    }

    // Om varan som läggs till + varorna som redan är i listan överskrider budgeten, så läggs inte varan till.
    // Jag valde bool i stället för throw eftersom det blir mindre och smidigare kod.
    // Med hjälp av if-satsen så kontrollerar den budgeten.
    public bool Add(Item item)
    {
        if (Total() + item.Price > budget)
        {
            return false;
        }

        items.Add(item);
        return true;
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        items.RemoveAt(number - 1);
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    public void Save()
    {
        List<string> lines = new List<string>();
        // Skriver budget först, så att det inte försvinner ur filen när den sparas.
        lines.Add($"budget;{budget}");

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }


        // Sparar i listan
        try
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n");
            Console.WriteLine("Listan är sparad.");
        }
        // Om items.txt är skrivskyddad får man ett felmeddelande
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Kunde inte spara listan.");
        }

        
    }

    // Reads the file back into the list.
    public void Load()
    {
        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            string[] parts = line.Split(';');

            if (parts[0] == "budget")
            {
                if (int.TryParse(parts[1], out int fileBudget))
                {
                    budget = fileBudget;
                }
                continue;

            }

            items.Add(new Item(parts[1], int.Parse(parts[0])));

            
        }
    }
}
