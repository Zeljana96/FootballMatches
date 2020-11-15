using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task5.Models
{
    public class Match
    {
        public int MatchId { get; set; }

        public string HostTeam { get; set; }
        
        public string GuestTeam { get; set; }

        public DateTime MatchTime { get; set; }

        public string MatchPlace { get; set; }
        
        public string Status { get; set; } = StatusClass.Not_Started.ToString("G");

        public string Result { get; set; }
        
        
        
    }
}