using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Porto.Saude.ComponentModel;
using Porto.Saude.Infra.Data;

namespace Porto.Saude.Infra.Monitoring
{
    public partial class MensagemRegistroProcesso : IValidatableObject 
    {
        [Display(Name = "Sequência", Description = "")]
        public virtual int Sequencia { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {            
            // Aqui devem ser implementadas regras de validação mais complexas que utilizem mais de 
            // um campo ou dependam do contexto
            // Por exemplo, regras de vigência onde uma data inicial não pode ser maior que a final
            // if (DataInicial.HasValue && DataFinal.HasValue && DataFinal.Value < DataInicial)
            //    yield return new ValidationResult("Data final de vigência deve ser igual ou superior à data inicial de vigência");
            //
            // if (DataFinal.HasValue && (DataFinal.Value - DataInicial.Value).Days > 180)
            //    yield return new ValidationResult("Validade da permissão deve ser inferior a 180 dias");

            // Exemplos de validação de vigência
            //if (!this.Competencia.Valida())
            //{
            //    yield return new ValidationResult("A vigência informada é inválida.");
            //}

            //if (this.Competencia.Cruzada(consulta.Select(d => d.Competencia)))
            //{
            //    yield return new ValidationResult("O período de vigência informado compreende a vigência de outro registro.");
            //}

            return Enumerable.Empty<ValidationResult>();
        }
    }
}