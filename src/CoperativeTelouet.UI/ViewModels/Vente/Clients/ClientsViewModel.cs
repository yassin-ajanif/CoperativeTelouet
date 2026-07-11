using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.UI.ViewModels.Vente.Clients;

/// <summary>Host: switches between client list and client detail.</summary>
public partial class ClientsViewModel : ViewModelBase
{
    private readonly IServiceProvider _services;

    [ObservableProperty]
    private ViewModelBase _content = null!;

    public ClientsViewModel(IServiceProvider services)
    {
        _services = services;
        ShowList();
    }

    public void ShowList()
    {
        var list = _services.GetRequiredService<ClientsListViewModel>();
        list.AttachHost(this);
        Content = list;
        _ = list.LoadAsync();
    }

    public void ShowDetail(int? clientId)
    {
        var detail = _services.GetRequiredService<ClientDetailViewModel>();
        detail.AttachHost(this);
        Content = detail;
        _ = detail.LoadAsync(clientId);
    }
}
