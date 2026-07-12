using System.ComponentModel;
using Avalonia.Controls;
using CoperativeTelouet.UI.ViewModels.Achat.BonsReception;

namespace CoperativeTelouet.UI.Views.Achat.BonsReception;

public partial class BonsReceptionDetailView : UserControl
{
    private BonsReceptionDetailViewModel? _vm;

    public BonsReceptionDetailView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) => AttachViewModel();
    }

    private void AttachViewModel()
    {
        if (_vm is not null)
            _vm.PropertyChanged -= OnVmPropertyChanged;

        _vm = DataContext as BonsReceptionDetailViewModel;
        if (_vm is null) return;

        _vm.PropertyChanged += OnVmPropertyChanged;
        ApplyColumnVisibility();
    }

    private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BonsReceptionDetailViewModel.ShowColRef)
            or nameof(BonsReceptionDetailViewModel.ShowColDesignation)
            or nameof(BonsReceptionDetailViewModel.ShowColQte)
            or nameof(BonsReceptionDetailViewModel.ShowColPrix)
            or nameof(BonsReceptionDetailViewModel.ShowColTva)
            or nameof(BonsReceptionDetailViewModel.ShowColHt)
            or nameof(BonsReceptionDetailViewModel.ShowColTtc))
            ApplyColumnVisibility();
    }

    private void ApplyColumnVisibility()
    {
        if (_vm is null || LinesGrid.Columns.Count < 7) return;
        LinesGrid.Columns[0].IsVisible = _vm.ShowColRef;
        LinesGrid.Columns[1].IsVisible = _vm.ShowColDesignation;
        LinesGrid.Columns[2].IsVisible = _vm.ShowColQte;
        LinesGrid.Columns[3].IsVisible = _vm.ShowColPrix;
        LinesGrid.Columns[4].IsVisible = _vm.ShowColTva;
        LinesGrid.Columns[5].IsVisible = _vm.ShowColHt;
        LinesGrid.Columns[6].IsVisible = _vm.ShowColTtc;
    }
}
