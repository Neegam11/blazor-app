var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // .NET 9 way for Swagger

// Add CORS to allow Blazor app to connect
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()    // Allow any origin during development
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // .NET 9 way for Swagger
}

app.UseHttpsRedirection();
app.UseCors("AllowAll"); // Use the new policy
app.UseAuthorization();
app.MapControllers();

// Test endpoint - add this before app.Run()
app.MapGet("/test", () => "API is working!");

app.Run();