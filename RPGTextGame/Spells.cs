namespace RPGTextGame;

public class Spells
{
    public string SpellName {get; set;}
    public string ItemDescription {get; set;}
    public string ItemType {get; set;}
    public int Attack;
    public int Defense;
    public int HealthRecovery;
    public int ManaRecovery;
    public int ManaDrain;

    public static Dictionary<int, Spells> spells = new Dictionary<int, Spells>
    {
        { 1, new Spells(0,15, 0, 0, 0,"Sleeping Fart", "Active","Set sleep on ememy for 2 turns" ) },
        { 2, new Spells(25,15, 0, 0,0,"Fireball", "Active", "Unassigned") }, //dodaj info
        { 3, new Spells(0,0, 0, 15,10,"Minor Recovery", "Passive", "Unassigned" ) }, //dodaj info
    };
    

    public Spells(int attack,int manaDrain, int defense, int healthRecovery, int manaRecovery , string spellName, string itemType, string itemDescription)
    {
        ManaDrain = manaDrain;
        ItemType = itemType;
        SpellName = spellName;
        ItemDescription = "";
        Attack = attack;
        Defense = defense;
        HealthRecovery = healthRecovery;
        ManaRecovery = manaRecovery;
        ItemDescription = itemDescription;
    }
}