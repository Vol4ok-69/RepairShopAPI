namespace RepairShopAPI.HelperControllerClasses
{
    public class UserUpdateDto
    {
        public string? Email { get; set; }
        public Guid Roleid { get; set; }
        public string? Phone { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string? Password { get; set; }
    }
}
