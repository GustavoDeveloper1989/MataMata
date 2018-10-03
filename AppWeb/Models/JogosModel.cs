using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class JogosModel
    {

        [Key]
        public int Id { get; set; }
        public int Id_Torneio { get; set; }
        public string TimeUm { get; set; }
        public string EscudoUm { get; set; }
        public string TimeDois { get; set; }
        public string EscudoDois { get; set; }
        public int GolsUm { get; set; }
        public int GolsDois { get; set; }

    }
}