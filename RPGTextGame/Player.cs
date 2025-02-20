namespace RPGTextGame;

public class Player
{
    public string Name { get; set; }
    public string Class { get; set; }
    public string Race { get; set; }

    public int Health { get; set; } = 100;

    public int Attack { get; set; }

    public int Defense { get; set; }
    public int Intelligence { get; set; }

    public int Experience { get; set; } = 0;
    public int Level { get; set; } = 1;

    public Player(string name, string className, string race, int health, int attack, int experience, int level,
        int intelligence, int defense)
    {
        Name = name;
        Class = className;
        Race = race;
        Health = health;
        Attack = attack;
        Experience = experience;
        Level = level;
        Intelligence = intelligence;
        Defense = defense;

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

    public static void PlayerAttributes(Player player)
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
                    player.Attack += 2;
                    Console.WriteLine($"Attack has been improved! Your current attack {player.Attack}");
                    break;
                }
                else if (option == "defense")
                {
                    player.Defense += 2;
                    Console.WriteLine($"Defense has been improved! Your current defense {player.Defense}");
                    break;
                }
                else if (option == "health")
                {
                    player.Health += 10;
                    Console.WriteLine($"Health has been improved! Your current health {player.Health}"); //TODO need to cap Health to 100 to prevent overhealing
                    break;
                }
                else if (option == "intelligence")
                {
                    player.Intelligence += 2;
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
}