namespace VehicleRegistrationService.Repositories;

public interface IVehicleInfoRepository
{
    Models.VehicleInfo GetVehicleInfo(string licenseNumber);
}
