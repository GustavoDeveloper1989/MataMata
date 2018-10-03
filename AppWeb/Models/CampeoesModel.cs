using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class CampeoesModel
    {
        [Key]
        public int Id { get; set; }
        public int Id_Torneio { get; set; }
        public string Campeao { get; set; }
    }
}