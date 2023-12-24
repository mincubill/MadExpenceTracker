using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Services;
using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IExpencePersistence, ExpencesPersistence>(o =>
{
    Connection mongoConnection = new Connection();
    MongoClient mongoClient = mongoConnection.GetClient();
    IMongoDatabase mongoDatabase = mongoConnection.GetDatabase(mongoClient);
    return new ExpencesPersistence(mongoDatabase);
});

builder.Services.AddSingleton<IIncomePersistence, IncomePersistence>(o =>
{
    Connection mongoConnection = new Connection();
    MongoClient mongoClient = mongoConnection.GetClient();
    IMongoDatabase mongoDatabase = mongoConnection.GetDatabase(mongoClient);
    return new IncomePersistence(mongoDatabase);
});

builder.Services.AddSingleton<IAmountsPersistence, AmountsPersistence>(o =>
{
    Connection mongoConnection = new Connection();
    MongoClient mongoClient = mongoConnection.GetClient();
    IMongoDatabase mongoDatabase = mongoConnection.GetDatabase(mongoClient);
    return new AmountsPersistence(mongoDatabase);
});

builder.Services.AddScoped<IExpencesService, ExpencesService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IAmountsService, AmountsService>();




var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
