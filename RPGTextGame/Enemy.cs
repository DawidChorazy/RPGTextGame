namespace RPGTextGame;

public class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }

    public int ExperienceAfterDefeated { get; set; }

    public Enemy(string name, int health, int attack, int experienceAfterDefeated)
    {
        Name = name;
        Health = health;
        Attack = attack;
        ExperienceAfterDefeated = experienceAfterDefeated;
    }
}