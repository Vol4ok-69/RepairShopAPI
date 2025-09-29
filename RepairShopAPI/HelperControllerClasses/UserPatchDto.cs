namespace RepairShopAPI.HelperControllerClasses
{
    public class UserPatchDto
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Patronymic { get; set; }
        public string? Password { get; set; }
        public Guid? Roleid { get; set; }
    }
}
