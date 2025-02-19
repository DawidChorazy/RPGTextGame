public class Program
{
    public static void Main(string[] args)
    {
        StartOfGame();
        RandomEventGenerator();
    }
    public static void StartOfGame()
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
        
    }

    public static void RandomEventGenerator()
    {
        Dictionary<int, string> opponents = new Dictionary<int, string>
        {
            { 1, "Goblin" },
            { 2, "Chupacabra" },
            { 3, "Wolf" },
            { 4, "Ghost" },
            { 5, "Wild boar" }
        };
        
        Random randomOpponentOutput = new Random();
        Random randomDiceOutput = new Random();
        Random randomCrossroadOutput = new Random();
        
        int randomCrossroad = randomCrossroadOutput.Next(1, 4);
        int die = randomDiceOutput.Next(1, 21);
        int opponent = randomOpponentOutput.Next(opponents.Count + 1);

        switch (die)
        {
            case 1:
                Console.WriteLine("Critical failure! You stepped into the trap!"); // losing hp
                break;
            case 2:
            case 3:
            case 4:
                Console.WriteLine("You found the enemy (You cannot run away nor hide)"); // only fight option
                Console.WriteLine($"You are fighting {opponent}!");
                break;
            case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                Console.WriteLine("Nothing Happens"); // skipping
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

    public static void Fighting()
    {
        
    }
}

public class Player
{
    public string Name{ get; set; }
    public string Class {get; set;}
    public string Race {get; set;}

    public Player(string name, string className, string race)
    {
        Name = name;
        Class = className;
        Race = race;
    }
}


