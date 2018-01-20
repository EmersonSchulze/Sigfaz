using System.Web.Security;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Infra.CrossCutting.Identity.Configuration;
using Sigfaz.Infra.CrossCutting.Identity.Model;
using Sigfaz.Portal.Areas.Basico.ViewModels.Cidade;
using Sigfaz.Portal.Areas.Basico.ViewModels.Estado;

namespace Sigfaz.Portal.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Estado, EstadoIndexViewModel>();
            // .ForMember(vm => vm.EstadoId, map => map.MapFrom(s => s.EstadoId))
            // .ForMember(vm => vm.Nome, map => map.MapFrom(s => s.Nome))
            //.ForMember(vm => vm.Sigla, map => map.MapFrom(s => s.Sigla));
            CreateMap<Cidade, CidadeIndexViewModel>();
            // .ForMember(vm => vm.CidadeId, map => map.MapFrom(s => s.CidadeId))
            // .ForMember(vm => vm.Nome, map => map.MapFrom(s => s.Nome))
            // .ForMember(vm => vm.EstadoId, map => map.MapFrom(s => s.Estado.EstadoId));
            CreateMap<IdentityRole, RoleViewModel>();

          

        }
    }
}