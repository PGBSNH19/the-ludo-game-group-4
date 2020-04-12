using LudoGameEngine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LudoGameEngine
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }
        [Required(ErrorMessage ="Required")]
        [StringLength (25, MinimumLength =3, ErrorMessage ="Invalid name length")]
        public string Name { get; set; }
        public IList<PlayerPiece> PlayerPiece { get; set; }
        public IList<PlayerSession> PlayerSession { get; set; }
    }
}
