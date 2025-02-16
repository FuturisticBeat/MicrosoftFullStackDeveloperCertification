using CRUDWithMySQL.Models;

// Create
using (ApplicationDbContext context = new())
{
    Product newProduct = new Product { Name = "Laptop", Price = 1200.99m };
    context.Products.Add(newProduct);
    context.SaveChanges();

// Read
    List<Product> allProducts = context.Products.ToList();
    Console.WriteLine("All Products:");
    allProducts.ForEach(p => Console.WriteLine($"{p.Id} - {p.Name} - {p.Price}"));

    Product? singleProduct = context.Products.Find(1);
    if (singleProduct == null)
    {
        return;
    }

    Console.WriteLine("Product Found:");
    Console.WriteLine($"{singleProduct.Id} - {singleProduct.Name} - {singleProduct.Price}");

// Update
    singleProduct.Price = 1100.99m;
    context.SaveChanges();

// Delete
    context.Products.Remove(singleProduct);
    context.SaveChanges();
}