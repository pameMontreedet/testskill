using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppLib.Core;
using Microsoft.EntityFrameworkCore;

namespace AppLib.Modules.Log {

    public class Log : BaseModel {
        [Required]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public long? Id { get; set; }

        [Required]
        public int CardId { get; set; }
        public int TypeId { get; set; }
        public int AddRound { get; set; }
        public int RemoveRound {get; set;}
        public int AddMoney { get; set; }
        public int RemoveMoney {get; set;}
        public DateTime CreatedTime {get; set;}
    }
}