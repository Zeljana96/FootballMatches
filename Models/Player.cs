using System.ComponentModel.DataAnnotations;

namespace Task5.Models
{
    public class Player
    {   
        [Key]
        public int PlayerId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public int Matches { get; set; } = 0;
        
        public int Goals { get; set; } = 0;
        
        public Team Team { get; set; }
        
        
        
        
    }
}