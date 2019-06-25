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
    public sealed partial class LampPage : Page
    {

        #region Fields

        private ObservableCollection<API.Lamp> _lampList;

        #endregion

        #region Properties

        public ObservableCollection<API.Lamp> Lamps
        {
            get
            {
                return _lampList;
            }
            set
            {
                _lampList = value;
            }
        }

        #endregion

        #region Constructor

        public LampPage()
        {
            this.InitializeComponent();

            Lamps = new ObservableCollection<API.Lamp>();
        }

        #endregion

        #region Page Loaded
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            API.APIResponseLampen ResponseLampen = await API.APIWrapper.GetLampen();

            foreach (API.Lamp lamp in ResponseLampen.Lamp)
            {
                Lamps.Add(lamp);
            }
        }

        #endregion

        #region Individual click event for Grid items.


        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Dit wordt m niet helaas.
        }

        #endregion
    }
}
