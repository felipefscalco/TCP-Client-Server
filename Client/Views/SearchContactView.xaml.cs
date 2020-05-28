using Client.ViewModels;
using System.Windows;

namespace Client.Views
{
    public partial class SearchContactView : Window
    {
        public SearchContactView()
        {
            InitializeComponent();

            if (DataContext is SearchContactViewModel viewmodel)
                viewmodel.Window = this;
        }
    }
}