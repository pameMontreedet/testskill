using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppLib.Core;
using Microsoft.EntityFrameworkCore;

namespace AppLib.Modules.Card {

    public class Card : BaseModel {
        [Required]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public long? Id { get; set; }

        [Required]
        public int TypeId { get; set; }

        [MaxLength (250)]
        public string Name { get; set; }
        public int? CardValue {get; set;} = 0;
        public string StationStartcode {get; set;}
        public string StationTerminatecode {get; set;}
        public int? CardValueBalance {get; set;} = 0;
        public int? CardRoundBalance {get; set;} = 0;
        public DateTime CreatedTime {get; set;}
        public DateTime? LastTimeAddRound {get; set;}
        #region Relationship
        public CardType.CardType CardType { get; set;}
        
        #endregion
    }
}