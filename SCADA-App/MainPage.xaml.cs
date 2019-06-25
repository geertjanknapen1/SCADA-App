using SCADA_App.Common;
using SDKTemplate.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SCADA_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Fields

        RootFrameNavigationHelper _rfnh;
        Dictionary<string, Type> _dstPages;
        Dictionary<Type, string> _dtsPageTags;

        #endregion

        #region Constructor
        public MainPage()
        {
            this.InitializeComponent();

            // Register the view frame with the SuspensionManager.
            SuspensionManager.RegisterFrame(fRootFrame, "rootFrame");

            // Initialize the RootFrameNavigationHandler to handle hardware navigation.
            _rfnh = new RootFrameNavigationHelper(fRootFrame);

            // Create dictionary that can be used to get page type by tag content.
            _dstPages = new Dictionary<string, Type>
            {
                { "Nav_Sprinklers", typeof(Pages.SprinklerPage) },
                { "Nav_Lamps", typeof(Pages.LampPage) }
            };

            // Initialize a dictionary that can be used to get the tag content by page type.
            _dtsPageTags = new Dictionary<Type, string>();

            // Fill dictionary with the key and value of the _dstPages switched.
            foreach (KeyValuePair<string, Type> kvpstItem in _dstPages)
            {
                _dtsPageTags.Add(kvpstItem.Value, kvpstItem.Key);
            }
        }

        #endregion

        #region Navigation
        private void NvNavigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock tbMenuItem = args.InvokedItem as TextBlock;

            if (tbMenuItem != null)
            {
                string sNavigationItemTag = tbMenuItem.Tag as string;

                if (!string.IsNullOrWhiteSpace(sNavigationItemTag))
                {
                    // Navigate to correct page by getting the type from the Pages dictionary (_dstPages)
                    // dictionary using the tag retrieved earlier as the key
                    fRootFrame.Navigate(_dstPages[sNavigationItemTag]);
                }
            }
        }

        private void FRootFrame_Loaded(object sender, RoutedEventArgs e)
        {
            // Check if the root frame currently has a page open, if so then the app was not restored
            if (fRootFrame.SourcePageType == null)
            {
                // Navigate to the start page loaded from  the local settings (or default to the home page when not set yet)
                switch ((StartPageOptions)(ApplicationData.Current.LocalSettings.Values["appUserSettingsStartPage"] ?? StartPageOptions.Homepage))
                {
                    case StartPageOptions.LampPage:
                        fRootFrame.Navigate(typeof(Pages.LampPage));
                        break;
                    case StartPageOptions.SprinklerPage:
                        fRootFrame.Navigate(typeof(Pages.SprinklerPage));
                        break;
                    case StartPageOptions.Homepage:
                        fRootFrame.Navigate(typeof(Pages.Homepage));
                        break;
                    default:
                        fRootFrame.Navigate(typeof(Pages.Homepage));
                        break;
                }
            }
            // Update navigationview to match the restored page.
            else
            {
                UpdateNavigationView(fRootFrame.CurrentSourcePageType);
            }
        }

        private void fRootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateNavigationView(e.SourcePageType);
        }

        #endregion

        #region Helpers

        private void UpdateNavigationView(Type tPageType)
        {
            string sNavigationItemTag;

            // Retrieve the tag value of the navigation item  from the Page Tags (_dtsPageTags)
            // dictionary using the page type that is passed in the event arguments
            if (_dtsPageTags.TryGetValue(tPageType, out sNavigationItemTag))
            {
                // Loop trough all navigation view items to determine which item to activate
                foreach (NavigationViewItemBase nvibItem in nvNavigation.MenuItems)
                {
                    TextBlock tbMenuItem = nvibItem.Content as TextBlock;

                    // Check if the tag from the dictionary matches the current menu item tag
                    if (tbMenuItem != null && tbMenuItem.Tag != null && tbMenuItem.Tag.Equals(sNavigationItemTag))
                    {
                        // Change the selected item to current item
                        nvNavigation.SelectedItem = nvibItem;

                        // Change the navigationview header to the text in the menu item
                        nvNavigation.Header = tbMenuItem.Text;

                        // Stop foreach since menu item was found
                        break;
                    }
                }
            }
        }

        #endregion
    }
}
