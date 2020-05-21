using Client.ViewModels;
using System.Windows;

namespace Client.Views
{
    public partial class NewContactView : Window
    {
        public NewContactView()
        {
            InitializeComponent();

            if (DataContext is NewContactViewModel viewmodel)
                viewmodel.Window = this;
        }
    }
}
