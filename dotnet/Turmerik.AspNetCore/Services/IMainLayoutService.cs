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
        bool OverlayEnabled { get; }
        ErrorViewModel ErrorViewModel { get; }

        event Action<bool> OnSideBarSizeChanged;
        event Action<bool> OnOverlayEnabledChanged;
        event Action<ErrorViewModel> OnErrorViewModelChanged;

        void SideBarSizeChanged(bool sideBarLarge);
        Task ExecuteWithUIBlockingOverlay(Func<Task<ErrorViewModel>> action);
        void HideOverlay();
    }

    public class MainLayoutService : IMainLayoutService
    {
        private Action<bool>? onSideBarSizeChanged;
        private Action<bool>? onOverlayEnabledChanged;
        private Action<ErrorViewModel>? onErrorViewModelChanged;

        public bool OverlayEnabled { get; private set; }
        public ErrorViewModel ErrorViewModel { get; private set; }


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

        public event Action<bool> OnOverlayEnabledChanged
        {
            add
            {
                onOverlayEnabledChanged += value;
            }

            remove
            {
                onOverlayEnabledChanged -= value;
            }
        }

        public event Action<ErrorViewModel> OnErrorViewModelChanged
        {
            add
            {
                onErrorViewModelChanged += value;
            }

            remove
            {
                onErrorViewModelChanged -= value;
            }
        }

        public void SideBarSizeChanged(bool sideBarLarge)
        {
            onSideBarSizeChanged?.Invoke(sideBarLarge);
        }

        public void HideOverlay()
        {
            ErrorViewModel = null;
            onErrorViewModelChanged?.Invoke(ErrorViewModel);

            OverlayEnabled = false;
            onOverlayEnabledChanged?.Invoke(OverlayEnabled);
        }

        public async Task ExecuteWithUIBlockingOverlay(Func<Task<ErrorViewModel>> action)
        {
            OverlayEnabled = true;
            onOverlayEnabledChanged?.Invoke(OverlayEnabled);

            ErrorViewModel errorViewModel;

            try
            {
                errorViewModel = await action();

                OverlayEnabled = errorViewModel != null;
                onOverlayEnabledChanged?.Invoke(OverlayEnabled);
            }
            catch (Exception exc)
            {
                errorViewModel = new ErrorViewModel("An unhandled error ocurred", exc);
            }

            ErrorViewModel = errorViewModel;
            onErrorViewModelChanged?.Invoke(errorViewModel);
        }
    }
}
