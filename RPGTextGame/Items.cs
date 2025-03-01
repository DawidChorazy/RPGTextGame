namespace RPGTextGame;

public class Items
{
    public string ItemName;
    public string ItemDescription;
    public string ItemType;
    public int Attack;
    public int Defense;

    public static Dictionary<int, Items> items = new Dictionary<int, Items>
    {
        { 1, new Items(4, 0, "Sword", "Weapon") },
        { 2, new Items(0, 4, "Shield", "Offhand") },
        { 3, new Items(0, 0, "Potion of Health", "Usable") },
        { 4, new Items(0, 0, "Potion of Strength", "Usable") },
        {5, new Items(0,4,"Leather armor", "Chestplate")},
        {6, new Items(0,2,"Leather cap", "Helmet")},
        {7, new Items(0,3,"Leather leggings", "Legs")},
    };

    public Items(int attack, int defense, string itemName, string itemType)
    {
        ItemName = itemName;
        ItemDescription = "";
        ItemType = itemType;
        Attack = attack;
        Defense = defense;
    }
}