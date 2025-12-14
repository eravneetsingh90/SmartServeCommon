using SmartServe.Domain.Dependencies;
using SmartServe.EFCore.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.UseEFCore(builder.Configuration);


builder.Services.UseSmartServeStores();
// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
