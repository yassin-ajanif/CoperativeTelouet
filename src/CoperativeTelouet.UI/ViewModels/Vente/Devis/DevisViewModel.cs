using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.UI.ViewModels.Vente.Devis;

/// <summary>Host: switches between devis list and devis detail.</summary>
public partial class DevisViewModel : ViewModelBase
{
    private readonly IServiceProvider _services;

    [ObservableProperty]
    private ViewModelBase _content = null!;

    public DevisViewModel(IServiceProvider services)
    {
        _services = services;
        ShowList();
    }

    public void ShowList()
    {
        var list = _services.GetRequiredService<DevisListViewModel>();
        list.AttachHost(this);
        Content = list;
        _ = list.LoadAsync();
    }

    public void ShowDetail(int? devisId)
    {
        var detail = _services.GetRequiredService<DevisDetailViewModel>();
        detail.AttachHost(this);
        Content = detail;
        _ = detail.LoadAsync(devisId);
    }
}
