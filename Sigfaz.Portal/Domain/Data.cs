using System.Collections.Generic;
using System.Linq;
using Sigfaz.Portal.Models;

namespace Sigfaz.Portal.Domain
{
    public class Data
    {
        public IEnumerable<Navbar> NavbarItems()
        {
            var menu = new List<Navbar>();
            menu.Add(new Navbar { Id = 1, NameOption = "Dashboard", Controller = "Home", Action = "Index", ImageClass = "fa fa-dashboard fa-fw", Status = true, IsParent = false, ParentId = 0 });
            menu.Add(new Navbar { Id = 2, NameOption = "Basicos", ImageClass = "fa fa-sitemap fa-fw", Status = true, IsParent = true, ParentId = 0 });
            menu.Add(new Navbar { Id = 3, NameOption = "Estado", Controller = "Basico/Estado/Estados", Action = "Index", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new Navbar { Id = 4, NameOption = "Cidade", Controller = "Basico/Cidade/Cidades", Action = "Index", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new Navbar { Id = 5, NameOption = "Unidade de Medida", Controller = "Basico/UnidadeMedida/UnidadeMedidas", Action = "Index", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new Navbar { Id = 6, NameOption = "Classificação do Lote", Controller = "Basico/ClassificacaoLote/ClassificacoesLotes", Action = "Index", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new Navbar { Id = 7, NameOption = "Cultura", Controller = "Basico/Cultura/Culturas", Action = "Index", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new Navbar { Id = 8, NameOption = "Destino da Despesa", Controller = "Basico/DestinoDespesa/DestinoDespesas", Action = "Index", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new Navbar { Id = 9, NameOption = "Grupo", Controller = "Basico/Grupo/Grupos", Action = "Index", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new Navbar { Id = 10, NameOption = "Item de Manutenção", Controller = "Basico/ItemManutencao/ItensManutencao", Action = "Index", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new Navbar { Id = 11, NameOption = "Raca", Controller = "Basico/Raca/Racas", Action = "Index", Status = true, IsParent = false, ParentId = 2 });
            menu.Add(new Navbar { Id = 12, NameOption = "Sanidade", Controller = "Basico/TipoSanidade/TipoSanidades", Action = "Index", Status = true, IsParent = false, ParentId = 2 });

            return menu.ToList();
        }
    }
}