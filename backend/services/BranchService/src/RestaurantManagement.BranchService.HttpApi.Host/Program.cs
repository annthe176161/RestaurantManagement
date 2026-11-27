using RestaurantManagement.BranchService;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseAutofac();
await builder.AddApplicationAsync<BranchServiceHttpApiHostModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();
await app.RunAsync();
