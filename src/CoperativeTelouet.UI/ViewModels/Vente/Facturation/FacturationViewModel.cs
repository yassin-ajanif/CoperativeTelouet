using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.UI.ViewModels.Vente.Facturation;

/// <summary>Host: switches between facturation list and detail.</summary>
public partial class FacturationViewModel : ViewModelBase
{
    private readonly IServiceProvider _services;

    [ObservableProperty]
    private ViewModelBase _content = null!;

    public FacturationViewModel(IServiceProvider services)
    {
        _services = services;
        ShowList();
    }

    public void ShowList()
    {
        var list = _services.GetRequiredService<FacturationListViewModel>();
        list.AttachHost(this);
        Content = list;
        _ = list.LoadAsync();
    }

    public void ShowDetail(int? factureId)
    {
        var detail = _services.GetRequiredService<FacturationDetailViewModel>();
        detail.AttachHost(this);
        Content = detail;
        _ = detail.LoadAsync(factureId);
    }
}
