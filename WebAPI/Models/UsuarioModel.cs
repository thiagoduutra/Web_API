namespace WebAPI.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime DataCreate { get; set; }
        public DateTime DataUpdate { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
