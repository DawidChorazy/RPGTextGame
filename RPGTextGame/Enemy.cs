namespace RPGTextGame;

public class Enemy: Characters
{
    public static Dictionary<int, (string,int,int,int,int)> opponents = new Dictionary<int, (string,int, int, int, int)>
    {
        { 1, ("Goblin", 20, 10,10 , 25) },
        { 2, ("Chupacabra", 12, 9, 9, 24) }, //TODO EXP TO CHANGE, SHOULDNT BE 100
        { 3, ("Wolf", 10, 12, 12, 23) }, // If higher level, higher difficulty
        { 4, ("Ghost", 33, 5, 5, 22) },
        { 5, ("Wild Boar", 28, 7, 7, 21) },
    };

    public static Dictionary<int, (string, int, int, int, int)> bosses = new Dictionary<int, (string, int, int, int, int)>
        {
            {1, ("Void bear", 77, 11, 34, 60) },
            {2, ("Void driad", 44, 22, 26, 60)}
        };
    public string Name { get; set; }
    public override double Health { get; set; }
    public override int Attack { get; set; }
    
    private int MinAttack { get; set; }
    private int MaxAttack { get; set; }
    public int ExperienceAfterDefeated { get; set; }
    
    public int CoinsAfterDefeated { get; set; }
    
    public Enemy(string name, int health, int minAttack, int maxAttack, int experienceAfterDefeated)
    {
        Random random = new Random();
        int randomizedAttack = random.Next(minAttack, maxAttack);
        int randomizedCoinsAfterDefeated = random.Next(1, 6);
        
        Name = name;
        Health = health;
        Attack = randomizedAttack;
        ExperienceAfterDefeated = experienceAfterDefeated;
        CoinsAfterDefeated = randomizedCoinsAfterDefeated;
    }

    public static Enemy GetEnemy(int id)
    {
        if (opponents.ContainsKey(id))
        {
            var data = opponents[id];
            return new Enemy(data.Item1, data.Item2, data.Item3, data.Item4, data.Item5);
        }
        return null; 
    }
    
    public static Enemy GetBoss(int id)
    {
        if (bosses.ContainsKey(id))
        {
            var data = bosses[id];
            return new Enemy(data.Item1, data.Item2, data.Item3, data.Item4, data.Item5);
        }
        return null; 
    }

    
}
