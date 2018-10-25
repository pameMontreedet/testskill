using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppLib.Core;
using Microsoft.EntityFrameworkCore;

namespace AppLib.Modules.Station {

    public class Station : BaseModel {
        [Required]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public long? Id { get; set; }

        [Required]
        public string Code { get; set; }

        [MaxLength (250)]
        public string Name { get; set; }

        public int LineId { get; set; }

        public DateTime CreatedTime {get; set;}
    }
}