using Client.ViewModels;
using Common.Abstractions.Interfaces;
using Common.Models.Tcp;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System.Windows;

namespace Client
{
    public partial class App : PrismApplication
    {
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ITcpHandler, ClientTcpHandler>();
        }

        protected override Window CreateShell()
            => new MainWindow();
    }
}