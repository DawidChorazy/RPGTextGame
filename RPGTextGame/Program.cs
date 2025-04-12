using System.Globalization;
using RPGTextGame;

public class Program
{


    public static void Main(string[] args)
    {
        Player player = StartOfGame();

        while (player.Health > 0)
        {
            RandomEventGenerator(player);
        }
    }
    public static Player StartOfGame()
        {
            Console.WriteLine("Welcome to RPGTextGame!");
            Console.WriteLine("Choose your Name:");
            string name = Console.ReadLine();
            string className = "";
            string race = "";

            while (true)
            {
                Console.WriteLine("Choose your class (rogue, archer, warrior, mage):");
                className = Console.ReadLine();

                if (className == "rogue" || className == "archer" || className == "warrior" || className == "mage")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid class, enter once again");

                }
            }

            while (true)
            {
                Console.WriteLine("Choose your race (human, elf, orc, dwarf):");
                race = Console.ReadLine();
                {
                    if (race == "human" || race == "elf" || race == "orc" || race == "dwarf")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid race, enter once again");
                    }
                }
            }

            return new Player(name, className, race);
        } 
        
    
    public static void RandomEventGenerator(Player player)
    {
        
        Random randomD2Output = new Random();
        Random randomD4Output = new Random();
        Random randomD6Output = new Random();
        Random randomD7Output = new Random();
        Random randomD10Output = new Random();
        Random randomD20Output = new Random();
        Random randomD49utput = new Random();
        Random randomD50Output = new Random();
        Random randomD100Output = new Random();

        string option = "";
        int randomD2 = randomD2Output.Next(1, 3);
        int randomD4 = randomD4Output.Next(1, 5);
        int randomD5 = randomD6Output.Next(1, 6);
        int randomD7 = randomD7Output.Next(1, 8);
        int randomD10 = randomD10Output.Next(1, 11);
        int randomD20 = randomD20Output.Next(1, 21);
        int randomD50 = randomD50Output.Next(1, 51);
        int randomD48 = randomD49utput.Next(1, 49);
        int randomD100 = randomD100Output.Next(1, 101);
        Enemy enemy = Enemy.GetEnemy(randomD7);
        Enemy boss = Enemy.GetBoss(randomD2);
        

        if (player.Health > 0)
        {
            switch (randomD48)
            {
                case 1:
                    Console.WriteLine("Critical failure! You stepped into the trap!");
                    Console.WriteLine($"You lost {randomD10} of health points. Your current health is {player.Health -=  randomD10}");
                    Thread.Sleep(2000);
                    break;

                case 2:
                case 3:
                case 4:
                    Console.WriteLine("You found the enemy. You cannot run away nor hide.");
                    Console.WriteLine(
                        $"You are fighting {enemy.Name} with power of {enemy.Attack} and {enemy.Health} health!");
                    Thread.Sleep(2000);
                    Fighting(player, enemy);
                    break;

                case >= 5 and <= 10:
                    Console.WriteLine("Nothing Happens");
                    Thread.Sleep(2000);
                    break;
                case 11:
                case 12:
                case 13:
                    Console.WriteLine("Crossroads! Choose to go left or right:");
                    
                    while (true)
                    {
                        string cross = Console.ReadLine().ToLower();
                        if (cross == "left" || cross == "right")
                        {
                            if (randomD4 == 1)
                            {
                                player.Health -= randomD20;
                                Console.WriteLine(
                                    $"You lost {randomD20} health points due to stepping into trap! Your current health is {player.Health}");
                                Thread.Sleep(2000);

                            }
                            else
                            {
                                Console.WriteLine("Nothing Happens");
                                Thread.Sleep(2000);
                            }

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option");
                        }
                    }

                    break;

                case 14:
                case 15:
                    Console.WriteLine("On your path from the darkness you hear elderly voice...");
                    Thread.Sleep(2000);
                    Merchant randomMerchant = Merchant.GetRandomMerchant();
                    Console.WriteLine($"It's a {randomMerchant.Name}. You can buy something from him");
                    Merchant.Buying(player, randomMerchant);
                    break;
                
                case >= 16 and <= 20:
                    Console.WriteLine("You found enemy [fight/flee/inventory]");
                    Console.WriteLine(
                        $"You are fighting {enemy.Name} with power of {enemy.Attack} and {enemy.Health} health!");
                    while (true)
                    {
                        option = Console.ReadLine().ToLower();
                        if (option == "fight")
                        {
                            Fighting(player, enemy);
                            break;
                        }
                        else if (option == "flee")
                        {
                            Player.Flee(player);
                            break;
                        }
                        else if (option == "inventory")
                        {
                            Player.InventoryAccess(player);
                        }
                        else
                        {
                            Console.WriteLine("Invalid option");
                        }
                    }

                    Thread.Sleep(2000);
                    break;

                case 21:
                case 22:
                    Console.WriteLine("You found supplies");
                    Player.CoinsGain(player, randomD20);
                    Thread.Sleep(2000);
                    break;

                case 23:
                    Console.WriteLine("You found treasure");
                    Player.CoinsGain(player, randomD50);
                    Thread.Sleep(2000);
                    break;
                case >= 24 and <= 27:
                    Console.WriteLine($"You found campsite, you rested beside it and regenerated to {player.Health += randomD20} health");
                    break;
                case >= 28 and <= 31:
                    Console.WriteLine("You felt freezing shiver on your spine. You thought you saw something between trees");
                    player.DoorCounter++;
                    if (player.DoorCounter >= 2)
                    {
                        Console.WriteLine("Between trees you see hidden illusory doors. You can't resist to enter. [enter]");
                        option = Console.ReadLine().ToLower();
                        if (option == "enter" && player.Inventory.Contains("Cursed key"))
                        {
                            player.Inventory.Remove("Cursed key");
                            Console.WriteLine("Thank you for playing! TBC...");
                        }
                        else
                        {
                            Console.WriteLine("Thank you for playing! TBC...");
                            return;
                        }

                    }
                    Thread.Sleep(2000);
                    break;
                case >= 32 and <= 34:
                    Console.WriteLine("Dead body of other adventurer is under your feet. You spot a weird looking key on his neck.");
                    Console.WriteLine("You feel that is magic or cursed item [pick/exit]");
                    option = Console.ReadLine().ToLower();
                    while (true)
                    {
                        if (option == "pick")
                        {
                            player.Inventory.Add("Cursed key");
                            Console.WriteLine("You picked up a key");
                            break;
                        }
                        else if (option == "exit")
                        {
                            return;
                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid option");
                        }
                    }

                    Thread.Sleep(2000);
                    break;
                case >= 35 and <= 43:
                    Console.WriteLine("Calming breeze of wind gives an opportunity to rest from thinking about this frightening world...");
                    Thread.Sleep(2000);
                    break;
                case >= 44 and <= 47:
                    Console.WriteLine("You found some berries and ate them");
                    
                    Console.WriteLine($"You regenerated {player.Health += randomD4} of your health. Current health is {player.Health}");
                    Thread.Sleep(2000);
                    break;
                case 48:
                    Console.WriteLine("Abhorrent creature appears from nowhere. You need to fight it, whatever it is [fight/inventory]");
                    Console.WriteLine(
                        $"You are fighting {boss.Name} with power of {boss.Attack} and {boss.Health} health!");
                    while (true)
                    {
                        option = Console.ReadLine().ToLower();
                        if (option == "fight")
                        {
                            Fighting(player, boss);
                            break;
                        }
                        else if (option == "flee")
                        {
                            Player.Flee(player);
                            break;
                        }
                        else if (option == "inventory")
                        {
                            Player.InventoryAccess(player);
                        }
                        else
                        {
                            Console.WriteLine("Invalid option");
                        }
                    }
                    Thread.Sleep(2000);
                        break;
                    
            }
        }
    }

    private static void Fighting(Player player, Enemy opp)
    {
        Random RandomDefensiveDie = new Random();
        int defensiveDie = RandomDefensiveDie.Next(1, 16);
        string option = "";
        bool defendActive = false;
        int roundCounter = 0;

        while (opp.Health > 0 && player.Health > 0)
        {
            roundCounter++;
            Console.WriteLine($"ROUND {roundCounter}");
            Console.WriteLine("[attack/defend/inventory]");

            while (true)
            {
                option = Console.ReadLine().ToLower();

                if (option == "attack" || option == "defend" || option == "inventory")
                    break;

                Console.WriteLine("Invalid option. Please type 'attack' or 'defend'.");
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


            if (opp.Health > 0)
            {
                int defensiveDamageTaken = opp.Attack - defensiveDie;
                if (defendActive == true)
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



