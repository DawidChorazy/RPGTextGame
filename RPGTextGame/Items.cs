namespace RPGTextGame;

public class Items
{
    public string ItemName;
    public string ItemDescription;
    public int Attack;
    public int Defense;

    public static Dictionary<int, Items> items = new Dictionary<int, Items>
    {
        { 1, new Items(4, 0, "Sword") },
        { 2, new Items(0, 4, "Shield") },
        { 3, new Items(0, 0, "Potion of Health") },
        { 4, new Items(0, 0, "Potion of Strength") },
    };

    public Items(int attack, int defense, string itemName)
    {
        ItemName = itemName;
        ItemDescription = "";
        Attack = attack;
        Defense = defense;
    }
}