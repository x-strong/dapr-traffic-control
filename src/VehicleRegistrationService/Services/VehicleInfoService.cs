using Grpc.Core;

namespace VehicleRegistrationService.Services;

public class VehicleInfoService : VehicleInfo.VehicleInfoBase
{
    private readonly ILogger<VehicleInfoService> _logger;
    private readonly IVehicleInfoRepository _vehicleInfoRepository;

    public VehicleInfoService(ILogger<VehicleInfoService> logger, IVehicleInfoRepository vehicleInfoRepository)
    {
        _logger = logger;
        _vehicleInfoRepository = vehicleInfoRepository;
    }
    
    public override Task<VehicleInfoReply> GetVehicleInfo(VehicleInfoRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Retrieving vehicle-info for licensenumber {request.LicenseNumber}");

        var info = _vehicleInfoRepository.GetVehicleInfo(request.LicenseNumber);
        
        return Task.FromResult(new VehicleInfoReply
        {
            VehicleId = info.VehicleId,
            Brand = info.Brand,
            Model = info.Model,
            OwnerName = info.OwnerName,
            OwnerEmail = info.OwnerEmail
        });
    }
}

