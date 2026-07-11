using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CoperativeTelouet.Business;
using CoperativeTelouet.UI.ViewModels;
using CoperativeTelouet.UI.ViewModels.Vente.Avoirs;
using CoperativeTelouet.UI.ViewModels.Vente.BonsCommande;
using CoperativeTelouet.UI.ViewModels.Vente.BonsLivraison;
using CoperativeTelouet.UI.ViewModels.Vente.Clients;
using CoperativeTelouet.UI.ViewModels.Vente.Devis;
using CoperativeTelouet.UI.ViewModels.Vente.Facturation;
using CoperativeTelouet.UI.Views;
using CoperativeTelouet.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.UI;

public partial class App : Application
{
    private IServiceProvider? _services;
    private IServiceScope? _appScope;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        _services = BuildServiceProvider();
        await _services.GetRequiredService<IAppDatabaseInitializer>().InitializeAsync();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // One long-lived scope for the desktop session (single-user MVP).
            _appScope = _services.CreateScope();
            desktop.MainWindow = new MainWindow
            {
                DataContext = _appScope.ServiceProvider.GetRequiredService<MainViewModel>(),
            };
            desktop.ShutdownRequested += (_, _) => _appScope?.Dispose();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static IServiceProvider BuildServiceProvider()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var connectionString = configuration.GetConnectionString("Default")
            ?? "Data Source=coperative-telouet.db";

        // Store DB next to the executable for a simple single-user install.
        if (connectionString.StartsWith("Data Source=", StringComparison.OrdinalIgnoreCase))
        {
            var fileName = connectionString["Data Source=".Length..].Trim();
            if (!Path.IsPathRooted(fileName))
                connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, fileName)}";
        }

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddBusiness(connectionString);

        services.AddSingleton<IUserDialogService, UserDialogService>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<AccueilViewModel>();
        services.AddTransient<CategoriesViewModel>();
        services.AddTransient<ClientsViewModel>();
        services.AddTransient<ClientsListViewModel>();
        services.AddTransient<ClientDetailViewModel>();
        services.AddTransient<DevisViewModel>();
        services.AddTransient<BonsCommandeViewModel>();
        services.AddTransient<BonsLivraisonViewModel>();
        services.AddTransient<FacturationViewModel>();
        services.AddTransient<AvoirsViewModel>();

        return services.BuildServiceProvider();
    }
}
