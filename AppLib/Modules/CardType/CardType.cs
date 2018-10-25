using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppLib.Core;
using Microsoft.EntityFrameworkCore;

namespace AppLib.Modules.CardType {

    public class CardType : BaseModel {
        [Required]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public long? Id { get; set; }

        [MaxLength (250)]
        public string Name { get; set; }

        #region Relationship
        public ICollection<Card.Card> Card { get; set; }
        #endregion
    }
}