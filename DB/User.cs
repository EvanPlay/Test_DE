using System.ComponentModel.DataAnnotations;

namespace WpfApp6.DB
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
    }
}