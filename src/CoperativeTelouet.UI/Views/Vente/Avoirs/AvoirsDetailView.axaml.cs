using System.ComponentModel;
using Avalonia.Controls;
using CoperativeTelouet.UI.ViewModels.Vente.Avoirs;

namespace CoperativeTelouet.UI.Views.Vente.Avoirs;

public partial class AvoirsDetailView : UserControl
{
    private AvoirsDetailViewModel? _vm;

    public AvoirsDetailView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) => AttachViewModel();
    }

    private void AttachViewModel()
    {
        if (_vm is not null)
            _vm.PropertyChanged -= OnVmPropertyChanged;

        _vm = DataContext as AvoirsDetailViewModel;
        if (_vm is null) return;

        _vm.PropertyChanged += OnVmPropertyChanged;
        ApplyColumnVisibility();
    }

    private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(AvoirsDetailViewModel.ShowColRef)
            or nameof(AvoirsDetailViewModel.ShowColDesignation)
            or nameof(AvoirsDetailViewModel.ShowColQte)
            or nameof(AvoirsDetailViewModel.ShowColPrix)
            or nameof(AvoirsDetailViewModel.ShowColRemise)
            or nameof(AvoirsDetailViewModel.ShowColTva)
            or nameof(AvoirsDetailViewModel.ShowColConditionnement)
            or nameof(AvoirsDetailViewModel.ShowColHt)
            or nameof(AvoirsDetailViewModel.ShowColTtc))
            ApplyColumnVisibility();
    }

    private void ApplyColumnVisibility()
    {
        if (_vm is null || LinesGrid.Columns.Count < 9) return;
        LinesGrid.Columns[0].IsVisible = _vm.ShowColRef;
        LinesGrid.Columns[1].IsVisible = _vm.ShowColDesignation;
        LinesGrid.Columns[2].IsVisible = _vm.ShowColQte;
        LinesGrid.Columns[3].IsVisible = _vm.ShowColPrix;
        LinesGrid.Columns[4].IsVisible = _vm.ShowColRemise;
        LinesGrid.Columns[5].IsVisible = _vm.ShowColTva;
        LinesGrid.Columns[6].IsVisible = _vm.ShowColConditionnement;
        LinesGrid.Columns[7].IsVisible = _vm.ShowColHt;
        LinesGrid.Columns[8].IsVisible = _vm.ShowColTtc;
    }
}
