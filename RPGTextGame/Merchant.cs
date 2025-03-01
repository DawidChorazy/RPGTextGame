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
            ShopItems = shopItems.ToDictionary(x => x.Key.ToLower(), x => x.Value);

            Attack = 60;
            Health = 300;

        }

        public static void Buying(Player player, Merchant foundMerchant)
        {
            Console.WriteLine("Do you want to buy something? [trade/exit]");
            while (true)
            {
                string option = Console.ReadLine().ToLower();
                if (option.ToLower() == "trade")
                {
                    while (true)
                    {
                        foreach (var item in foundMerchant.ShopItems)
                        {
                            Console.WriteLine($"{item.Key} - {item.Value} coins");
                        }

                        Console.WriteLine("Type in what do you want to buy or type 'exit' to leave");
                        string buyOption = Console.ReadLine();
              
                            if (buyOption.ToLower() == "exit")
                            {
                                return;
                            }
                            if (foundMerchant.ShopItems.TryGetValue(buyOption, out int price))
                            {
                                if (player.Coins >= price)
                                {
                                    player.Coins -= price;
                                    Console.WriteLine($"You bought a {buyOption}! It has been added to your inventory.");
                                    player.Inventory.Add(buyOption);
                                }
                                else
                                {
                                    Console.WriteLine("You don't have enough coins to buy this item.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid item. Please choose from the list.");
                            }

                            Console.WriteLine("Do you want to buy another item? [trade/exit]");
                            string continueBuying = Console.ReadLine().ToLower();
                            if (continueBuying == "trade")
                            {
                                Buying(player, foundMerchant);
                            }

                            break;
                            
                    }
                }

                else if (option == "exit")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Please enter a valid option");
                }
            }
        }
        public static Merchant GetRandomMerchant()
        {
            Random random = new Random();
            int randomMerchantId = merchants.Keys.ElementAt(random.Next(merchants.Count));
            return merchants[randomMerchantId];
        }
        
}