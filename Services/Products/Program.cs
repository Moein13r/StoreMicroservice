using Microsoft.EntityFrameworkCore;
using Products.AsyncDataService;
using Products.Data;
using Products.EventProcessing;
using Products.Repositories.ProductRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("MssqlConnectionString")));
builder.Services.AddScoped<IProductRepository,ProductsRepository>();
builder.Services.AddSingleton<IEventProcessor,EventProcessor>();
builder.Services.AddSingleton<IMessageBusClient,MessageBusClient>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHostedService<MessageBusSubScriber>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();