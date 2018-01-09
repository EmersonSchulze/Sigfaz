using AutoMapper;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Portal.ViewModels;

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