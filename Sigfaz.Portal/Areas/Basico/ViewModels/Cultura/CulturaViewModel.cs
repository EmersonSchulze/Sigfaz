using System;
using System.ComponentModel.DataAnnotations;
using Sigfaz.Infra.ComponentModel.DataAnnotations;
using Sigfaz.Infra.Mvc;

namespace Sigfaz.Portal.Areas.Basico.ViewModels.Cultura
{
	public class CulturaViewModel
	{
		[Display(Name = "Descricao", Description = "")]
        [Width(Width.Six)]
		public String Descricao { get; set; } 

       
        [Display(Name = "Nome Popular", Description = "")]
	    [Width(Width.Six)]
		public String Apelido { get; set; } 

	}
}