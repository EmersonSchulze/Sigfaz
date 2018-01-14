using AutoMapper;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Portal.Areas.Cidade.ViewModels;
using Sigfaz.Portal.Areas.Estado.ViewModel;

namespace Sigfaz.Portal.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Estado, EstadoViewModel>();
            // .ForMember(vm => vm.EstadoId, map => map.MapFrom(s => s.EstadoId))
            // .ForMember(vm => vm.Nome, map => map.MapFrom(s => s.Nome))
            //.ForMember(vm => vm.Sigla, map => map.MapFrom(s => s.Sigla));
            CreateMap<Cidade, CidadeViewModel>();
            // .ForMember(vm => vm.CidadeId, map => map.MapFrom(s => s.CidadeId))
            // .ForMember(vm => vm.Nome, map => map.MapFrom(s => s.Nome))
            // .ForMember(vm => vm.EstadoId, map => map.MapFrom(s => s.Estado.EstadoId));
        }
    }
}