using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SCADA_App.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SprinklerPage : Page
    {

        #region Fields

        private ObservableCollection<API.Sproeier> _sproeierList;

        #endregion

        #region Properties

        public ObservableCollection<API.Sproeier> Sproeiers
        {
            get
            {
                return _sproeierList;
            }
            set
            {
                _sproeierList = value;
            }
        }

        #endregion

        #region Constructor

        public SprinklerPage()
        {
            this.InitializeComponent();


            Sproeiers = new ObservableCollection<API.Sproeier>();
        }

        #endregion

        #region Page Loaded
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            API.APIResponseSproeiers ResponseSproeiers = await API.APIWrapper.GetSproeiers();

            foreach (API.Sproeier sproeier in ResponseSproeiers.Sproeier)
            {
                Sproeiers.Add(sproeier);
            }
        }

        #endregion
    }
}
