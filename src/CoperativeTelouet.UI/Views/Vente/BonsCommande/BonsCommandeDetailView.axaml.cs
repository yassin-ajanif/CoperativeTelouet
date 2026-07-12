using System.ComponentModel;
using Avalonia.Controls;
using CoperativeTelouet.UI.ViewModels.Vente.BonsCommande;

namespace CoperativeTelouet.UI.Views.Vente.BonsCommande;

public partial class BonsCommandeDetailView : UserControl
{
    private BonsCommandeDetailViewModel? _vm;

    public BonsCommandeDetailView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) => AttachViewModel();
    }

    private void AttachViewModel()
    {
        if (_vm is not null)
            _vm.PropertyChanged -= OnVmPropertyChanged;

        _vm = DataContext as BonsCommandeDetailViewModel;
        if (_vm is null) return;

        _vm.PropertyChanged += OnVmPropertyChanged;
        ApplyColumnVisibility();
    }

    private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BonsCommandeDetailViewModel.ShowColRef)
            or nameof(BonsCommandeDetailViewModel.ShowColDesignation)
            or nameof(BonsCommandeDetailViewModel.ShowColQte)
            or nameof(BonsCommandeDetailViewModel.ShowColPrix)
            or nameof(BonsCommandeDetailViewModel.ShowColRemise)
            or nameof(BonsCommandeDetailViewModel.ShowColTva)
            or nameof(BonsCommandeDetailViewModel.ShowColConditionnement)
            or nameof(BonsCommandeDetailViewModel.ShowColHt)
            or nameof(BonsCommandeDetailViewModel.ShowColTtc))
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
