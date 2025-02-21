namespace RPGTextGame;

public class Merchant: Characters
{
    public static Dictionary<int, Merchant> merchants = new Dictionary<int, Merchant>
    {
        {
            1, new Merchant("Jonathan The Alchemist", new Dictionary<string, int>
            {
                { "Potion Of Health", 50 },
                { "Potion Of Strength", 75 }
            })
                },
        {
            2, new Merchant("Eboron The Great Smith", new Dictionary<string, int>
            {
                {"Sword", 100},
                {"Shield", 80},
            })
        }
    };
        
        public string Name { get; set; }
        public override int Attack { get; set; }
        public override int Health { get; set; }
        
        public Dictionary<string,int> ShopItems { get; set; }
        

        public Merchant(string name, Dictionary<string,int> shopItems)
        {
            Name = name;
            ShopItems = shopItems;

            Attack = 60;
            Health = 300;

        }

        public static void Buying(Player player)
        {
            Random random = new Random();
            string option = Console.ReadLine().ToLower();
            int randomMerchantId = merchants.Keys.ElementAt(random.Next(0, merchants.Count));
            Merchant foundMerchant = merchants[randomMerchantId];
            
            Console.WriteLine("Type in 'trade' to trade or 'exit' to move on.");
            while (true)
            {
                if (option == "trade")
                {
                    foreach (var item in foundMerchant.ShopItems)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Type in what do you want to buy:");
                    string buyOption = Console.ReadLine().ToLower();
                    for (int i = 0; i < foundMerchant.ShopItems.Count; i++)
                    {
                        if (buyOption == foundMerchant.ShopItems.ElementAt(i).Key)
                        {
                            Console.WriteLine($"An {foundMerchant.ShopItems.ElementAt(i).Key} is added to your inventory.");
                            break;
                        }
                    }
                }
                else if (option == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid option");
                }
            }
        }
        
}