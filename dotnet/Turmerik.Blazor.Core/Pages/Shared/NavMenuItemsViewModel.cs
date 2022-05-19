using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Blazor.Core.Pages.Shared
{
    public interface INavMenuItem : ICloneableObject
    {
        string Url { get; }
        string Title { get; }
        string IconCssClass { get; }
    }

    public class NavMenuItemImmtbl : CloneableObjectImmtblBase, INavMenuItem
    {
        public NavMenuItemImmtbl(ClnblArgs args) : base(args)
        {
        }

        public NavMenuItemImmtbl(ICloneableMapper mapper, INavMenuItem src) : base(mapper, src)
        {
        }

        public string Url { get; protected set; }
        public string Title { get; protected set; }
        public string IconCssClass { get; protected set; }
    }

    public class NavMenuItemMtbl : CloneableObjectMtblBase, INavMenuItem
    {
        public NavMenuItemMtbl()
        {
        }

        public NavMenuItemMtbl(ClnblArgs args) : base(args)
        {
        }

        public NavMenuItemMtbl(ICloneableMapper mapper, INavMenuItem src) : base(mapper, src)
        {
        }

        public string Url { get; set; }
        public string Title { get; set; }
        public string IconCssClass { get; set; }
    }

    public class NavMenuItemsDictnr : NestedClnblDictnr<string, INavMenuItem, NavMenuItemImmtbl, NavMenuItemMtbl>
    {
        public NavMenuItemsDictnr()
        {
        }

        public NavMenuItemsDictnr(ReadOnlyDictionary<string, NavMenuItemImmtbl> immtbl) : base(immtbl)
        {
        }

        public NavMenuItemsDictnr(ReadOnlyDictionary<string, NavMenuItemImmtbl> immtbl, Dictionary<string, NavMenuItemMtbl> mtbl) : base(immtbl, mtbl)
        {
        }
    }

    public interface INavMenuItemsViewModel : ICloneableObject
    {
        NavMenuItemsDictnr NavMenuItemsDictnr { get; }
    }

    public class NavMenuItemsViewModelImmtbl : CloneableObjectImmtblBase, INavMenuItemsViewModel
    {
        public NavMenuItemsViewModelImmtbl(ClnblArgs args) : base(args)
        {
        }

        public NavMenuItemsViewModelImmtbl(ICloneableMapper mapper, INavMenuItemsViewModel src) : base(mapper, src)
        {
        }

        public NavMenuItemsDictnr NavMenuItemsDictnr { get; protected set; }
    }

    public class NavMenuItemsViewModelMtbl : CloneableObjectMtblBase, INavMenuItemsViewModel
    {
        public NavMenuItemsViewModelMtbl()
        {
        }

        public NavMenuItemsViewModelMtbl(ClnblArgs args) : base(args)
        {
        }

        public NavMenuItemsViewModelMtbl(ICloneableMapper mapper, INavMenuItemsViewModel src) : base(mapper, src)
        {
        }

        public NavMenuItemsDictnr NavMenuItemsDictnr { get; set; }
    }
}
