using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Reflection;
using System.Waf.Applications;
using System.Windows;
using System.Windows.Threading;
using SvgToPixelIcon.Model.Controllers;

namespace SvgToPixelIcon.Presentation
{
            
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AggregateCatalog _catalog;
        private CompositionContainer _container;
        private IApplicationController _controller;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            base.ShutdownMode = ShutdownMode.OnLastWindowClose;
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnDomainUnhandledException;
            
            _catalog = new AggregateCatalog();
            // Add the WpfApplicationFramework assembly to the catalog
            _catalog.Catalogs.Add(new AssemblyCatalog(typeof(ViewModel).Assembly));
            // Add the Writer.Presentation assembly to the catalog
            _catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            // Add the Writer.Applications assembly to the catalog
            _catalog.Catalogs.Add(new AssemblyCatalog(typeof(IApplicationController).Assembly));

            _container = new CompositionContainer(_catalog, CompositionOptions.DisableSilentRejection);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(_container);
            _container.Compose(batch);

            _controller = _container.GetExportedValue<IApplicationController>();
            _controller.Initialize();
            _controller.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _controller.Shutdown();
            _container.Dispose();
            _catalog.Dispose();

            base.OnExit(e);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception, false);
        }

        private void OnDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception, e.IsTerminating);
        }

        private void HandleException(Exception exception, bool isTerminating)
        {
            if (exception == null) { return; }

            Trace.TraceError(exception.ToString());

            if (!isTerminating)
            {
                MessageBox.Show(exception.Message, ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
