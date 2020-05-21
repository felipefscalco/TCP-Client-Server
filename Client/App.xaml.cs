using Client.Handlers;
using Client.ViewModels;
using Client.Views;
using Common.Abstractions.Interfaces;
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
            ViewModelLocationProvider.Register<NewContactView, NewContactViewModel>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ITcpHandler, ClientTcpHandler>();
        }

        protected override Window CreateShell()
            => new MainWindow();
    }
}