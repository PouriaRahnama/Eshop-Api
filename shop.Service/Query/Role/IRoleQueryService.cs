namespace shop.Service.Query
{
    public interface IRoleQueryService
    {
        Task<RoleQueryDto?> GetRoleById(int RoleId);
        Task<List<RoleQueryDto>> GetAllRole();
    }
}
