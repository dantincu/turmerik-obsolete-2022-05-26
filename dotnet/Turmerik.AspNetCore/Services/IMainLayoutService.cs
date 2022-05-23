using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Components;

namespace Turmerik.AspNetCore.Services
{
    public interface IMainLayoutService
    {
        UIBlockingOverlayViewModel UIBlockingOverlayViewModel { get; }

        event Action<bool> OnSideBarSizeChanged;
        event Action OnUIBlockingOverlayChanged;

        void SideBarSizeChanged(bool sideBarLarge);
        void EnableUIBlockingOverlay();
        void DisableUIBlockingOverlay();
        void SetError(ErrorViewModel errorViewModel);
        void SetApiBaseUriView(SetApiBaseUriViewModel setApiBaseUriViewModel);
        Task ExecuteWithUIBlockingOverlay(Func<UIBlockingOverlayViewModel, Task> action);
    }

    public class MainLayoutService : IMainLayoutService
    {
        private readonly ITrmrkAppSettings appSettings;

        private Action<bool>? onSideBarSizeChanged;
        private Action onUIBlockingOverlayChanged;

        public MainLayoutService(ITrmrkAppSettings appSettings)
        {
            appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));

            UIBlockingOverlayViewModel = new UIBlockingOverlayViewModel
            {
                ShowBackBtn = true
            };
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

        public void EnableUIBlockingOverlay()
        {
            UIBlockingOverlayViewModel.Error = null;
            UIBlockingOverlayViewModel.Enabled = true;

            onUIBlockingOverlayChanged?.Invoke();
        }

        public void DisableUIBlockingOverlay()
        {
            UIBlockingOverlayViewModel.Enabled = false;
            onUIBlockingOverlayChanged?.Invoke();
        }

        public async Task ExecuteWithUIBlockingOverlay(Func<UIBlockingOverlayViewModel, Task> action)
        {
            EnableUIBlockingOverlay();

            try
            {
                await action(UIBlockingOverlayViewModel);
            }
            catch (Exception exc)
            {
                UIBlockingOverlayViewModel.Enabled = true;

                UIBlockingOverlayViewModel.Error = new ErrorViewModel(
                    "An unhandled error ocurred",
                    exc, appSettings.IsDevMode);
            }

            onUIBlockingOverlayChanged?.Invoke();
        }

        public void SetError(ErrorViewModel errorViewModel)
        {
            UIBlockingOverlayViewModel.Error = errorViewModel;
            UIBlockingOverlayViewModel.Enabled = errorViewModel != null;

            onUIBlockingOverlayChanged?.Invoke();
        }

        public void SetApiBaseUriView(SetApiBaseUriViewModel setApiBaseUriViewModel)
        {
            UIBlockingOverlayViewModel.SetApiBaseUri = setApiBaseUriViewModel;
            UIBlockingOverlayViewModel.Enabled = setApiBaseUriViewModel != null;

            onUIBlockingOverlayChanged?.Invoke();
        }
    }
}
