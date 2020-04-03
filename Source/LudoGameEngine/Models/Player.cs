using LudoGameEngine.Models;
using System;
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
        [Required(ErrorMessage = "Required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Invalid color length")]
        public string Color { get; set; }
        public int WinCount { get; set; }
        [Required(ErrorMessage ="Session ID is Required")]
        public Session SessionId { get; set; }
    }
}
