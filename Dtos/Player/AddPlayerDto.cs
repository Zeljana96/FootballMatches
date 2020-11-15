using System.ComponentModel.DataAnnotations;
namespace Task5.Dtos.Player
{
    public class AddPlayerDto
    { 
        [Required]
        public string Name { get; set; }

        public int TeamId { get; set; }
        
        

    }
}