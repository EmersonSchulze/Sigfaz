using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Infra.CrossCutting.Identity.Model;
using Sigfaz.Portal.Areas.Basico.ViewModels.Cidade;
using Sigfaz.Portal.Areas.Basico.ViewModels.Estado;
using Sigfaz.Portal.Areas.Basico.ViewModels.UnidadeMedida;

namespace Sigfaz.Portal.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EstadoIndexViewModel, Estado>();
            CreateMap<CidadeIndexViewModel, Cidade>();
            CreateMap<EstadoDetalheViewModel, Estado>();
            CreateMap<CidadeDetalheViewModel, Cidade>();
            CreateMap<UnidadeMedidaIndexViewModel, UnidadeMedida>();
            CreateMap<RoleViewModel, IdentityRole>();
          
        }
    }
}