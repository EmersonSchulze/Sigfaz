using AutoMapper;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Portal.Areas.Cidade.ViewModels;
using Sigfaz.Portal.Areas.Estado.ViewModel;

namespace Sigfaz.Portal.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EstadoViewModel, Estado>();
            CreateMap<CidadeViewModel, Cidade>();
        }
    }
}