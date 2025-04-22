using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure application services
// Add custom application services (database, authentication, etc.)
builder.Services.AddApplicationServices(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
SwaggerServiceExtensions.AddOpenApi(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
    app.MapOpenApi();
}

app.MapControllers();

app.Run();
