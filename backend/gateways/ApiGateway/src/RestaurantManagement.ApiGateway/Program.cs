var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();
app.MapGet("/", () => Results.Ok(new
{
    Service = "RestaurantManagement.ApiGateway",
    Status = "Running",
    Routes = new[]
    {
        "/api/branches -> BranchService (http://localhost:5002)",
        "/api/menu-categories -> MenuService (http://localhost:5004)"
    }
}));

app.MapReverseProxy();
app.Run();
