using System.ComponentModel.DataAnnotations;

namespace ShopEasy.Models
{
    public class Customer
    {
        [Key]
        public int customer_Id { get; set; }
        public string customer_Name { get; set; }
        public string customer_Email { get; set; }
        public string customer_Phone { get; set; }
        public string customer_Address { get; set; }
        public string customer_Password { get; set; }
        public bool IsVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
