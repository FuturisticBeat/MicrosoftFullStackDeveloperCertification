namespace RecipeManager.Data
{
    /// <summary>
    /// Represents a recipe with an ID, name, and description.
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Dummy data for recipes. TODO: Replace with data downloaded from the database.
        /// </summary>
        public static List<Recipe> DummyRecipes = new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Spaghetti Bolognese", Description = "A classic Italian pasta dish with rich meat sauce." },
            new Recipe { Id = 2, Name = "Chicken Curry", Description = "A spicy and flavorful chicken curry with coconut milk." },
            new Recipe { Id = 3, Name = "Vegetable Stir Fry", Description = "A quick and healthy stir fry with mixed vegetables." },
            new Recipe { Id = 4, Name = "Beef Tacos", Description = "Mexican-style beef tacos with fresh toppings." },
            new Recipe { Id = 5, Name = "Caesar Salad", Description = "A fresh salad with romaine lettuce, croutons, and Caesar dressing." }
        };
        
        /// <summary>
        /// Gets or sets the unique identifier for the recipe.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the recipe.
        /// </summary>
        public string? Name { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the recipe.
        /// </summary>
        public string? Description { get; set; }
    }
}