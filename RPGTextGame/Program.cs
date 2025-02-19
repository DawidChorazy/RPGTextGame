using System.Globalization;

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
        return new Player(name, className, race, 100, 10);
    }

    public static void RandomEventGenerator(Player player)
    {
        Dictionary<int, Enemy> opponents = new Dictionary<int, Enemy>
        {
            { 1, new Enemy("Goblin", 20, 4)},
            { 2, new Enemy("Chupacabra", 12, 7) },
            { 3, new Enemy("Wolf", 10, 10) },
            { 4, new Enemy("Ghost", 33, 2) },
            { 5, new Enemy("Wild Boar", 28, 2) }
        };
        
        Random randomOpponentOutput = new Random();
        Random randomDiceOutput = new Random();
        Random randomCrossroadOutput = new Random();

        string option = "";
        int randomCrossroad = randomCrossroadOutput.Next(1, 4);
        int die = randomDiceOutput.Next(1, 21);
        int enemyIndex = randomOpponentOutput.Next(1, 6);
        Enemy enemy = opponents[enemyIndex];
        
    if(player.Health > 0)
    {
        switch (die)
        {
            case 1:
                Console.WriteLine("Critical failure! You stepped into the trap!");
                player.Health -= die;
                Console.WriteLine($"You lost {die} of health points. Your current health is {player.Health - die}");
                Thread.Sleep(2000);
             break;
            
            case 2:
            case 3:
            case 4:
                Console.WriteLine("You found the enemy. You cannot run away nor hide.");
                Thread.Sleep(2000);// only fight option
                Console.WriteLine($"You are fighting {enemy.Name} with power of {enemy.Attack} and {enemy.Health} health!");
                Thread.Sleep(2000);
                Fighting(player, enemy);
                break;
            
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                Console.WriteLine("Nothing Happens"); // skippin
                break;
            case 11:
            case 12:
            case 13:
                Console.WriteLine("Crossroads! Choose to go left or right:");
                string cross = Console.ReadLine().ToLower();
                if (randomCrossroad == 1)
                {
                    player.Health -= die;
                    Console.WriteLine($"You lost {die} health points due to stepping into trap! Your current health is {player.Health}");
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
            case 16:
            case 17:
                Console.WriteLine("You found enemy (You can fight or flee)"); // attack or flee
                option = Console.ReadLine().ToLower();
                if (option == "fight")
                {
                    Fighting(player, enemy);
                }
                else if (option == "flee")
                {
                    Flee(player, enemy);
                }
                Thread.Sleep(2000);
                break;
            
            case 18:
            case 19:
                Console.WriteLine("You found supplies");// default supplies
                Thread.Sleep(2000);
                break;
            
            case 20:
                Console.WriteLine("You found treasure");// better supplies
                Thread.Sleep(2000);
                break;
            
        }
    }
    }

    public static void Fighting(Player player, Enemy opp)
    {
        Random randomAttackDie = new Random();
        
        while (opp.Health > 0 && player.Health > 0)
        {
            if (player.Health > 0 && opp.Health > 0)
            {
                int attack = randomAttackDie.Next(1, 21);
                opp.Health -= attack;
                Console.WriteLine(
                    $"You attacked the beast for {attack} health points! Remaining health is {opp.Health} ");
                Thread.Sleep(2000);
                if (opp.Health > 0)
                {
                    player.Health -= opp.Attack;
                    Console.WriteLine($"You got hit for {opp.Attack}. You have {player.Health} remaining life points");
                    Thread.Sleep(2000);
                }
                else if (opp.Health <= 0)
                {
                    Console.WriteLine("You bravely defeated monster");
                }
            }
            else if (player.Health <= 0)
            {
                Console.WriteLine("Unexpectedly your opponent gives you a kiss of sudden death");
                Environment.Exit(0);
            }
            else if (opp.Health <= 0)
            {
                Console.WriteLine("You slained your opponent");
            }
        }
    }

    public static void Flee(Player player, Enemy opp)
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
}

public class Player
{
    public string Name{ get; set; }
    public string Class {get; set;}
    public string Race {get; set;}

    public int Health { get; set; } = 100;

    public int Attack { get; set; }

    public Player(string name, string className, string race, int health, int attack)
    {
        Name = name;
        Class = className;
        Race = race;
        Health = health;
        Attack = attack;
    }
}

public class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack  {get; set;}

    public Enemy(string name, int health, int attack)
    {
        Name = name;
        Health = health;
        Attack = attack;
    }
}


