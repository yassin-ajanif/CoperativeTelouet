using System.ComponentModel;
using Avalonia.Controls;
using CoperativeTelouet.UI.ViewModels.Achat.Devis;

namespace CoperativeTelouet.UI.Views.Achat.Devis;

public partial class DevisAchatDetailView : UserControl
{
    private DevisAchatDetailViewModel? _vm;

    public DevisAchatDetailView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) => AttachViewModel();
    }

    private void AttachViewModel()
    {
        if (_vm is not null)
            _vm.PropertyChanged -= OnVmPropertyChanged;

        _vm = DataContext as DevisAchatDetailViewModel;
        if (_vm is null) return;

        _vm.PropertyChanged += OnVmPropertyChanged;
        ApplyColumnVisibility();
    }

    private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(DevisAchatDetailViewModel.ShowColRef)
            or nameof(DevisAchatDetailViewModel.ShowColDesignation)
            or nameof(DevisAchatDetailViewModel.ShowColQte)
            or nameof(DevisAchatDetailViewModel.ShowColPrix)
            or nameof(DevisAchatDetailViewModel.ShowColRemise)
            or nameof(DevisAchatDetailViewModel.ShowColTva)
            or nameof(DevisAchatDetailViewModel.ShowColConditionnement)
            or nameof(DevisAchatDetailViewModel.ShowColHt)
            or nameof(DevisAchatDetailViewModel.ShowColTtc))
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
