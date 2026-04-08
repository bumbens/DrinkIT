using SQLite;
using DrinkIT.Models;

namespace DrinkIT.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection _database;


    public async Task Init()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "drinkit.db");

        if (_database != null) return;

        _database = new SQLiteAsyncConnection(dbPath);
        await _database.CreateTableAsync<Ingredient>();
    }
    
    public async Task<List<Ingredient>> GetIngredientsAsync()
    {
        await Init();
        return await _database.Table<Ingredient>().ToListAsync();
    }

    public async Task AddIngredientsAsync(Ingredient ingredient)
    {
        await Init();
        await _database.InsertAsync(ingredient);
    }

    public async Task RemoveIngredientsAsync(Ingredient ingredient)
    {
        await Init();
        await _database.DeleteAsync(ingredient);
    }

    public async Task ClearAllIngredientsAsync()
    {
        await Init();
        await _database.DeleteAllAsync<Ingredient>();
    }
}