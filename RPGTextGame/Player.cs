using System.ComponentModel;
using System.Data;

namespace RPGTextGame;

public class Player: Characters
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
                    Console.WriteLine($"Health has been improved! Your current max health is {player.MaxHealth}"); //TODO need to cap Health to 100 to prevent overhealing
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

    public static void InventoryAccess(Player player) // TODO implement equipping items and using them
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
            else if(option == "equip")
            {
                int itemId = 1;
                EquippingItem(player, itemId);
                
            }
        }

    }

    public static void EquippingItem(Player player, int itemId)
    {
        Console.WriteLine("What do you want to equip?");
        

        while (true)    
        {
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                string option = Console.ReadLine().ToLower();
                
                if (player.Inventory.Any(item => item.ToLower() == option))
                {
                    Console.WriteLine($"You equipped {player.Inventory[i]}.");
                    if (Items.items.TryGetValue(itemId, out var item))
                    {
                        player.Attack += item.Attack;
                        Console.WriteLine($"{item.Attack} attack was added due to equipping {item.ItemName}");
                        player.Defence += item.Defense;
                        Console.WriteLine($"{item.Defense} defence was added due to equipping {item.ItemName}");//TODO add the headArmor, chestArmor, lArm, rArm etc, new dictionary equipped items
                            //TODO add logic to be not possible to wear 2 swords, 2 chests etc
                    }
                }

                break;
            }

            break;
        }


    }
}