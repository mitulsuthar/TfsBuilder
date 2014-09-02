using StructureMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using TfsBuilder.Lib;
using TfsBuilder.ViewModels;

namespace TfsBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {       
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Show();
        }
        private void ConfigureContainer()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.AssemblyContainingType<ITfsRepository>();
                    scan.AssemblyContainingType<MainWindow>();
                    scan.SingleImplementationsOfInterface();
                    scan.WithDefaultConventions();                    
                });

            });     
        }
        private void ComposeObjects()
        {

            IContainer container = ObjectFactory.Container;
            Application.Current.MainWindow = (Window)container.GetInstance<MainWindow>();
            Application.Current.MainWindow.Title = "Tfs Builder";            
        }
    }
}
