using System; 
using System.ComponentModel.DataAnnotations; 
namespace WingtipToys.Models 
{ 
    public class User 
    { 
        [Key] 
        public int UserId { get; set; } 
        [Required] 
        [StringLength(256)] 
        public string Username { get; set; } 
        [Required] 
        [StringLength(256)] 
        public string Email { get; set; } 
        [Required] 
        public string PasswordHash { get; set; } 
        public DateTime CreatedDate { get; set; } 
        public DateTime? LastLoginDate { get; set; } 
        public User() 
        { 
            CreatedDate = DateTime.UtcNow; 
        } 
    } 
}