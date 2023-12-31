﻿using System.Collections.Generic;
using System.Linq;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Styling;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GranBreadTracker.Classes.Data;

public sealed class DataManager
{
    private static readonly GranBreadGoals _goals = new();
    private static readonly GranBreadItems _items = new();
    private static readonly GranBreadItemSources _itemSources = new();
    private static readonly GranBreadTrackers _itemTrackerDefs = new();

    private const string ItemsPath = "Data/Items.json";
    private const string ItemSourcesPath = "Data/Sources.json";
    private const string TrackerPath = "Data/Tracker.json";
    private const string GoalsPath = "Data/Goals.json";
    
    private static readonly DataManager Instance = new();

    public static void Initialise()
    {
        _items.Initialise(ItemsPath);
        _itemSources.Initialise(ItemSourcesPath);
        _itemTrackerDefs.Initialise(TrackerPath);
        _goals.Initialise(GoalsPath);
        
        // Ensure each item is loaded initialised
        var items = _items.All();
        var sources = _itemSources.All();
        var trackers = _itemTrackerDefs.All();
        var goals = _goals.All();

        foreach (var item in items)
        {
            // Populate the iconSource for each item
            if(item.Icon == null || string.IsNullOrEmpty(item.Icon.IconKey)) continue;
            if (!App.Current.Resources.TryGetResource(item.Icon.IconKey, null, out var icon)) continue;
            if (icon is not GranBreadIconSource iconSource) continue;
            item.Icon.IconSource = iconSource;
        }

        foreach (var source in sources)
        {
            // Populate the iconSource for each source
            if(source.Icon == null || string.IsNullOrEmpty(source.Icon.IconKey)) continue;
            if (!App.Current.Resources.TryGetResource(source.Icon.IconKey, null, out var icon)) continue;
            if (icon is not GranBreadIconSource iconSource) continue;
            source.Icon.IconSource = iconSource;
        }
            
        foreach (var tracker in trackers)
        {
            // Populate the iconSource for each source
            if(tracker.Icon == null || string.IsNullOrEmpty(tracker.Icon.IconKey)) continue;
            if (!App.Current.Resources.TryGetResource(tracker.Icon.IconKey, null, out var icon)) continue;
            if (icon is not GranBreadIconSource iconSource) continue;
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
        
        foreach (var goal in goals)
        {
            // Populate the iconSource for each source
            if(goal.Icon == null || string.IsNullOrEmpty(goal.Icon.IconKey)) continue;
            if (!App.Current.Resources.TryGetResource(goal.Icon.IconKey, null, out var icon)) continue;
            if (icon is not GranBreadIconSource iconSource) continue;
            goal.Icon.IconSource = iconSource;

            var outdatedItemCounts = new List<string>();
                
            foreach (var itemPair in goal.Items)
            {
                var foundItem = items.FirstOrDefault(i => i.Id.Equals(itemPair.Key));
                if (foundItem == null)
                {
                    outdatedItemCounts.Add(itemPair.Key);
                    continue;
                }
                    
                //goal.Items.Add(foundSource);
            }

            foreach (var itemId in outdatedItemCounts)
            {
                if(!goal.Items.ContainsKey(itemId)) continue;
                
                goal.Items.Remove(itemId);
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
    
    public static GranBreadGoals Goals()
    {
        return _goals;
    }
}