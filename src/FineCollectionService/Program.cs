// create web-app
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IFineCalculator, HardCodedFineCalculator>();
builder.Services.AddTransient<VehicleRegistrationService>();

var daprHttpPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3601";
var daprGrpcPort = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT") ?? "60001";
builder.Services.AddDaprClient(builder => builder
    .UseHttpEndpoint($"http://localhost:{daprHttpPort}")
    .UseGrpcEndpoint($"http://localhost:{daprGrpcPort}"));

builder.Services.AddGrpcClient<FineCollectionService.VehicleInfo.VehicleInfoClient>(o =>
{
    // VehicleRegistrationService is listening on https://localhost:6102.
    o.Address = new Uri($"https://localhost:6102");
});

builder.Services.AddControllers().AddDapr();

var app = builder.Build();

// configure web-app
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseCloudEvents();

// configure routing
app.MapControllers();

app.MapSubscribeHandler();

// let's go!
app.Run("http://localhost:6001");
