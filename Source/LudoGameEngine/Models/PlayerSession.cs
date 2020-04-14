using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LudoGameEngine.Models
{
    public class PlayerSession
    {
        public int PlayerId { get; set; }
        public Player PlayerRef { get; set; }
        public int SessionId { get; set; }
        public Session SessionRef { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Invalid color length")]
        public string Color { get; set; }
    }
}
