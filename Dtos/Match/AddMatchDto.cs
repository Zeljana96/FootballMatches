using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task5.Dtos.Match
{
    public class AddMatchDto
    {

        public string HostTeam { get; set; }
        
        public string GuestTeam { get; set; }

        public DateTime MatchTime { get; set; }
        public string MatchPlace { get; set; }

        public List<string> HostTeamPlayers { get; set; }
        public List<string> GuestTeamPlayers { get; set; }

        
        
        
        
        
        
    }
}