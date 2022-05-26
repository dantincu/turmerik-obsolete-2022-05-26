using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Components;
using Turmerik.AspNetCore.Infrastructure;

namespace Turmerik.AspNetCore.Services
{
    public interface IMainLayoutService
    {
        UIBlockingOverlayViewModel UIBlockingOverlayViewModel { get; }

        event Action<bool> OnSideBarSizeChanged;
        event Action OnUIBlockingOverlayChanged;
        event Func<TextEventArgsWrapper, Task> OnApiBaseUriSet;

        void SideBarSizeChanged(bool sideBarLarge);
        void EnableUIBlockingOverlay();
        void DisableUIBlockingOverlay();
        void SetError(ErrorViewModel errorViewModel);
        void SetApiBaseUriView(SetApiBaseUriViewModel setApiBaseUriViewModel, bool? keepUIBlockingOverlay = null);
        Task ApiBaseUriSet(TextEventArgsWrapper args);
        Task ExecuteWithUIBlockingOverlay(Func<UIBlockingOverlayViewModel, Task> action);
    }

    public class MainLayoutService : IMainLayoutService
    {
        private readonly ITrmrkAppSettings appSettings;

        private Action<bool>? onSideBarSizeChanged;
        private Action onUIBlockingOverlayChanged;
        private Func<TextEventArgsWrapper, Task> onApiBaseUriSet;

        public MainLayoutService(ITrmrkAppSettings appSettings)
        {
            this.appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));

            UIBlockingOverlayViewModel = new UIBlockingOverlayViewModel
            {
                ShowBackBtn = true
            };
        }

        public UIBlockingOverlayViewModel UIBlockingOverlayViewModel { get; }

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

        public event Func<TextEventArgsWrapper, Task> OnApiBaseUriSet
        {
            add
            {
                onApiBaseUriSet += value;
            }

            remove
            {
                onApiBaseUriSet -= value;
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

        public void SetApiBaseUriView(SetApiBaseUriViewModel setApiBaseUriViewModel, bool? keepUIBlockingOverlay = null)
        {
            bool uiBlockingOverlayEnabled = keepUIBlockingOverlay ?? setApiBaseUriViewModel != null;
            UIBlockingOverlayViewModel.Enabled = uiBlockingOverlayEnabled;

            UIBlockingOverlayViewModel.SetApiBaseUri = setApiBaseUriViewModel;
            onUIBlockingOverlayChanged?.Invoke();
        }

        public async Task ApiBaseUriSet(TextEventArgsWrapper args)
        {
            await onApiBaseUriSet.InvokeTextEventAsyncIfReq(args);
        }
    }
}
