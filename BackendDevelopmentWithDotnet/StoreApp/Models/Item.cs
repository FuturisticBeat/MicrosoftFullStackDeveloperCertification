namespace StoreApp.Models
{
    public class Item
    {
        public required string Name { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            return obj is Item otherItem &&
                   Name == otherItem.Name &&
                   Description == otherItem.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }
    }
}
