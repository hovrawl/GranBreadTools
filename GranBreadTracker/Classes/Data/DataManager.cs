using System.Collections.Generic;
using System.Linq;
using FluentAvalonia.UI.Controls;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GranBreadTracker.Classes.Data;

public sealed class DataManager
{
    private static readonly GranBreadItems _items = new();
    private static readonly GranBreadItemSources _itemSources = new();
    private static readonly GranBreadTrackers _itemTrackerDefs = new();

    private const string ItemsPath = "Data/Items.json";
    private const string ItemSourcesPath = "Data/Sources.json";
    private const string TrackerPath = "Data/Tracker.json";
    
    private static readonly DataManager Instance = new();

    public static void Initialise()
    {
        _items.Initialise(ItemsPath);
        _itemSources.Initialise(ItemSourcesPath);
        _itemTrackerDefs.Initialise(TrackerPath);
        
        // Ensure each item is loaded initialised
        var items = _items.All();
        var sources = _itemSources.All();
        var trackers = _itemTrackerDefs.All();

        foreach (var item in items)
        {
            // Populate the iconSource for each item
            if(item.Icon == null || string.IsNullOrEmpty(item.Icon.IconKey)) continue;
            if (!App.Current.Resources.TryGetResource(item.Icon.IconKey, null, out var icon)) continue;
            if (icon is not ImageIconSource iconSource) continue;
            item.Icon.IconSource = iconSource;
        }

        foreach (var source in sources)
        {
            // Populate the iconSource for each source
            if(source.Icon == null || string.IsNullOrEmpty(source.Icon.IconKey)) continue;
            if (!App.Current.Resources.TryGetResource(source.Icon.IconKey, null, out var icon)) continue;
            if (icon is not ImageIconSource iconSource) continue;
            source.Icon.IconSource = iconSource;
        }
            
        foreach (var tracker in trackers)
        {
            // Populate the iconSource for each source
            if(tracker.Icon == null || string.IsNullOrEmpty(tracker.Icon.IconKey)) continue;
            if (!App.Current.Resources.TryGetResource(tracker.Icon.IconKey, null, out var icon)) continue;
            if (icon is not ImageIconSource iconSource) continue;
            tracker.Icon.IconSource = iconSource;

            var outdatedSourceIds = new List<string>();
                
            foreach (var sourceId in tracker.SourceIds)
            {
                var foundSource = sources.FirstOrDefault(i => i.Id.Equals(sourceId));
                if (foundSource == null)
                {
                    outdatedSourceIds.Add(sourceId);
                    continue;
                }
                    
                tracker.Sources.Add(foundSource);
            }
        }
    }

    public static GranBreadItems Items()
    {
        return _items;
    }
    
    public static GranBreadItemSources ItemSources()
    {
        return _itemSources;
    }
    
    public static GranBreadTrackers ItemTrackerDefs()
    {
        return _itemTrackerDefs;
    }
}