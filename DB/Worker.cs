using System.ComponentModel.DataAnnotations;

namespace WpfApp6.DB
{
    public class Worker
    {
        [Key]
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
        public string job { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
    }
}