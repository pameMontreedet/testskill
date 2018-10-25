using System;

namespace AppBackend.ViewModels
{
    public class CardView
    {
        public long? Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public int? CardValue {get; set;}
        public string StationStartcode {get; set;}
        public int? CardValueBalance {get; set;}
        public int? CardRoundBalance {get; set;}
        public DateTime CreatedTime { get; set; }
        public DateTime? LastTimeAddRound { get; set; }
    }
}