using System;
using System.Collections.Generic;
using System.Linq;
using Task5.Models;

namespace Task5.Dtos.Team
{
    public class GetTeamDto
    {
        public int TeamId { get; set; }

        public string Name { get; set; }
        
        public string Desc { get; set; }

        public List<string> Players { get; set; }
        public int NumberOfPlayers { get; set; }
        
        
        
        
        
    }
}