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

namespace SimpleLogin_UI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            UsernameErrorMessage.Visibility = Visibility.Collapsed;
            PasswordErrorMessage.Visibility = Visibility.Collapsed;

            if (string.IsNullOrEmpty(username))
            {
                UsernameErrorMessage.Text = "Please enter Usernmame";
                UsernameErrorMessage.Visibility = Visibility.Visible;
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                PasswordErrorMessage.Text = "Please enter Password";
                PasswordErrorMessage.Visibility = Visibility.Visible;
                return;
            }
            if (username.Length < 3)
            {
                UsernameErrorMessage.Text = "Username must be atleast 3 characters long";
                UsernameErrorMessage.Visibility = Visibility.Visible;
                return;
            }
            if (password.Length < 6)
            {
                PasswordErrorMessage.Text = "Password must be atleast 6 characters long";
                PasswordErrorMessage.Visibility = Visibility.Visible;
                return;
            }


            ContentDialog dialog = new ContentDialog()
            {
                Title = "Login Successful",
                Content = $"Welcome, {username}!",
                CloseButtonText = "Ok",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();

            UsernameTextBox.Text = "";
            PasswordBox.Password = "";
        }
    }
}
