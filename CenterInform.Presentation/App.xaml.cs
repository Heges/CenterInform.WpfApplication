using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using layerApplication = CenterInform.Application;
using System.Windows;
using CenterInform.Application;
using CenterInform.Infrastructure;
using CenterInform.Serializator;

namespace CenterInform.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IHost AppHost { get; set; }

        //private IConfiguration Configuration { get; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplication();
                    services.AddPersistence();
                    services.AddSingleton<MainWindow>();
                    services.AddJsonSerializer();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();
            using (var scope = AppHost.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<EmployeDbContext>();
                    DbInitializer.Initialize(context);
                    FolderInitializer.Initialize("G:\\f\\TMP\\c#\\CenterInform.Solution\\SerializationFolder");
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
            

            var window = AppHost.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }
}
