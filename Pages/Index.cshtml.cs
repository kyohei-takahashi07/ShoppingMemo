using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingMemo.Pages;

public class IndexModel : PageModel
{
public record ShoppingItem(long Id, string Name);
public List<ShoppingItem> Items { get; } = new();    
    public string? ReceivedItem { get; private set; }

public void OnPost(string? itemName)
{
    if (string.IsNullOrWhiteSpace(itemName))
    {
        ReceivedItem = "買うものを入力してください。";
        return;
    }

    var environment = HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
    var databasePath = Path.Combine(environment.ContentRootPath, "shopping.db");

    using var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={databasePath}");
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "INSERT INTO ShoppingItems (Name) VALUES ($name)";
    command.Parameters.AddWithValue("$name", itemName.Trim());
    command.ExecuteNonQuery();

    ReceivedItem = $"{itemName.Trim()}を保存しました。";
    LoadItems();
}
    public void OnGet()
   
{
    LoadItems();
}
public IActionResult OnPostDelete(long id)
{
    var environment = HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
    var databasePath = Path.Combine(environment.ContentRootPath, "shopping.db");

    using var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={databasePath}");
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "DELETE FROM ShoppingItems WHERE Id = $id";
    command.Parameters.AddWithValue("$id", id);
    command.ExecuteNonQuery();

    return RedirectToPage();
}public IActionResult OnPostUpdate(long id, string? itemName)
{
    if (string.IsNullOrWhiteSpace(itemName))
    {
        ReceivedItem = "買うものを入力してください。";
        LoadItems();
        return Page();
    }

    var environment = HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
    var databasePath = Path.Combine(environment.ContentRootPath, "shopping.db");

    using var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={databasePath}");
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "UPDATE ShoppingItems SET Name = $name WHERE Id = $id";
    command.Parameters.AddWithValue("$name", itemName.Trim());
    command.Parameters.AddWithValue("$id", id);
    command.ExecuteNonQuery();

    return RedirectToPage();
}

private void LoadItems()
{
    var environment = HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
    var databasePath = Path.Combine(environment.ContentRootPath, "shopping.db");

    using var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={databasePath}");
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "SELECT Id, Name FROM ShoppingItems ORDER BY Id";

    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        Items.Add(new ShoppingItem(reader.GetInt64(0), reader.GetString(1)));
    }
}
}
