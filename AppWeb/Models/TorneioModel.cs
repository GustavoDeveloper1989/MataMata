using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class TorneioModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Qnt_Times { get; set; }

    }
}