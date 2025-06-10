using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace RPGTextGame;

public class Player : Characters
{
    public string Name { get; set; }
    public string Class { get; set; }
    private string Race { get; set; }

    public override double Health { get; set; }

    public double MaxHealth { get; set; }
    public override int Attack { get; set; }

    public override int Defence { get; set; }
    public override int Intelligence { get; set; }
    public override double DodgeChance { get; set; }

    private int Experience { get; set; } = 0;
    private int Level { get; set; } = 1;

    public int Coins { get; set; }

    public List<string> Inventory { get; set; } = new List<string>();
    
    public List<string> EquippedItems { get; set; } = new List<string>();
    public string Weapon { get; set; }
    public string Offhand { get; set; }
    public string Helmet { get; set; }
    public string Chestplate { get; set; }
    public string Legs { get; set; }
    public int DoorCounter {get; set;}

    public Player()
    {
        Name = "";
        Class = "";
        Race = "";
        MaxHealth = 100;
        Health = MaxHealth;
        Attack = 10;
        Experience = 0;
        Level = 1;
        Intelligence = 0;
        Defence = 0;
        Coins = 300; // change amount of coins
        Weapon = null;
        Offhand = null;
        Helmet = null;
        Chestplate = null;
        Legs = null;
        DoorCounter = 0;

    }

    public static void Flee(Player player)
    {
        Random randomFleeDie = new Random();
        int fleeDie = randomFleeDie.Next(1, 21);

        if (fleeDie >= 10)
        {
            Console.WriteLine("You succeeded to flee");
        }
        else
        {
            player.Health -= fleeDie;
            Console.WriteLine($"You failed to flee and lost {fleeDie} hp due to opportunist attack ");
        }
    }

    public static void AddingPlayerAttributes(Player player)
    {
        Console.WriteLine(
            "Choose which attributes you want to upgrade (3 points) [attack,defense,health,intelligence(not implemented)]"); //TODO int not implemented

        for (int i = 0; i < 3; i++)
        {
            while (true)
            {
                string option = Console.ReadLine().ToLower();

                if (option == "attack")
                {
                    player.Attack += 1;
                    Console.WriteLine($"Attack has been improved! Your current attack {player.Attack}");
                    break;
                }
                else if (option == "defense")
                {
                    player.Defence += 1;
                    Console.WriteLine($"Defense has been improved! Your current defense {player.Defence}");
                    break;
                }
                else if (option == "health")
                {
                    player.MaxHealth += 10;
                    player.Health += 10;
                    Console.WriteLine(
                        $"Health has been improved! Your current max health is {player.MaxHealth}"); //TODO need to cap Health to 100 to prevent overhealing
                    break;
                }
                else if (option == "intelligence")
                {
                    player.Intelligence += 1;
                    Console.WriteLine(
                        $"Intelligence has been improved! Your current intelligence {player.Intelligence}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }
    }

    public static void ExperianceGain(Player player, int xp)
    {
        player.Experience += xp;
        Console.WriteLine($"Achieved {xp} xp. Current Experience is {player.Experience}/100");
        if (player.Experience >= 100)
        {
            player.Level += 1;
            player.Experience -= 100;
            player.Health += 0.25 * player.MaxHealth;

            Console.WriteLine($"Level up! You are currently Lvl.{player.Level}");
            if (player.Health >= player.MaxHealth)
            {
                player.Health = player.MaxHealth;
                Console.WriteLine($"You regenerated to your max health of {player.MaxHealth}");
            }
            Console.WriteLine($"You regenerated some of your health and you have now {player.Health} health.");
            AddingPlayerAttributes(player);
        }
    }

    public static void CoinsGain(Player player, int coin)
    {
        player.Coins += coin;
        Console.WriteLine($"You got {coin} coin(s). Your current wealth is {player.Coins} coins.");

    }

    public static void InventoryAccess(Player player)
    {

        while (true)
        {
            foreach (var item in player.Inventory)
            {
                Console.WriteLine(item);
            }

            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("You have nothing in your inventory.");
            }

            Console.WriteLine("Choose to [use/equip/unequip] item or [exit].");
            string option = Console.ReadLine().ToLower();
            if (option == "exit")
            {
                return;
            }
            else if (option == "equip")
            {
                EquippingItem(player);

            }
            else if (option == "use")
            {
                UsingItem(player);
            }
            else if (option == "unequip")
            {
                UnequippingItem(player);
            }
        }

    }

    public static void EquippingItem(Player player)
    {


        while (true)
        {
            Console.WriteLine("What do you want to equip?");
            Console.WriteLine("------------------");
            foreach (var item in player.Inventory)
            {
                Console.WriteLine($"|{item}|");
            }
            Console.WriteLine("------------------");
            string option = Console.ReadLine().ToLower();
            
            if (player.Inventory.Any(item => item.ToLower() == option))
            {
                var itemEntry = Items.items.FirstOrDefault(kv => kv.Value.ItemName.ToLower() == option);
                if (!itemEntry.Equals(default(KeyValuePair<int, Items>)))
                {
                    var item = itemEntry.Value;
                    Console.WriteLine($"You equipped {option}.");

                    switch (item.ItemType)
                    { //TODO test equipping multiple weapons
                        case "Weapon":
                            if(player.Weapon is not null)
                            {           
                                player.Weapon = option;
                                player.Attack += item.Attack;
                                player.Inventory.Remove(option);
                                player.EquippedItems.Add(option);
                                
                            }
                            else
                            {
                                Console.WriteLine("Item already equipped.");
                            }

                            break;  
                 
                        case "Offhand":
                            player.Offhand = option;
                            player.Defence += item.Defense;
                            player.Inventory.Remove(option);
                            player.EquippedItems.Add(option);
                            break;
                        case "Helmet":
                            player.Helmet = option;
                            player.Defence += item.Defense;
                            player.Inventory.Remove(option);
                            player.EquippedItems.Add(option);
                            break;
                        case "Chestplate":
                            player.Chestplate = option;
                            player.Defence += item.Defense;
                            player.Inventory.Remove(option);
                            player.EquippedItems.Add(option);
                            break;
                        case "Legs":
                            player.Legs = option;
                            player.Defence += item.Defense;
                            player.Inventory.Remove(option);
                            player.EquippedItems.Add(option);
                            break;
                        default:
                            Console.WriteLine("This item is usable, not equipable.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Item not found in inventory.");
            }

            Console.WriteLine("Do you want to equip another item? [equip/exit]");
            string choice = Console.ReadLine().ToLower();
            if (choice == "exit")
            {
                break;
            }
            else if (choice != "equip")
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }

    public static void UnequippingItem(Player player)
    {
        while (true)
        {
            Console.WriteLine("What do you want to unequip?");
            foreach (var item in player.EquippedItems)
            {
                Console.WriteLine(item);
            }

            string option = Console.ReadLine().ToLower();

            string itemsToUnequip = player.EquippedItems.FirstOrDefault(i => i.ToLower() == option);

            if (itemsToUnequip != null)
            {


                var itemEntry = Items.items.FirstOrDefault(kv => kv.Value.ItemName.ToLower() == option);

                if (itemEntry.Value != null)
                {
                    player.EquippedItems.Remove(itemsToUnequip);
                    player.Inventory.Add(option);

                    player.Attack -= itemEntry.Value.Attack;
                    player.Defence -= itemEntry.Value.Defense;
                    player.Health -= itemEntry.Value.HealthRecovery;

                    Console.WriteLine($"Item {itemEntry.Value.ItemName} unequipped.");
                }
                else
                {
                    Console.WriteLine("Item not found in inventory.");
                }

            }
            else
            {
                Console.WriteLine("Item not found on your character.");
            }
            Console.WriteLine("Do you want to unequip another item? [unequip/exit]");
            
            option = Console.ReadLine().ToLower();

            if (option == "exit")
            {
                break;
            }
            else if (option == "unequip")
            {
                UnequippingItem(player);
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
        
    }
    
    public static void UsingItem(Player player)
    {
        while (true)
        {
            Console.WriteLine("What do you want to use?");
            Console.WriteLine("------------------");
            foreach (var item in player.Inventory)
            {
                Console.WriteLine("|{item}|");
            }
            Console.WriteLine("------------------");
            string option = Console.ReadLine().ToLower();
            
            if (player.Inventory.Any(item => item.ToLower() == option))
            {
                var itemEntry = Items.items.FirstOrDefault(kv => kv.Value.ItemName.ToLower() == option);
                if (!itemEntry.Equals(default(KeyValuePair<int, Items>)))
                {
                    var item = itemEntry.Value;
                    if (item.ItemType == "Usable")
                    {
                        player.Attack += item.Attack;
                        player.Health += item.HealthRecovery;
                        if (player.Health > 100)
                        {
                            player.Health = 100;
                        }
                        Console.WriteLine($"You used {option}.");
                        player.Inventory.Remove(option);
                    }
                }
            }
            else
            {
                Console.WriteLine("Item not found in inventory.");
            }

            Console.WriteLine("Do you want to use another item? [use/exit]");
            string choice = Console.ReadLine().ToLower();
            if (choice == "exit")
            {
                break;
            }
            else if (choice != "use")
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }

    public static void PlayerClassRaceNameSelection(Player player)
    {
        string className = "";
        string raceName = "";
        
        Console.WriteLine("Choose your Name:");
        player.Name = Console.ReadLine().ToLower();
        
        while (true)
        {
            Console.WriteLine("Choose your class (rogue, archer, warrior, mage):");
            className = Console.ReadLine().ToLower();

            switch (className)
            {
                case "rogue":
                    player.Class = "Rogue";
                    break;
                case "archer":
                    player.Class = "Archer";
                    break;
                case "warrior":
                    player.Class = "Warrior";
                    break;
                case "mage":
                    player.Class = "Mage";
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            break;
        }
        while (true)
        {
            Console.WriteLine("Choose your race (human, elf, orc, dwarf):");
            raceName = Console.ReadLine().ToLower();

            switch (raceName)
            {
                case "human":
                    player.Race = "Human";
                    break;
                case "elf":
                    player.Race = "Elf";
                    break;
                case "orc":
                    player.Race = "Orc";
                    break;
                case "dwarf":
                    player.Race = "Dwarf";
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            break;
        }
        if (raceName == "human")
        {
            player.Attack += 1;
            player.Defence += 1;
        }
        else if (raceName == "elf")
        {
            player.Attack += 1;
            player.Intelligence += 1;
        }
        else if (raceName == "orc")
        {
            player.Attack += 2;
        }
        else if (raceName == "dwarf")
        {
            player.Defence += 2;
        }

        if (className == "rogue")
        {
            player.Attack += 4;
            player.MaxHealth = 90;
            player.Health = 90;
        }
        else if (className == "archer")
        {
            player.Attack += 1;
            player.DodgeChance += 0.15;
        }
        else if (className == "warrior")
        {
            player.Attack += 1;
            player.Defence += 2;
            player.MaxHealth += 15;
        }
        else if (className == "mage")
        {
            player.Intelligence = 3;
        }
    }
    
}

