namespace RPGTextGame;

public class Characters
{
    public virtual string Class { get; set; }
    public virtual double Health {get; set;}
    public virtual int Attack {get; set;}
    public virtual int Defence {get; set;}
    public virtual int Intelligence {get; set;}
    public virtual int DodgeChance {get; set;}
    
    public int GetDamage(Characters enemy, bool isDefensiveStance)
    {
        float attack = 0;
        Random randomAttackDie = new Random();
        if (enemy.Class != "mage")
        {
            attack = randomAttackDie.Next(2, 20);
        }
        else
        {
            attack = randomAttackDie.Next(2, 9);
        }
        
        attack /= 10;
        int damage = Convert.ToInt32(enemy.Attack * attack);
        if (isDefensiveStance)
        {
            damage /= 2;
        }
        if (damage > Defence)
        {
            Health -= Math.Max(0,  damage - Defence);
        }
        Thread.Sleep(2000);
        
        return Math.Max(0,  damage - Defence);
    }
}