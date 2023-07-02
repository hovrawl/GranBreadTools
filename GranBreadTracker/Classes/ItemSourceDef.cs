﻿using System.Text.Json.Serialization;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Classes;

public class ItemSourceDef
{
    /// <summary>
    /// Name of Source
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
    public SourceDefDialogViewModel ToViewModel()
    {
        return new SourceDefDialogViewModel()
        {
            Icon = Icon,
            Name = Name,
            Description = Description
        };
    }
}