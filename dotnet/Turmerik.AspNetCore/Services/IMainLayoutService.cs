using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services
{
    public interface IMainLayoutService
    {
        event Action<bool> OnSideBarSizeChanged;
        void SideBarSizeChanged(bool sideBarLarge);
    }

    public class MainLayoutService : IMainLayoutService
    {
        private Action<bool> onSideBarSizeChanged;

        public event Action<bool> OnSideBarSizeChanged
        {
            add
            {
                onSideBarSizeChanged += value;
            }

            remove
            {
                onSideBarSizeChanged -= value;
            }
        }

        public void SideBarSizeChanged(bool sideBarLarge)
        {
            onSideBarSizeChanged?.Invoke(sideBarLarge);
        }
    }
}
