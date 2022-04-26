using System.ComponentModel.DataAnnotations;

namespace Eryaz_ProductSearch.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
