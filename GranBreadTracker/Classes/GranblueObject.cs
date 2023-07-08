using FluentAvalonia.UI.Controls;

namespace GranBreadTracker.Classes;

public class GranblueObject
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public GranBreadIcon Icon { get; set; }
    
    public GranblueObjectType Type { get; set; }

    public GranblueObject(ItemDef itemDef)
    {
        Id = itemDef.Id;
        Name = itemDef.Name;
        Icon = itemDef.Icon;
        Type = GranblueObjectType.Item;
    }
    
    public GranblueObject(ItemSourceDef itemSourceDef)
    {
        Id = itemSourceDef.Id;
        Name = itemSourceDef.Name;
        Icon = itemSourceDef.Icon;
        Type = GranblueObjectType.Source;
    }
    
    public GranblueObject(ItemTrackerDef trackerDef)
    {
        Id = trackerDef.Id;
        Name = trackerDef.Name;
        Icon = trackerDef.Icon;
        Type = GranblueObjectType.Tracker;
    }
}