using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Managers;
using RestaurantReservatie.DL.Repositories;
using RestaurantReservatie.Rest.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
string connectionString = "Data Source=.\\KUTDATABASE;Initial Catalog=ReservatieBeheer;Integrated Security=True;Trust Server Certificate=True";

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IRestaurantRepository>(r => new RestaurantRepository(connectionString));
builder.Services.AddSingleton<RestaurantManager>();
builder.Services.AddSingleton<IReservationRepository>(r => new ReservationRepository(connectionString));
builder.Services.AddSingleton<ReservationManager>();

builder.Services.AddSingleton<ICustomerRepository>(r => new CustomerRepository(connectionString));
builder.Services.AddSingleton<CustomerManager>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

ILogger logger = builder.Logging.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseLogURLMiddleware();

}


app.UseAuthorization();

app.MapControllers();

app.Run();