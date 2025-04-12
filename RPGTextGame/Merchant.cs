namespace RPGTextGame;

public class Merchant: Characters
{
    public static Dictionary<int, Merchant> merchants = new Dictionary<int, Merchant>
    {
        {
            1, new Merchant("Jonathan The Alchemist", new Dictionary<string, int>
            {
                { "Small Potion Of Health", 50 },
                { "Small Potion Of Strength", 75 }
            })
                },
        {
            2, new Merchant("Eboron The Great Smith", new Dictionary<string, int>
            {
                {"Wooden Sword", 100},
                {"Wooden Shield", 80},
                {"Leather armor", 120},
                {"Leather cap", 80},
                {"Leather leggings", 100}
            })
        }
        //TODO add cursed key merchant
    };
        
        public string Name { get; set; }
        public override int Attack { get; set; }
        public override double Health { get; set; }
        
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
                        Console.WriteLine("---------------------");
                        foreach (var item in foundMerchant.ShopItems)
                        {
                            Console.WriteLine($"|{item.Key} - {item.Value} coins|");
                        }
                        Console.WriteLine("---------------------");

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
                                    Console.WriteLine($"You bought a {buyOption}! It has been added to your inventory. You have {player.Coins } coins left.");
                                    player.Inventory.Add(buyOption);
                                }
                                else
                                {
                                    Console.WriteLine($"You don't have enough coins to buy this item. You have {player.Coins } coins.");
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

                            else if (continueBuying == "exit")
                            {
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Please enter a valid option");
                            }
                            
                    }
                }
                else if (option.ToLower() == "exit")
                {
                    break;
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