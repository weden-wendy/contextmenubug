using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Media;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MultipleWindow
{
    public class WindowHelper : IWindowHelper
    {
        private int? _mainViewId;
        private int? _secondaryViewId;

        public void SetMainViewId(int mainViewId)
        {
            _mainViewId = mainViewId;
        }

        public async Task CreateSecondaryViewAsync()
        {
            if (_secondaryViewId != null)
            {
                return;
            }

            var coreView = CoreApplication.CreateNewView();

            await coreView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var frame = new Frame();

                Window.Current.Content = frame;
                Window.Current.Activate();

                var appView = ApplicationView.GetForCurrentView();
                if (appView != null && await ApplicationViewSwitcher.TryShowAsStandaloneAsync(appView.Id, ViewSizePreference.Default, (int)_mainViewId, ViewSizePreference.Default))
                {
                    _secondaryViewId = appView.Id;
                    await ApplicationViewSwitcher.SwitchAsync(appView.Id);

                    await coreView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        _ = frame?.Navigate(typeof(SecondaryPage), null, null);
                    });
                }
            });
        }

        public async Task OpenMainWindowAsync()
        {
            if (_mainViewId != null)
            {
                if (Window.Current.Dispatcher != null)
                {
                    await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync((int)_mainViewId))
                        {
                            await ApplicationViewSwitcher.SwitchAsync((int)_mainViewId);
                        }
                    });
                }
            }
        }
    }
}