namespace VehicleRegistrationService.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleInfoController : ControllerBase
{
    private readonly ILogger<VehicleInfoController> _logger;
    private readonly IVehicleInfoRepository _vehicleInfoRepository;

    public VehicleInfoController(ILogger<VehicleInfoController> logger, IVehicleInfoRepository vehicleInfoRepository)
    {
        _logger = logger;
        _vehicleInfoRepository = vehicleInfoRepository;
    }

    private static int _callCount;

    [HttpGet("{licenseNumber}")]
    public ActionResult GetVehicleInfo(string licenseNumber)
    {
        _callCount += 1;

        _logger.LogInformation($"{_callCount}: Retrieving vehicle-info for licensenumber {licenseNumber}");

        if (_callCount % 2 == 0)
        {
            return StatusCode(503);
        }

        var info = _vehicleInfoRepository.GetVehicleInfo(licenseNumber);
        return Ok(info);
    }
}
