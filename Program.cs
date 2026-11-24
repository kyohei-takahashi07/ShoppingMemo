using MySqlConnector;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();




var connectionString = app.Configuration.GetConnectionString("ShoppingMemo");

using (var connection = new MySqlConnection(connectionString))
{
    connection.Open();

    var sql = @"
        CREATE TABLE IF NOT EXISTS ShoppingItems (
            Id INT AUTO_INCREMENT PRIMARY KEY,
            Name TEXT NOT NULL
        );
    ";

    using var command = new MySqlCommand(sql, connection);
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
