using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace RPGTextGame;

public class Player : Characters
{
    public string Name { get; set; }
    public override string Class { get; set; }
    private string Race { get; set; }

    public override double Health { get; set; }

    public double MaxHealth { get; set; }
    public double MaxMana { get; set; }
    public double Mana { get; set; }
    public override int Attack { get; set; }
    
    public override int Defence { get; set; }
    public override int Intelligence { get; set; }
    public override int DodgeChance { get; set; }

    private int Experience { get; set; } = 0;
    private int Level { get; set; } = 1;

    public int Coins { get; set; }

    public List<string> Inventory { get; set; } = new List<string>();
    
    public List<string> EquippedItems { get; set; } = new List<string>();
    
    public static List<Spells> SpellBook { get; set; } = new List<Spells>();
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
        MaxMana = 0;
        Mana = MaxMana;
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

    public static void SpellsAccess(Player player)
    {
        Console.WriteLine("What do you want to do?");
        Console.WriteLine("Type '[spell name] info' to see description or 'exit' to leave.");

        while (true)
        {
            Console.WriteLine("\nYour spells:");
            foreach (var item in SpellBook)
            {
                Console.WriteLine($"- {item.SpellName}");
            }

            Console.Write("\n> ");
            string option = Console.ReadLine().ToLower();

            if (option == "exit")
            {
                break;
            }
            else if (option.EndsWith(" info"))
            {
                string spellName = option.Replace(" info", "").Trim();
                var selectedSpell = SpellBook
                    .FirstOrDefault(s => s.SpellName.ToLower() == spellName);

                if (selectedSpell != null)
                {
                    Console.WriteLine($"\n{selectedSpell.ItemDescription}");
                }
                else
                {
                    Console.WriteLine("You don't have a spell with that name.");
                }
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
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
                        player.Mana += item.ManaRecovery;
                        if (player.Mana >= 100)
                        {
                            player.Mana = player.MaxMana;
                        }
                        if (player.Health > 100)
                        {
                            player.Health = player.MaxHealth;
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
        bool classChoice = false;
        bool raceChoice = false;
        
        Console.WriteLine("Choose your Name:");
        player.Name = Console.ReadLine().ToLower();
        
        while (!classChoice)
        {
            Console.WriteLine("Choose your class (rogue, archer, warrior, mage):");
            className = Console.ReadLine().ToLower();

            switch (className)
            {
                case "rogue":
                    player.Class = "Rogue";
                    classChoice = true;
                    break;
                case "archer":
                    player.Class = "Archer";
                    classChoice = true;
                    break;
                case "warrior":
                    player.Class = "Warrior";
                    classChoice = true;
                    break;
                case "mage":
                    player.Class = "Mage";
                    classChoice = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            
        }
        while (!raceChoice)
        {
            Console.WriteLine("Choose your race (human, elf, orc, dwarf):");
            raceName = Console.ReadLine().ToLower();

            switch (raceName)
            {
                case "human":
                    player.Race = "Human";
                    raceChoice = true;
                    break;
                case "elf":
                    player.Race = "Elf";
                    raceChoice = true;
                    break;
                case "orc":
                    player.Race = "Orc";
                    raceChoice = true;
                    break;
                case "dwarf":
                    player.Race = "Dwarf";
                    raceChoice = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            
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
            player.DodgeChance += 15;
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
            player.MaxMana = 100;
            var spell = Spells.spells.Values.FirstOrDefault(s => s.SpellName == "Sleeping Fart");
            if (spell != null)
            {
                Player.SpellBook.Add(spell);
            }

        }
    }
    public static void Fighting(Player player, Enemy opp)
    {
        Random randomDodgeDie = new Random();
        Random RandomDefensiveDie = new Random();
        Random randomIgniteDie = new Random();
        int ignite = randomIgniteDie.Next(1, 5);
        int defensiveDie = RandomDefensiveDie.Next(1, 16);
        int rollForDodge = randomDodgeDie.Next(1, 101);
        string option = "";
        bool defendActive = false;
        int roundCounter = 0;
        var spellOption = SpellBook.FirstOrDefault(s => s.SpellName == option);
        string spellToCast = "";
        int turnsInactive = 0;
        int turnsIgnited = 0;

        while (opp.Health > 0 && player.Health > 0)
        {

                roundCounter++;
                Console.WriteLine($"ROUND {roundCounter}");
                Console.WriteLine("[attack/cast/defend/inventory/spells]");

                while (true)
                {
                    option = Console.ReadLine().ToLower();

                    if (option == "attack" || option.StartsWith("cast ") || option == "defend" || option == "inventory" || option == "spells")
                        break;

                    Console.WriteLine("Invalid option. Please type [attack/cast + [spell name]/defend/inventory/spells].");
                }


                if (option == "attack")
                {
                    Console.WriteLine($"Enemy got {opp.GetDamage(player, false)} damage. Current enemy health is {opp.Health}");
                }
                else if (option == "defend")
                {
                    Console.WriteLine("You try to defend against monster");
                    defendActive = true;
                }
                else if (option == "inventory")
                {
                    Player.InventoryAccess(player);
                }
                else if (option == "spells")
                {
                    Player.SpellsAccess(player);
                }
                else if (option.StartsWith("cast "))
                {
                    string spellName = option.Substring(5).Trim();

                    var spell = SpellBook.FirstOrDefault(s => s.SpellName.ToLower() == spellName);
                    if (spell != null && spell.SpellName == "Sleeping Fart" && player.Mana > spell.ManaDrain)
                    {
                        player.Mana -= spell.ManaDrain;
                        Console.WriteLine("You cast Sleeping Fart. The enemy is stunned for 2 turns!");
                        Console.WriteLine($"You lost {spell.ManaDrain} mana.");
                        turnsInactive = 2;
                    }
                    else if (spell != null && spell.SpellName == "Fireball" && player.Mana > spell.ManaDrain)
                    {
                        player.Mana -= spell.ManaDrain;
                        Console.WriteLine($"You cast Fireball. You deal {spell.Attack} damage.");
                        Console.WriteLine($"You lost {spell.ManaDrain} mana.");
                        opp.Health -= spell.Attack;

                        if (ignite == 4)
                        {
                            Console.WriteLine("You ignited your enemy!");
                            turnsIgnited = 2;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You can't cast that spell.");
                    }
                }
                
                
                if (opp.Health > 0)
                {
                        int defensiveDamageTaken = opp.Attack - defensiveDie;
                        int playerDodgeChance = player.DodgeChance;

                        if (turnsInactive > 0)
                        {
                            Console.WriteLine($"Enemy is stunned and cannot attack! ({turnsInactive} turns remaining)");
                            turnsInactive--;
                        }
                        else
                        {
                            if (playerDodgeChance >= rollForDodge)
                            {
                                Console.WriteLine("You dodged an attack!");
                            }
                            else if (defendActive)
                            {
                                Console.WriteLine(
                                    $"You got hit for {player.GetDamage(opp, true)}. You have {player.Health} remaining life points");

                                defendActive = false;
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"You got hit for {player.GetDamage(opp, false)}. You have {player.Health} remaining life points");

                            }
                        }
                        if (turnsIgnited > 0)
                        {
                            opp.Health -= 3;
                            turnsIgnited--;
                        }

                }

                var minorRecoverySpell = Player.SpellBook.FirstOrDefault(s => s.SpellName == "Minor Recovery");

                if (minorRecoverySpell != null)
                {
                    player.Health += minorRecoverySpell.HealthRecovery;
                    player.Mana += minorRecoverySpell.ManaRecovery;
                    Console.WriteLine($"You regenerated {minorRecoverySpell.HealthRecovery} health and {minorRecoverySpell.ManaRecovery} mana due to Minor Recovery spell.");
                }
        }


        if (player.Health <= 0)
        {
            Console.WriteLine("Unexpectedly your opponent gives you a kiss of sudden death");
            Environment.Exit(0);
        }
        else if (opp.Health <= 0)
        {
            Console.WriteLine("You have slain your opponent");
            Player.CoinsGain(player, opp.CoinsAfterDefeated );
            Player.ExperianceGain(player, opp.ExperienceAfterDefeated);
        }
    }
    
}

