using System.Collections.Generic;
using System.Linq;
using Sigfaz.Portal.Models;

namespace Sigfaz.Portal.Domain
{
    public class Data
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 2, nameOption = "Basicos", imageClass = "fa fa-sitemap fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 3, nameOption = "Estado", controller = "Basico/Estado/Estados", action = "Index", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 4, nameOption = "Cidade", controller = "Basico/Cidade/Cidades", action = "Index", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 5, nameOption = "Unidade de Medida", controller = "Basico/UnidadeMedida/UnidadeMedidas", action = "Index", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 6, nameOption = "Classificação do Lote", controller = "Basico/ClassificacaoLote/ClassificacoesLotes", action = "Index", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 7, nameOption = "Cultura", controller = "Basico/Cultura/Culturas", action = "Index", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 8, nameOption = "Destino da Despesa", controller = "Basico/DestinoDespesa/DestinoDespesas", action = "Index", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 9, nameOption = "Grupo", controller = "Basico/Grupo/Grupos", action = "Index", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 10, nameOption = "Item de Manutenção", controller = "Basico/ItemManutencao/ItensManutencao", action = "Index", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 11, nameOption = "Raca", controller = "Basico/Raca/Racas", action = "Index", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 12, nameOption = "Sanidade", controller = "Basico/TipoSanidade/TipoSanidades", action = "Index", status = true, isParent = false, parentId = 2 });

            return menu.ToList();
        }
    }
}