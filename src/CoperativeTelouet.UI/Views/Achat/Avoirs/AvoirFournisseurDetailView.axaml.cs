using System.ComponentModel;
using Avalonia.Controls;
using CoperativeTelouet.UI.ViewModels.Achat.Avoirs;

namespace CoperativeTelouet.UI.Views.Achat.Avoirs;

public partial class AvoirFournisseurDetailView : UserControl
{
    private AvoirFournisseurDetailViewModel? _vm;

    public AvoirFournisseurDetailView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) => AttachViewModel();
    }

    private void AttachViewModel()
    {
        if (_vm is not null)
            _vm.PropertyChanged -= OnVmPropertyChanged;

        _vm = DataContext as AvoirFournisseurDetailViewModel;
        if (_vm is null) return;

        _vm.PropertyChanged += OnVmPropertyChanged;
        ApplyColumnVisibility();
    }

    private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(AvoirFournisseurDetailViewModel.ShowColRef)
            or nameof(AvoirFournisseurDetailViewModel.ShowColDesignation)
            or nameof(AvoirFournisseurDetailViewModel.ShowColQte)
            or nameof(AvoirFournisseurDetailViewModel.ShowColPrix)
            or nameof(AvoirFournisseurDetailViewModel.ShowColRemise)
            or nameof(AvoirFournisseurDetailViewModel.ShowColTva)
            or nameof(AvoirFournisseurDetailViewModel.ShowColHt)
            or nameof(AvoirFournisseurDetailViewModel.ShowColTtc))
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
