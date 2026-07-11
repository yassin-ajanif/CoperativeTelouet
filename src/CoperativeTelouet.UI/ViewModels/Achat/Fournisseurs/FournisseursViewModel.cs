using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.UI.ViewModels.Achat.Fournisseurs;

/// <summary>Host: switches between fournisseur list and fournisseur detail.</summary>
public partial class FournisseursViewModel : ViewModelBase
{
    private readonly IServiceProvider _services;

    [ObservableProperty]
    private ViewModelBase _content = null!;

    public FournisseursViewModel(IServiceProvider services)
    {
        _services = services;
        ShowList();
    }

    public void ShowList()
    {
        var list = _services.GetRequiredService<FournisseursListViewModel>();
        list.AttachHost(this);
        Content = list;
        _ = list.LoadAsync();
    }

    public void ShowDetail(int? fournisseurId)
    {
        var detail = _services.GetRequiredService<FournisseurDetailViewModel>();
        detail.AttachHost(this);
        Content = detail;
        _ = detail.LoadAsync(fournisseurId);
    }
}
