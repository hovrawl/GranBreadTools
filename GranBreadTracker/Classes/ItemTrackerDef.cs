﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text.Json.Serialization;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Classes;

public class ItemTrackerDef
{
    /// <summary>
    /// Unique Id
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Name of Item Tracker
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Description of Item Tracker
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// GranBreadIcon def that includes ItemKey and ItemType
    /// </summary>
    public GranBreadIcon Icon { get; set; }

    /// <summary>
    /// List of the Ids of the Sources saved into this tracker
    /// </summary>
    public List<string> SourceIds { get; set; } = new ();
    
    [JsonIgnore]
    public ObservableCollection<ItemSourceDef> Sources { get; set; } = new ();

    public List<ItemSourcePageViewModel> GenerateSourceViewModels()
    {
        var returnList = new List<ItemSourcePageViewModel>();

        foreach (var source in Sources)
        {
            var vm = new ItemSourcePageViewModel(source);
            returnList.Add(vm);
        }
        
        return returnList;
    }
    
    /// <summary>
    /// Get this item as a view model
    /// </summary>
    /// <returns></returns>
    public ItemTrackerPageViewModel ToViewModel()
    {
        return new ItemTrackerPageViewModel(this);
    }
}