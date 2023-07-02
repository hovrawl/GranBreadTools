namespace GranBreadTracker.Classes.Data;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IGranBreadTrackers
{
    IGranBreadTrackers Initialise(string dataPath);
    IGranBreadTrackers Items();
    IGranBreadTrackers Save();
    IGranBreadTrackers Add(ItemTrackerDef itemDef);
    IGranBreadTrackers Remove(ItemTrackerDef itemDef);
    ItemTrackerDef FindById(string id);
    List<ItemTrackerDef> All();
}

public class GranBreadTrackers : IGranBreadTrackers
{
    private List<ItemTrackerDef> ItemTrackerDefs { get; set; }
    
    private string DataPath { get; set; }
    
    public IGranBreadTrackers Initialise(string dataPath)
    {
        ItemTrackerDefs = new List<ItemTrackerDef>();

        DataPath = dataPath;
        
        if (File.Exists(dataPath))
        {
            try
            {
                var dataText = File.ReadAllText(dataPath);
                var deserializedData = JsonSerializer.Deserialize<List<ItemTrackerDef>>(dataText);
                ItemTrackerDefs.AddRange(deserializedData);
            } catch{/**/}
        }
        
        return this;
    }
    
    public IGranBreadTrackers Items()
    {
        return this;
    }

    public ItemTrackerDef FindById(string id)
    {
        return ItemTrackerDefs.Find(i => i.Name.Equals(id));
    }

    public List<ItemTrackerDef> All()
    {
        return ItemTrackerDefs;
    }

    public IGranBreadTrackers Save()
    {
        try
        {
            var directory = new FileInfo(DataPath).Directory.FullName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var serializedData= JsonSerializer.Serialize(ItemTrackerDefs);
            File.WriteAllText(DataPath, serializedData);
        }
        catch
        { /**/ }

        return this;
    }
    
    public IGranBreadTrackers Add(ItemTrackerDef itemDef)
    {
        ItemTrackerDefs.RemoveAll(i => i.Name.Equals(itemDef.Name));
        ItemTrackerDefs.Add(itemDef);
        return this;
    }
    
    public IGranBreadTrackers Remove(ItemTrackerDef itemDef)
    {
        ItemTrackerDefs.RemoveAll(i => i.Name.Equals(itemDef.Name));
        return this;
    }

    
}