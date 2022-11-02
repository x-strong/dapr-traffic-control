namespace VehicleRegistrationService.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleInfoController : ControllerBase
{
    private readonly ILogger<VehicleInfoController> _logger;
    private readonly IVehicleInfoRepository _vehicleInfoRepository;

    private static int _callCount;

    public VehicleInfoController(ILogger<VehicleInfoController> logger, IVehicleInfoRepository vehicleInfoRepository)
    {
        _logger = logger;
        _vehicleInfoRepository = vehicleInfoRepository;
    }

    [HttpGet("{licenseNumber}")]
    public async Task<ActionResult> GetVehicleInfo(string licenseNumber)
    {
        _callCount += 1;

        _logger.LogInformation($"Retrieving vehicle-info for licensenumber {licenseNumber}");

        if (_callCount % 3 != 0)
        {
            return base
        }

        // Take some time to trigger resiliency timeout.
        await Task.Delay(TimeSpan.FromSeconds(3));

        VehicleInfo info = _vehicleInfoRepository.GetVehicleInfo(licenseNumber);
        return info;
    }
}
