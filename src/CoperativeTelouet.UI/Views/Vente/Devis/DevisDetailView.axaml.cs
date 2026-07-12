using System.ComponentModel;
using Avalonia.Controls;
using CoperativeTelouet.UI.ViewModels.Vente.Devis;

namespace CoperativeTelouet.UI.Views.Vente.Devis;

public partial class DevisDetailView : UserControl
{
    private DevisDetailViewModel? _vm;

    public DevisDetailView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) => AttachViewModel();
    }

    private void AttachViewModel()
    {
        if (_vm is not null)
            _vm.PropertyChanged -= OnVmPropertyChanged;

        _vm = DataContext as DevisDetailViewModel;
        if (_vm is null) return;

        _vm.PropertyChanged += OnVmPropertyChanged;
        ApplyColumnVisibility();
    }

    private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(DevisDetailViewModel.ShowColRef)
            or nameof(DevisDetailViewModel.ShowColDesignation)
            or nameof(DevisDetailViewModel.ShowColQte)
            or nameof(DevisDetailViewModel.ShowColPrix)
            or nameof(DevisDetailViewModel.ShowColRemise)
            or nameof(DevisDetailViewModel.ShowColTva)
            or nameof(DevisDetailViewModel.ShowColHt)
            or nameof(DevisDetailViewModel.ShowColTtc))
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
