using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.UI.ViewModels.Achat.BonsCommande;

/// <summary>Host: switches between bons commande achat list and detail.</summary>
public partial class BonsCommandeAchatViewModel : ViewModelBase
{
    private readonly IServiceProvider _services;

    [ObservableProperty]
    private ViewModelBase _content = null!;

    public BonsCommandeAchatViewModel(IServiceProvider services)
    {
        _services = services;
        ShowList();
    }

    public void ShowList()
    {
        var list = _services.GetRequiredService<BonsCommandeAchatListViewModel>();
        list.AttachHost(this);
        Content = list;
        _ = list.LoadAsync();
    }

    public void ShowDetail(int? id)
    {
        var detail = _services.GetRequiredService<BonsCommandeAchatDetailViewModel>();
        detail.AttachHost(this);
        Content = detail;
        _ = detail.LoadAsync(id);
    }
}
