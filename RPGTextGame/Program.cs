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

                Console.WriteLine($"You lost {die} of health points. Your current health is {player.Health - die}");
             break; 
            case 2:
            case 3:
            case 4:
                Console.WriteLine("You found the enemy (You cannot run away nor hide)"); // only fight option
                Console.WriteLine($"You are fighting {enemy.Name} with power of {enemy.Attack} and {enemy.Health} health!");
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
                string cross = Console.ReadLine();
                if (randomCrossroad == 1)
                {
                    Console.WriteLine("You lost health points due to stepping into trap!");
                    break;
                }
                else
                {
                    Console.WriteLine("Nothing Happens");
                    break;
                }
            case 14:
            case 15:
            case 16:
            case 17:
                Console.WriteLine("You found enemy (You can attack or flee)"); // attacking enemy or fleeing
                break;
            case 18:
            case 19:
                Console.WriteLine("You found supplies"); // default supplies
                break;
            case 20:
                Console.WriteLine("You found treasure"); // better supplies
                break; 
        }
    }
    }

    public static void Fighting(Player player, Enemy opp)
    {
        Random randomAttackDie = new Random();
        int attack = randomAttackDie.Next(1, 21);
        string option = "";
        Console.WriteLine("Choose one:");
        Console.WriteLine("Attack");
        Console.WriteLine("Flee");
        if (option == "Attack")
        {
            if (player.Health > 0 && opp.Health > 0)
            {
                opp.Health -= attack;
                player.Health -= opp.Attack;
            }
            else if (player.Health <= 0)
            {
                Console.WriteLine("Unexpectedly your opponent gives you a kiss of sudden death");
            }
            else if (opp.Health <= 0)
            {
                Console.WriteLine("You slained your opponent");
            }
        }
        // add flee mechanic
        
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


