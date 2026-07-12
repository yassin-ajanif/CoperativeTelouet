using System.ComponentModel;
using Avalonia.Controls;
using CoperativeTelouet.UI.ViewModels.Achat.Facturation;

namespace CoperativeTelouet.UI.Views.Achat.Facturation;

public partial class FacturationAchatDetailView : UserControl
{
    private FacturationAchatDetailViewModel? _vm;

    public FacturationAchatDetailView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) => AttachViewModel();
    }

    private void AttachViewModel()
    {
        if (_vm is not null)
            _vm.PropertyChanged -= OnVmPropertyChanged;

        _vm = DataContext as FacturationAchatDetailViewModel;
        if (_vm is null) return;

        _vm.PropertyChanged += OnVmPropertyChanged;
        ApplyColumnVisibility();
    }

    private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(FacturationAchatDetailViewModel.ShowColRef)
            or nameof(FacturationAchatDetailViewModel.ShowColDesignation)
            or nameof(FacturationAchatDetailViewModel.ShowColQte)
            or nameof(FacturationAchatDetailViewModel.ShowColPrix)
            or nameof(FacturationAchatDetailViewModel.ShowColRemise)
            or nameof(FacturationAchatDetailViewModel.ShowColTva)
            or nameof(FacturationAchatDetailViewModel.ShowColHt)
            or nameof(FacturationAchatDetailViewModel.ShowColTtc))
            ApplyColumnVisibility();
    }

    private void ApplyColumnVisibility()
    {
        if (_vm is null || LinesGrid.Columns.Count < 8) return;
        LinesGrid.Columns[0].IsVisible = _vm.ShowColRef;
        LinesGrid.Columns[1].IsVisible = _vm.ShowColDesignation;
        LinesGrid.Columns[2].IsVisible = _vm.ShowColQte;
        LinesGrid.Columns[3].IsVisible = _vm.ShowColPrix;
        LinesGrid.Columns[4].IsVisible = _vm.ShowColRemise;
        LinesGrid.Columns[5].IsVisible = _vm.ShowColTva;
        LinesGrid.Columns[6].IsVisible = _vm.ShowColHt;
        LinesGrid.Columns[7].IsVisible = _vm.ShowColTtc;
    }
}
