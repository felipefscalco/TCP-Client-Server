using Common.Abstractions.Interfaces;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using Server.Extensions;
using Server.Handlers;
using Server.ViewModels;
using System.Windows;

namespace Server
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
            containerRegistry.Register<ITcpHandler, ServerTcpHandler>();

            containerRegistry.RegisterRepositorieDependencies();
        }

        protected override Window CreateShell()
            => new MainWindow();
    }
}