using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Components;

namespace Turmerik.AspNetCore.Services
{
    public interface IMainLayoutService
    {
        UIBlockingOverlayViewModel UIBlockingOverlayViewModel { get; }

        event Action<bool> OnSideBarSizeChanged;
        event Action OnUIBlockingOverlayChanged;

        void SideBarSizeChanged(bool sideBarLarge);
        Task ExecuteWithUIBlockingOverlay(Func<UIBlockingOverlayViewModel, Task> action);
    }

    public class MainLayoutService : IMainLayoutService
    {
        private Action<bool>? onSideBarSizeChanged;
        private Action onUIBlockingOverlayChanged;

        public MainLayoutService()
        {
            UIBlockingOverlayViewModel = new UIBlockingOverlayViewModel();
        }

        public UIBlockingOverlayViewModel UIBlockingOverlayViewModel { get; set; }

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

        public event Action OnUIBlockingOverlayChanged
        {
            add
            {
                onUIBlockingOverlayChanged += value;
            }

            remove
            {
                onUIBlockingOverlayChanged -= value;
            }
        }

        public void SideBarSizeChanged(bool sideBarLarge)
        {
            onSideBarSizeChanged?.Invoke(sideBarLarge);
        }

        public async Task ExecuteWithUIBlockingOverlay(Func<UIBlockingOverlayViewModel, Task> action)
        {
            UIBlockingOverlayViewModel.Enabled = true;
            UIBlockingOverlayViewModel.Error = null;

            onUIBlockingOverlayChanged?.Invoke();

            try
            {
                await action(UIBlockingOverlayViewModel);
            }
            catch (Exception exc)
            {
                UIBlockingOverlayViewModel.Enabled = true;
                UIBlockingOverlayViewModel.Error = new ErrorViewModel("An unhandled error ocurred", exc);
            }

            onUIBlockingOverlayChanged?.Invoke();
        }
    }
}
