using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Interfaces.UseCase;
using MadExpenceTracker.Core.Interfaces.Utils;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Services;
using MadExpenceTracker.Core.UseCase;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MadExpenceTracker.Persistence.MongoDB.Util;
using MadExpenceTracker.Server.Util;

IMongoDBProvider mongoProvider = new MongoDBProvider("mongodb://localhost:27017", "MadExpencesTracker");
IDbInitialization dbInit = new DbInitialization("mongodb://localhost:27017", "MadExpencesTracker");
new DbInitializationUtil(dbInit).Initialize();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IExpencePersistence, ExpencesPersistence>(_ => new ExpencesPersistence(mongoProvider));

builder.Services.AddSingleton<IIncomesPersistence, IncomesPersistence>(_ => new IncomesPersistence(mongoProvider));

builder.Services.AddSingleton<IAmountsPersistence, AmountsPersistence>(_ => new AmountsPersistence(mongoProvider));

builder.Services.AddSingleton<IConfigurationPersistence, ConfigurationPersistence>(_ =>
    new ConfigurationPersistence(mongoProvider));

builder.Services.AddSingleton<IMonthIndexPersistence, MonthIndexPersistence>(_ =>
    new MonthIndexPersistence(mongoProvider));

builder.Services.AddScoped<IExpencesService, ExpencesService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IAmountsService, AmountsService>(_ =>
    new AmountsService(new AmountsPersistence(mongoProvider),
        new ExpencesPersistence(mongoProvider),
        new IncomesPersistence(mongoProvider),
        new ConfigurationPersistence(mongoProvider)
    ));
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<IMonthIndexingService, MonthIndexingService>();
builder.Services.AddScoped<IExpencesService, ExpencesService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();

builder.Services.AddScoped<IMonthClose, MonthClose>(_ =>
    new MonthClose(
        new ExpencesPersistence(mongoProvider),
        new IncomesPersistence(mongoProvider),
        new AmountsPersistence(mongoProvider),
        new ConfigurationPersistence(mongoProvider),
        new MonthIndexPersistence(mongoProvider)
    ));



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