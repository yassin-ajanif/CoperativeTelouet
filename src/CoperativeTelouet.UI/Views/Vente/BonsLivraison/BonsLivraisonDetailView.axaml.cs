using System.ComponentModel;
using Avalonia.Controls;
using CoperativeTelouet.UI.ViewModels.Vente.BonsLivraison;

namespace CoperativeTelouet.UI.Views.Vente.BonsLivraison;

public partial class BonsLivraisonDetailView : UserControl
{
    private BonsLivraisonDetailViewModel? _vm;

    public BonsLivraisonDetailView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) => AttachViewModel();
    }

    private void AttachViewModel()
    {
        if (_vm is not null)
            _vm.PropertyChanged -= OnVmPropertyChanged;

        _vm = DataContext as BonsLivraisonDetailViewModel;
        if (_vm is null) return;

        _vm.PropertyChanged += OnVmPropertyChanged;
        ApplyColumnVisibility();
    }

    private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BonsLivraisonDetailViewModel.ShowColRef)
            or nameof(BonsLivraisonDetailViewModel.ShowColDesignation)
            or nameof(BonsLivraisonDetailViewModel.ShowColQte)
            or nameof(BonsLivraisonDetailViewModel.ShowColPrix)
            or nameof(BonsLivraisonDetailViewModel.ShowColRemise)
            or nameof(BonsLivraisonDetailViewModel.ShowColTva)
            or nameof(BonsLivraisonDetailViewModel.ShowColConditionnement)
            or nameof(BonsLivraisonDetailViewModel.ShowColHt)
            or nameof(BonsLivraisonDetailViewModel.ShowColTtc))
            ApplyColumnVisibility();
    }

    private void ApplyColumnVisibility()
    {
        // Ref, Designation, Qté cmd (Conditionnement slot), Qté liv (Qte), Prix, Remise, TVA, HT, TTC
        if (_vm is null || LinesGrid.Columns.Count < 9) return;
        LinesGrid.Columns[0].IsVisible = _vm.ShowColRef;
        LinesGrid.Columns[1].IsVisible = _vm.ShowColDesignation;
        LinesGrid.Columns[2].IsVisible = _vm.ShowColConditionnement;
        LinesGrid.Columns[3].IsVisible = _vm.ShowColQte;
        LinesGrid.Columns[4].IsVisible = _vm.ShowColPrix;
        LinesGrid.Columns[5].IsVisible = _vm.ShowColRemise;
        LinesGrid.Columns[6].IsVisible = _vm.ShowColTva;
        LinesGrid.Columns[7].IsVisible = _vm.ShowColHt;
        LinesGrid.Columns[8].IsVisible = _vm.ShowColTtc;
    }
}
