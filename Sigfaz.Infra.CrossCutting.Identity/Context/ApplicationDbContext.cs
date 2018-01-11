using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Sigfaz.Infra.Cross.Cutting.Identity.Model;

namespace Sigfaz.Infra.Cross.Cutting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        public ApplicationDbContext()
            : base("SigfazConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}