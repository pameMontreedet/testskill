using System;
using System.Collections.Generic;

namespace AppLib.Modules.Card
{
    public class CardModel
    {
        public long? Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public int? CardValue {get; set;}
        public string StationStartcode {get; set;}
        public string StationTerminatecode {get; set;}
        public int? CardValueBalance {get; set;}
        public int? CardRoundBalance {get; set;}
        public DateTime CreatedTime { get; set; }
        public DateTime? LastTimeAddRound { get; set; }
        #region Relationship
        public CardType.CardType CardType { get; set; }

        #endregion
    }


}