using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Middleware;
using RaceDay.API.Services;
using System.Reflection;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

// Register RaceDayDbContext (Microsoft, 2024)
builder.Services.AddDbContext<RaceDayDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configuring Swagger/OpenAPI with XML documentation integration (Microsoft, 2024)
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RaceDay API",
        Version = "v1",
        Description = "RESTful Web API for RaceDay event management, enrolments, and performance results."
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddScoped<IAuthService, AuthService>();

// Configuring CORS policy to allow client applications (Microsoft, 2024)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Seed initial database records on application startup (Microsoft, 2024)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<RaceDayDbContext>();
        var authService = services.GetRequiredService<IAuthService>();
        await DbInitializer.SeedAsync(context, authService);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Register global exception handling middleware (Microsoft, 2024)
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS middleware (Microsoft, 2024)
app.UseCors("AllowAllPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();