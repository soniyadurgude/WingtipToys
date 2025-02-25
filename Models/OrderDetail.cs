using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
namespace WingtipToys.Models 
{ 
    public class OrderDetail 
    { 
        [Key] 
        public int OrderDetailId { get; set; } 
        [ForeignKey("Order")] 
        public int OrderId { get; set; } 
        public string Username { get; set; } 
        [ForeignKey("Product")] 
        public int ProductId { get; set; } 
        public int Quantity { get; set; } 
        [Column(TypeName = "decimal(18,2)")] 
        public decimal? UnitPrice { get; set; } 
        public virtual Order Order { get; set; } 
        public virtual Product Product { get; set; } 
    } 
} 