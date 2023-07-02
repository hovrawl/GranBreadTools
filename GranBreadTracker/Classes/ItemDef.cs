using System.Text.Json.Serialization;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Classes;

public class ItemDef
{
    /// <summary>
    /// Unique Id
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Name of Item
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Item Description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// GranBreadIcon def that includes ItemKey and ItemType
    /// </summary>
    public GranBreadIcon Icon { get; set; }

    /// <summary>
    /// Get this item as a view model
    /// </summary>
    /// <returns></returns>
    public ItemDefDialogViewModel ToViewModel()
    {
        return new ItemDefDialogViewModel
        {
            Id = Id,
            Icon = Icon,
            Name = Name,
            Description = Description
        };
    }
}