using System.ComponentModel.DataAnnotations;

namespace ShopEasy.Models
{
    public class Admin
    {
        [Key]
        public int admin_Id { get; set; }
        public string admin_name { get; set; }
        public string admin_password { get; set; }
        public string admin_image { get; set; }



    }
}
