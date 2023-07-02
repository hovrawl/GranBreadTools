using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GranBreadTracker.Classes.Data;

public sealed class DataManager
{
    private static GranBreadItems _items = new();
    private static GranBreadItemSources _itemSources = new();
    private static GranBreadTrackers _itemTrackerDefs = new();

    private const string ItemsPath = "Data/Items.json";
    private const string ItemSourcesPath = "Data/Sources.json";
    private const string TrackerPath = "Data/Tracker.json";
    
    private static readonly DataManager Instance = new();

    public static void Initialise()
    {
        _items.Initialise(ItemsPath);
        _itemSources.Initialise(ItemSourcesPath);
        _itemTrackerDefs.Initialise(TrackerPath);
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