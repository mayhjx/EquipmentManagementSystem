namespace EquipmentManagementSystem.Interfaces
{
    public interface IUserResolverService
    {
        public string GetUserId();
        public string GetUserName();
        public string GetUserGroup();
    }
}
