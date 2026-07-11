using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IServiceProvider _services;

    [ObservableProperty]
    private ViewModelBase _currentPage = null!;

    [ObservableProperty]
    private string _statusMessage = "Prêt";

    public MainViewModel(IServiceProvider services)
    {
        _services = services;
        CurrentPage = _services.GetRequiredService<CategoriesViewModel>();
    }

    [RelayCommand]
    private void NavigateToCategories()
    {
        CurrentPage = _services.GetRequiredService<CategoriesViewModel>();
        StatusMessage = "Catégories";
    }
}
