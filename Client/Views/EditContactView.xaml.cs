using Client.ViewModels;
using Common.Models;
using System.Windows;

namespace Client.Views
{
    public partial class EditContactView : Window
    {
        public EditContactView(Contact contact)
        {
            InitializeComponent();

            if (DataContext is EditContactViewModel viewmodel)
            {
                viewmodel.Window = this;
                viewmodel.SetContactDetails(contact);
            }
        }
    }
}