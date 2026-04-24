using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_ProductManager_WithoutBinding.ViewModels.Base
{
    // Inherits ObservableObject so PropertyChanged is still raised —
    // the code-behind subscribes to it to update UI manually (no bindings)

    public partial class BaseViewModel : ObservableObject
    {
        
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private string _successMessage = string.Empty;
        public string SuccessMessage
        {
            get => _successMessage;
            set => SetProperty(ref _successMessage, value);
        }

        protected void SetError(string message)
        {
            ErrorMessage = message;
            SuccessMessage = string.Empty;
        }

        protected void SetSuccess(string message)
        {
            SuccessMessage = message;
            ErrorMessage = string.Empty;
        }

        protected void ClearMessages()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;
        }
    }
}
