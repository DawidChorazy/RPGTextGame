namespace RPGTextGame;

public class Items
{
    public string ItemName {get; set;}
    public string ItemDescription {get; set;}
    public string ItemType { get; set; }
    public int Attack;
    public int Defense;
    public int HealthRecovery;
    public int ManaRecovery;

    public static Dictionary<int, Items> items = new Dictionary<int, Items>
    {
        { 1, new Items(2, 0, 0, 0,"Wooden Sword", "Weapon") },
        { 2, new Items(0, 2, 0,0,"Wooden Shield", "Offhand") },
        { 3, new Items(0, 0, 20,0,"Small Potion of Health", "Usable") },
        { 4, new Items(0, 0, 0,50,"Small Potion of Mana", "Usable") },
        { 5, new Items(1, 0, 0,0,"Small Potion of Strength", "Usable") },
        { 6, new Items(0,2,0,0,"Leather armor", "Chestplate")},
        { 7, new Items(0,1,0,0,"Leather cap", "Helmet")},
        { 8, new Items(0,1,0,0,"Leather leggings", "Legs")},
        { 9, new Items(0,0,0,25,"Wooden Staff", "Weapon")},
        { 10, new Items(0,0,0,0,"Cursed key", "Unique")}
    };
    

    public Items(int attack, int defense, int healthRecovery, int manaRecovery , string itemName, string itemType)
    {
        ItemName = itemName;
        ItemDescription = "";
        ItemType = itemType;
        Attack = attack;
        Defense = defense;
        HealthRecovery = healthRecovery;
        ManaRecovery = manaRecovery;
    }
}