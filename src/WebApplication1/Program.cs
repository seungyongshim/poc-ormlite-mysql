using ServiceStack.Data;
using ServiceStack.OrmLite;

OrmLiteConfig.DialectProvider = MySqlDialect.Provider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbConnectionFactory>(sp =>
    new OrmLiteConnectionFactory(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddTransient(sp => sp.GetService<IDbConnectionFactory>(). CreateDbConnection());

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

app.UseAuthorization();

app.MapControllers();

app.Run();
