using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasterDetails.Models
{
    public class Master
    {
        [Required]
        public int MasterId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Required]
        public List<Detail> Details { get; set; }
    }
}
