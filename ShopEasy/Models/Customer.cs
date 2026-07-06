using System.ComponentModel.DataAnnotations;

namespace ShopEasy.Models
{
    public class Customer
    {
        [Key]
        public int customer_Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string customer_Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(100)]
        public string customer_Email { get; set; }

        [StringLength(15)]
        public string customer_Phone { get; set; } = string.Empty;

        [StringLength(500)]
        public string customer_Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string customer_Password { get; set; }

        public bool IsVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
