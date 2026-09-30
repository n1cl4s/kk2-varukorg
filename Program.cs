ShoppingList list = new ShoppingList("items.txt");
list.Load();

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
        int price = int.Parse(Console.ReadLine());
        list.Add(new Item(name, price));
    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        // Läser in vilken vara som ska tas bort (nummer)
        bool removeSuccess = int.TryParse(Console.ReadLine(), out int number);      

         // Identifierar varan
        int index = number -1;
    
    // Om numret är ogiltligt eller finns inte så kraschar inte programmet
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
