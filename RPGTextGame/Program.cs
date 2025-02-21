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
        Random randomD20Output = new Random();
        Random randomD50Output = new Random();

        string option = "";
        int randomD2 = randomD2Output.Next(1, 3);
        int randomD4 = randomD4Output.Next(1, 5);
        int randomD5 = randomD6Output.Next(1, 6); // enemy index
        int randomD20 = randomD20Output.Next(1, 21); //die
        int randomD50 = randomD50Output.Next(1, 51);
        Enemy enemy = Enemy.GetEnemy(randomD5);
        

        if (player.Health > 0)
        {
            switch (randomD20)
            {
                case 1:
                    Console.WriteLine("Critical failure! You stepped into the trap!");
                    player.Health -= randomD20;
                    Console.WriteLine($"You lost {randomD20} of health points. Your current health is {player.Health - randomD20}");
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

                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    Console.WriteLine("Nothing Happens");
                    Thread.Sleep(2000);
                    break;
                case 11:
                case 12:
                case 13:
                    Console.WriteLine("Crossroads! Choose to go left or right:");
                    string cross = Console.ReadLine().ToLower();
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

                case 14:
                case 15:
                    Console.WriteLine("On your path from the darkness you hear elderly voice...");
                    Thread.Sleep(2000);
                    Merchant randomMerchant = Merchant.GetRandomMerchant();
                    Console.WriteLine($"It's a {randomMerchant.Name}. You can buy something from him");
                    Merchant.Buying(player, randomMerchant);
                    break;
                
                case 16:
                case 17:
                    Console.WriteLine("You found enemy (You can fight or flee)");
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
                        else
                        {
                            Console.WriteLine("Invalid option");
                        }
                    }

                    Thread.Sleep(2000);
                    break;

                case 18:
                case 19:
                    Console.WriteLine("You found supplies"); //TODO add supplies(default)
                    Player.CoinsGain(player, randomD20);
                    Thread.Sleep(2000);
                    break;

                case 20:
                    Console.WriteLine("You found treasure"); //TODO add supplies(better)
                    Player.CoinsGain(player, randomD50);
                    Thread.Sleep(2000);
                    break;

            }
        }
    }

    private static void Fighting(Player player, Enemy opp)
    {
        Random randomAttackDie = new Random();
        string option = "";
        bool defendActive = false;
        int roundCounter = 0;

        while (opp.Health > 0 && player.Health > 0)
        {
            roundCounter++;
            Console.WriteLine($"ROUND {roundCounter}");
            Console.WriteLine("[attack/defend]");

            while (true)
            {
                option = Console.ReadLine().ToLower();

                if (option == "attack" || option == "defend")
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


            if (opp.Health > 0)
            {
                int defensiveDamageTaken = opp.Attack / 2;
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



