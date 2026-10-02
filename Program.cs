ShoppingList list = new ShoppingList("items.txt");

// Felhantering så att inte programmet kraschar om items.txt saknas.
try
{
    File.ReadAllText("items.txt");
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
        bool priceSuccess = int.TryParse(Console.ReadLine(), out int price);

        // kollar så inmatning är rätt samt inte mindre än 0.
        if (priceSuccess == false || price <= 0)
        {
            Console.WriteLine("Felaktig inmatning");
            Console.ReadLine();
            continue;
        }

        // Försöker lägga till i listan
        try
        {
             // Om det blir mer än budget så läggs inte varan till.
            bool added = list.Add(new Item(name, price));
        
            if (added == false)
            {
               Console.WriteLine($"Din budget är: {list.Budget}. Varan du försökte lägga till har inte lagts till");
               Console.ReadLine();
            }
        }

        // Fångar in tom inmatning samt pris som är 0 eller lägre
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }

    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        // Läser in vilken vara som ska tas bort (nummer)
        bool removeSuccess = int.TryParse(Console.ReadLine(), out int number);      

         // Identifierar varan
        int index = number -1;
    
    // Stoppar nummer och bokstäver som inte finns i listan
    if (!removeSuccess || index < 0 || index >= list.Count)
        {
            Console.WriteLine("Ogilitigt nummer. Tryck enter för att komma till menyn");
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
