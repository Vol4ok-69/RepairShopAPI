namespace RepairShopAPI.HelperControllerClasses
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
