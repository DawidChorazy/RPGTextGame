using System.ComponentModel;
using System.Data;

namespace RPGTextGame;

public class Player : Characters
{
    private string Name { get; set; }
    private string Class { get; set; }
    private string Race { get; set; }

    public override int Health { get; set; }

    private int MaxHealth { get; set; }
    public override int Attack { get; set; }

    public override int Defence { get; set; }
    private int Intelligence { get; set; }

    private int Experience { get; set; } = 0;
    private int Level { get; set; } = 1;

    public int Coins { get; set; }

    public List<string> Inventory { get; set; } = new List<string>();
    public string Weapon { get; set; }
    public string Offhand { get; set; }
    public string Helmet { get; set; }
    public string Chestplate { get; set; }
    public string Legs { get; set; }

    public Player(string name, string className, string race)
    {
        Name = name;
        Class = className;
        Race = race;
        Health = 100;
        MaxHealth = Health;
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
            Console.WriteLine($"Level up! You are currently Lvl.{player.Level}");
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

            Console.WriteLine("Choose to use item or exit");
            string option = Console.ReadLine().ToLower();
            if (option == "exit")
            {
                break;
            }
            else if (option == "equip")
            {
                int itemId = 1;
                EquippingItem(player);

            }
        }

    }

    public static void EquippingItem(Player player)
    {


        while (true)
        {
            Console.WriteLine("What do you want to equip?");
            foreach (var item in player.Inventory)
            {
                Console.WriteLine(item);
            }
            string option = Console.ReadLine().ToLower();
            
            if (player.Inventory.Any(item => item.ToLower() == option))
            {
                var itemEntry = Items.items.FirstOrDefault(kv => kv.Value.ItemName.ToLower() == option);
                if (!itemEntry.Equals(default(KeyValuePair<int, Items>)))
                {
                    var item = itemEntry.Value;
                    Console.WriteLine($"You equipped {option}.");

                    switch (item.ItemType)
                    {
                        case "Weapon":
                            player.Weapon = option;
                            player.Attack += item.Attack;
                            player.Inventory.Remove(option);
                            break;
                        case "Offhand":
                            player.Offhand = option;
                            player.Defence += item.Defense;
                            player.Inventory.Remove(option);
                            break;
                        case "Helmet":
                            player.Helmet = option;
                            player.Defence += item.Defense;
                            player.Inventory.Remove(option);
                            break;
                        case "Chestplate":
                            player.Chestplate = option;
                            player.Defence += item.Defense;
                            player.Inventory.Remove(option);
                            break;
                        case "Legs":
                            player.Legs = option;
                            player.Defence += item.Defense;
                            player.Inventory.Remove(option);
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

}