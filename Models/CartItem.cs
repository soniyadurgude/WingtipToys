using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 
namespace WingtipToys.Models 
{ 
    public class CartItem 
    { 
        [Key] 
        public string ItemId { get; set; } 
        [Required] 
        public string CartId { get; set; } 
        [Required] 
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")] 
        public int Quantity { get; set; } 
        [Required] 
        public System.DateTime DateCreated { get; set; } 
        [Required] 
        [ForeignKey("Product")] 
        public int ProductId { get; set; } 
        public virtual Product Product { get; set; } 
    } 
} 