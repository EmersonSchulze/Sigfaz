using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sigfaz.Infra.Monitoring.Performance.Entidades
{
    public partial class RegistroMonitoramento : IValidatableObject 
    { 
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {                        
            return Enumerable.Empty<ValidationResult>();
        }
    }
}