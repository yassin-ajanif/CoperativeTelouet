namespace CoperativeTelouet.UI.Services;

/// <summary>Column visibility prefs shared by all document line grids.</summary>
public sealed class DocumentColumnVisibility
{
    public bool ShowColRef { get; set; } = true;
    public bool ShowColDesignation { get; set; } = true;
    public bool ShowColQte { get; set; } = true;
    public bool ShowColPrix { get; set; } = true;
    public bool ShowColRemise { get; set; } = true;
    public bool ShowColTva { get; set; } = true;
    public bool ShowColConditionnement { get; set; }
    public bool ShowColHt { get; set; } = true;
    public bool ShowColTtc { get; set; } = true;
}
