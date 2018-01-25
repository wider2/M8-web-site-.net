using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace M8Solutions.DAL
{
    public class DataAccess : DbContext
    {
        public DataAccess() : base("MdfConnection")
        { }

        public DbSet<M8task> M8task { get; set; }

    }
}