using System;
using System.Collections.Generic;

namespace Task5.Dtos.Match
{
    public class GetMatchDto
    {
        public int MatchId { get; set; }

        public string HostTeam { get; set; }
        
        public string GuestTeam { get; set; }

        public DateTime MatchTime { get; set; }

        public string MatchPlace { get; set; }
        
        public string Status { get; set; } 

        public string Result { get; set; }
        
    }
}