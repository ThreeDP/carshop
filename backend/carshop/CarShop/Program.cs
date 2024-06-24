
using CarShop.Context;
using CarShop.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using CarShop.Filters;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Logger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => {
    options.Filters.Add(typeof(ApiExceptionFilter));
})
.AddJsonOptions(opts =>
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddTransient<DbContext, CarShopDataContext>( conf => );
// builder.Services.AddTransient<IConfiguration>( conf => builder.Configuration);
builder.Services.AddDbContext<CarShopDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("defaultConn")));
builder.Services.AddScoped<CarShopLoggingFilter>();
builder.Services.AddCors();
builder.Logging.AddProvider(new CustomLoggerProvider( new CustomLoggerProviderConfig {
    LogLevel = LogLevel.Information
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
    app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true));
                    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    // .AllowCredentials());
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
