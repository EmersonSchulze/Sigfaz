using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Infra.CrossCutting.Identity.Model;
using Sigfaz.Portal.Areas.Basico.ViewModels.Cidade;
using Sigfaz.Portal.Areas.Basico.ViewModels.ClassificacaoLote;
using Sigfaz.Portal.Areas.Basico.ViewModels.Cultura;
using Sigfaz.Portal.Areas.Basico.ViewModels.DestinoDespesa;
using Sigfaz.Portal.Areas.Basico.ViewModels.Estado;
using Sigfaz.Portal.Areas.Basico.ViewModels.Grupo;
using Sigfaz.Portal.Areas.Basico.ViewModels.ItemManutencao;
using Sigfaz.Portal.Areas.Basico.ViewModels.Raca;
using Sigfaz.Portal.Areas.Basico.ViewModels.TipoSanidade;
using Sigfaz.Portal.Areas.Basico.ViewModels.UnidadeMedida;

namespace Sigfaz.Portal.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            // .ForMember(vm => vm.CidadeId, map => map.MapFrom(s => s.CidadeId))
            // .ForMember(vm => vm.Nome, map => map.MapFrom(s => s.Nome))
            // .ForMember(vm => vm.EstadoId, map => map.MapFrom(s => s.Estado.EstadoId));
            CreateMap<IdentityRole, RoleViewModel>();

            #region Cadastro Basico
            CreateMap<Estado, EstadoIndexViewModel>();
            CreateMap<Cidade, CidadeIndexViewModel>();
            CreateMap<UnidadeMedida, UnidadeMedidaIndexViewModel>();
            CreateMap<ClassificacaoLote, ClassificacaoLoteIndexViewModel>();
            CreateMap<Cultura, CulturaIndexViewModel>();
            CreateMap<DestinoDespesa, DestinoDespesaIndexViewModel>();
            CreateMap<Grupo, GrupoIndexViewModel>();
            CreateMap<ItemManutencao, ItemManutencaoIndexViewModel>();
            CreateMap<Raca, RacaIndexViewModel>();
            CreateMap<TipoSanidade, TipoSanidadeIndexViewModel>();
            #endregion

        }
    }
}