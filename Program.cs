// Sökvägen till varorna.
ShoppingList list = new ShoppingList("items.txt");

// Felhantering så att inte programmet kraschar om items.txt saknas.
try
{
    list.Load();
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"Filen kunde inte hittas: {ex.FileName}");
}
while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");

    // Gör så att programmet inte kraschar om man t.ex skriver en bokstav
    int.TryParse(Console.ReadLine(), out int choice);

    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();
        Console.Write("Pris: ");

        // kollar så inmatning är ett heltal
        if (!int.TryParse(Console.ReadLine(), out int price))
        {
            Console.WriteLine("Felaktig inmatning. Det måste vara ett heltal");
            Console.ReadLine();
            continue;
        }

        // Försöker lägga till i listan
        try
        {
             // Om det blir mer än budget så läggs inte varan till.
        
            if (list.Add(new Item(name, price)))
            {
               Console.WriteLine($"Din budget är: {list.Budget}. Varan du försökte lägga till har inte lagts till");
               Console.ReadLine();
            }
        }

        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Felaktig inmatning. Negativt pris");
            Console.ReadLine();
        }

        // Fångar in tom inmatning
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }

    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        // Läser in vilken vara som ska tas bort (nummer) och skyddar mot kraschar om det blir fel inmatning
        int.TryParse(Console.ReadLine(), out int number);

        // Skriver anvädnaren 1 blir det 1an på listan i terminalen. Som egentligen är 0 i en lista. 
        int index = number - 1;

        if (index < 0 || index >= list.Count)
        {
            Console.WriteLine("Ogiltigt nummer. Tryck enter för att komma till menyn");
            Console.ReadLine();
            continue;
        }
        // Varan tas bort från listan
        list.RemoveAt(number);

    }
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }

}
