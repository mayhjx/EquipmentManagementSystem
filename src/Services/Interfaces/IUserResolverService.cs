namespace EquipmentManagementSystem.Services.Interfaces
{
    public interface IUserResolverService
    {
        string GetUserId();
        string GetUserName();
        string GetUserGroup();
    }
}
