using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetails.Models
{
    [Table(nameof(Master))]
    public class Master
    {
        //PROP
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(nameof(MasterId))]
        public int MasterId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), Column(nameof(Data), TypeName = ("smalldatetime")), DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Campo obrigatório"), Column(nameof(ClienteId)), Display(Name ="Cliente")]
        public int ClienteId { get; set; }

        //NAV
        [Required(ErrorMessage = "Campo obrigatório")]
        public List<Detail> Details { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente Cliente { get; set; }
    }
}
