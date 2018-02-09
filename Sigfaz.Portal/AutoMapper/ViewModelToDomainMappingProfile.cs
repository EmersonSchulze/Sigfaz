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
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RoleViewModel, IdentityRole>();

            #region Cadastros Basicos
            CreateMap<EstadoIndexViewModel, Estado>();
            CreateMap<CidadeIndexViewModel, Cidade>();
            CreateMap<UnidadeMedidaIndexViewModel, UnidadeMedida>();
            CreateMap<ClassificacaoLoteIndexViewModel, ClassificacaoLote>();
            CreateMap<CulturaIndexViewModel, Cultura>();
            CreateMap<CulturaViewModel, Cultura>();
            CreateMap<DestinoDespesaIndexViewModel, DestinoDespesa>();
            CreateMap<GrupoIndexViewModel, Grupo>();
            CreateMap<ItemManutencaoIndexViewModel, ItemManutencao>();
            CreateMap<RacaIndexViewModel, Raca>();
            CreateMap<TipoSanidadeIndexViewModel, TipoSanidade>();
            #endregion
        }
    }
}