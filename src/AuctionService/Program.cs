using AuctionService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add MVC Controllers to the application's services to respond to HTTP requests with controller classes
builder.Services.AddControllers();

// Configure Entity Framework DbContext for the application, specifying PostgreSQL as the database using the connection string from app settings
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add AutoMapper to the services collection and scan the current domain's assemblies for mapping profiles
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Enable authorization middleware, which checks for permissions on requests
app.UseAuthorization();

// Map routes to controller actions, telling the application how to route HTTP requests to the appropriate controller actions
app.MapControllers();

try
{
    // Initialize the database using a custom method, applying any pending migrations and seeding the database with initial data
    DBInitializer.InitDb(app);
}
catch (Exception e)
{
    // Catch and log any exceptions during database initialization to the console
    Console.WriteLine(e);
}

// Run the application, start listening for incoming HTTP requests and handle them as configured
app.Run();
