using ImagineDashboard.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ImagineDashboard.Views
{
    public sealed partial class ChargeDetailsView : UserControl
    {
        public PatientDetailsViewModel ViewModel { get; }

        public ChargeDetailsView()
        {
            this.InitializeComponent();
            Loaded += ChargeDetailsView_Loaded;
        }

        private void ChargeDetailsView_Loaded(object sender, RoutedEventArgs e)
        {
            //Tip.IsOpen = true;
        }

        public ChargeDetailsView(PatientDetailsViewModel viewModel) : this()
        {
            ViewModel = viewModel;
        }

        private void SearchInput_FocusEngaged(Control sender, FocusEngagedEventArgs args)
        {
            Tip.IsOpen = true;
        }

        private void SearchInput_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {
            Tip.IsOpen = true;
        }

        private void SearchInput_GotFocus(object sender, RoutedEventArgs e)
        {
            Tip.IsOpen = true;
        }

        private void SearchInput_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Tip.IsOpen = true;
        }

        private void SearchInput_LostFocus(object sender, RoutedEventArgs e)
        {
            Tip.IsOpen = false;
        }
    }
}
