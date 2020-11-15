using System.Collections.Generic;

namespace Task5.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        public string Name { get; set; }
        
        public string Desc { get; set; }

        public List<Player> Players { get; set; }
        
        

    }
}