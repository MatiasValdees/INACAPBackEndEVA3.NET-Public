namespace EVA3.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Rol {get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
    }
}
