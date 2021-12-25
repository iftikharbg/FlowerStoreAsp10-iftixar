using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace FlowerStore.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        
    }
}
