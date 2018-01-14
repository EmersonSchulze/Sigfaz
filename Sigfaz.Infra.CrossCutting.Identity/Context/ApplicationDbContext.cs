using Microsoft.AspNet.Identity.EntityFramework;
using Sigfaz.Infra.CrossCutting.Identity.Model;
using System;
using System.Data.Entity;

namespace Sigfaz.Infra.CrossCutting.Identity.Context
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        public ApplicationDbContext()
            : base("SigfazConection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}