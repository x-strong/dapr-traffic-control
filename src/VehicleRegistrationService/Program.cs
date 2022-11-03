// create web-app
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IVehicleInfoRepository, InMemoryVehicleInfoRepository>();

builder.Services.AddGrpc();

var daprHttpPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3602";
var daprGrpcPort = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT") ?? "60002";
builder.Services.AddDaprClient(builder => builder
    .UseHttpEndpoint($"http://localhost:{daprHttpPort}")
    .UseGrpcEndpoint($"http://localhost:{daprGrpcPort}"));

builder.Services.AddControllers().AddDapr();

var app = builder.Build();

// configure web-app
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseCloudEvents();

// configure routing
app.MapGrpcService<VehicleInfoService>();
app.MapControllers();

app.Urls.Add("http://localhost:6002");
app.Urls.Add("https://localhost:6102");

// let's go!
app.Run();
