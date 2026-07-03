using System.ComponentModel.DataAnnotations;

namespace ShopEasy.Models
{
    public class Customer
    {
        [Key]
        public int customer_Id { get; set; }
        public string customer_Name { get; set; }

        public string customer_Email { get; set; }

        public int customer_Password { get; set; }

    }
}
