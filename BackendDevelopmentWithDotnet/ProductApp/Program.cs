using Newtonsoft.Json;
using System;

public class Product
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public List<string>? Tags { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        string json = "{\"Name\":\"Apple\",\"Price\":1.20,\"Tags\":[\"Fruit\",\"Healthy\"]}";

        Product? product = JsonConvert.DeserializeObject<Product>(json);

        Console.WriteLine($"Name: {product?.Name}, Price: {product?.Price}");

        //Product product = new Product
        //{
        //    Name = "Apple",
        //    Price = 1.20m,
        //    Tags = new List<string> { "Fruit", "Healthy" }
        //};

        //string json = JsonConvert.SerializeObject(product, Formatting.Indented);

        //Console.WriteLine(json);
    }
}