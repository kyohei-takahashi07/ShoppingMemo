var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();
SQLitePCL.Batteries_V2.Init();

var databasePath = Path.Combine(app.Environment.ContentRootPath, "shopping.db");
using (var connection = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={databasePath}"))
{
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = """
        CREATE TABLE IF NOT EXISTS ShoppingItems (
            Id INTEGER PRIMARY KEY,
            Name TEXT NOT NULL
        );
        """;
    command.ExecuteNonQuery();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
