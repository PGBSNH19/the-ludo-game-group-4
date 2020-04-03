using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LudoGameEngine.Models
{
    public class Session
    {
        [Key]
        public int SessionID { get; set; }
        [Required(ErrorMessage = "Required")]
        public bool GameFinished { get; set; }
        public string Winner { get; set; }
    }
}
