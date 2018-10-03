using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AppWeb.Models;

namespace AppWeb.Context
{
    public class MataMataContext : DbContext
    {

        public DbSet<TimeModel> Times { get; set; }

        public MataMataContext()
            : base(Properties
                  .Settings
                  .Default.DBConStr)
        {
        }

        public System.Data.Entity.DbSet<AppWeb.Models.TorneioModel> TorneioModels { get; set; }

        public System.Data.Entity.DbSet<AppWeb.Models.JogosModel> JogosModels { get; set; }

        public System.Data.Entity.DbSet<AppWeb.Models.CampeoesModel> Campeoes { get; set; }

    }
}