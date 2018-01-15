using AutoMapper;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Portal.Areas.Basico.ViewModels.Cidade;
using Sigfaz.Portal.Areas.Basico.ViewModels.Estado;

namespace Sigfaz.Portal.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EstadoIndexViewModel, Estado>();
            CreateMap<CidadeIndexViewModel, Cidade>();
        }
    }
}