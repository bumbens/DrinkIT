using SQLite;

namespace DrinkIT.Models;

[Table("Ingredients")]
public class Ingredient
{
    [PrimaryKey, AutoIncrement]
    public int Id {get; set; }
    public string? Name { get; set; }
    public bool IsAvailable { get; set; }
}