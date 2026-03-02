namespace WebAPI.Models
{
    public class AuditoriaModel
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string DataUpdate { get; set; }
    }
}
