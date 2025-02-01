using System;
using System.Collections.Generic;

class InventoryManagementSystem
{
    // Encapsulated item information data
    class Item
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public uint Quantity { get; set; }

        public Item(string name, float price, uint quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        // Overriden Equals and GetHashCode methods for accurate checking of equality
        public override bool Equals(object? obj)
        {
            if (obj is Item other)
            {
                return Name == other.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Price, Quantity);
        }
    }

    // Inventory items as a list of items
    private static readonly List<Item> items = new List<Item>();

    public static void Main(string[] args)
    {
        while (true)
        {
            // Prompt the user to select an option
            Console.WriteLine("Inventory Manager");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Sell Item");
            Console.WriteLine("3. Remove Item");
            Console.WriteLine("4. Update Item");
            Console.WriteLine("5. View Items");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Select option: (choose 1-6)");


            /* 
            IF input is valid THEN
                IF selection is valid THEN
                    Execute selected process
                ELSE
                    Print "Invalid Selection"
            ELSE 
                Print "Invalid input"
            */
            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        AddItem();
                        ViewItems();
                        break;
                    case 2:
                        ViewItems();
                        SellItem();
                        ViewItems();
                        break;
                    case 3:
                        ViewItems();
                        RemoveItem();
                        ViewItems();
                        break;
                    case 4:
                        ViewItems();
                        UpdateItem();
                        ViewItems();
                        break;
                    case 5:
                        ViewItems();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }

    private static void AddItem()
    {
        // Prompt user to enter a valid item name
        string itemName = GetValidItemName();

        // Prompt user to enter a valid item quantity
        uint itemQuantity = GetValidItemQuantity();

        // Find an existing item with the same name
        Item? existingItem = items.Find(item => item.Name == itemName);
        if (existingItem != null)
        {
            // Increment the quantity of the existing item
            existingItem.Quantity += itemQuantity;
            Console.WriteLine($"{itemQuantity} new {itemName} added to inventory!");
            return;
        }

        // Prompt user to enter a valid item price
        float itemPrice = GetValidItemPrice();

        // Finalize adding new item to the inventory
        items.Add(new Item(itemName, itemPrice, itemQuantity));
        Console.WriteLine("New item added!");
    }

    private static void SellItem()
    {
        if (items.Count == 0)
        {
            return;
        }

        // Prompt user to enter item number of item to sell
        Console.WriteLine($"Select item to sell: (choose 1-{items.Count})");
        if (int.TryParse(Console.ReadLine(), out int itemIndex))
        {
            if (itemIndex <= 0 || itemIndex > items.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Item itemForSale = items[itemIndex - 1];
            if (itemForSale.Quantity == 0)
            {
                Console.WriteLine($"There's no more {itemForSale.Name} left to sell.");
                return;
            }

            uint itemQuantity = GetValidItemQuantity();

            if (itemQuantity > itemForSale.Quantity)
            {
                Console.WriteLine($"There's not enough {itemForSale.Name} in stock.");
                return;
            }

            itemForSale.Quantity -= itemQuantity;
            Console.WriteLine($"Sold {itemQuantity} {itemForSale.Name}.");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    private static void RemoveItem()
    {
        if (items.Count == 0)
        {
            return;
        }

        // Prompt user to enter item number of item to remove
        Console.WriteLine($"Select item to remove: (choose 1-{items.Count})");
        if (int.TryParse(Console.ReadLine(), out int itemIndex))
        {
            if (itemIndex <= 0 || itemIndex > items.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            int adjustedItemIndex = itemIndex - 1;
            Item itemToRemove = items[adjustedItemIndex];
            items.RemoveAt(adjustedItemIndex);
            Console.WriteLine($"Removed {itemToRemove.Name} from inventory.");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    private static void UpdateItem()
    {
        if (items.Count == 0)
        {
            return;
        }

        // Prompt user to enter item number of item to update
        Console.WriteLine($"Select item to update: (choose 1-{items.Count})");
        if (int.TryParse(Console.ReadLine(), out int itemIndex))
        {
            if (itemIndex <= 0 || itemIndex > items.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            while (true)
            {
                Console.WriteLine("What do you want to update? ");
                Console.WriteLine("1. Name | 2. Price | 3. Quantity");
                Console.WriteLine("Select option: (choose 1-3)");

                if (int.TryParse(Console.ReadLine(), out int attributeIndex))
                {
                    switch (attributeIndex)
                    {
                        case 1:
                            UpdateItemName(itemIndex - 1);
                            return;
                        case 2:
                            UpdateItemPrice(itemIndex - 1);
                            return;
                        case 3:
                            UpdateItemQuantity(itemIndex - 1);
                            return;
                        default:
                            Console.WriteLine("Invalid selection.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        #region Local Functions
        static void UpdateItemName(int itemIndex)
        {
            Item itemToUpdate = items[itemIndex];
            string oldItemName = itemToUpdate.Name;
            string newItemName = GetValidItemName();
            items[itemIndex].Name = newItemName;
            Console.WriteLine($"Changed {oldItemName} name to {newItemName}");
        }

        static void UpdateItemPrice(int itemIndex)
        {
            Item itemToUpdate = items[itemIndex];
            float newItemPrice = GetValidItemPrice();
            items[itemIndex].Price = newItemPrice;
            Console.WriteLine($"Changed {itemToUpdate.Name} price to ${newItemPrice:F2}");
        }

        static void UpdateItemQuantity(int itemIndex)
        {
            Item itemToUpdate = items[itemIndex];
            uint newItemQuantity = GetValidItemQuantity();
            items[itemIndex].Quantity = newItemQuantity;
            Console.WriteLine($"Changed {itemToUpdate.Name} quantity to {newItemQuantity}");
        }
        #endregion
    }

    private static string GetValidItemName()
    {
        while (true)
        {
            Console.WriteLine("Enter item name: ");
            string itemName = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrEmpty(itemName))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }
            return itemName;
        }
    }

    private static uint GetValidItemQuantity()
    {
        while (true)
        {
            Console.WriteLine("Enter item quantity: ");
            if (uint.TryParse(Console.ReadLine(), out uint itemQuantity))
            {
                if (itemQuantity == 0)
                {
                    Console.WriteLine("Quantity cannot be zero.");
                    continue;
                }
                else
                {
                    return itemQuantity;
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }

    private static float GetValidItemPrice()
    {
        while (true)
        {
            Console.WriteLine("Enter item price: ");
            if (float.TryParse(Console.ReadLine(), out float itemPrice))
            {
                if (itemPrice < 0f)
                {
                    Console.WriteLine("Price cannot be negative.");
                    continue;
                }
                else
                {
                    return itemPrice;
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }

    private static void ViewItems()
    {
        Console.WriteLine("Current Inventory");
        if (items.Count > 0)
        {
            // Display information of all existing items
            int itemIndex = 1;
            foreach (Item item in items)
            {
                Console.WriteLine($"{itemIndex}. {item.Name} | price: ${item.Price:F2} | quantity: {item.Quantity}");
                itemIndex++;
            }
        }
        else
        {
            Console.WriteLine("Nothing in stock.");
        }
    }
}
