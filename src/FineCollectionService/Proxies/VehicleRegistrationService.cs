namespace FineCollectionService.Proxies;

public class VehicleRegistrationService
{
    private VehicleInfo.VehicleInfoClient _vehicleInfoClient;

    public VehicleRegistrationService(VehicleInfo.VehicleInfoClient vehicleInfoClient)
    {
        _vehicleInfoClient = vehicleInfoClient;
    }

    public async Task<Models.VehicleInfo> GetVehicleInfo(string licenseNumber)
    {
        var request = new VehicleInfoRequest
        {
            LicenseNumber = licenseNumber
        };

        var reply = await _vehicleInfoClient.GetVehicleInfoAsync(request);

        return new Models.VehicleInfo
        {
            VehicleId = reply.VehicleId,
            Brand = reply.Brand,
            Model = reply.Model,
            OwnerName = reply.OwnerName,
            OwnerEmail = reply.OwnerEmail
        };
    }
}
