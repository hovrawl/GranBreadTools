namespace GranBreadTracker.Classes.Data;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IGranBreadItemSources
{
    IGranBreadItemSources Initialise(string dataPath);

    IGranBreadItemSources Sources();
    IGranBreadItemSources Save();
    IGranBreadItemSources Add(ItemSourceDef itemDef);
    IGranBreadItemSources Remove(ItemSourceDef itemDef);

    ItemSourceDef FindById(string id);
    
    List<ItemSourceDef> All();
    
    
}

public class GranBreadItemSources : IGranBreadItemSources
{
    private List<ItemSourceDef> ItemSourceDefs { get; set; }
    
    private string DataPath { get; set; }
    
    public IGranBreadItemSources Initialise(string dataPath)
    {
        ItemSourceDefs = new List<ItemSourceDef>();

        DataPath = dataPath;
        
        if (File.Exists(dataPath))
        {
            try
            {
                var dataText = File.ReadAllText(dataPath);
                var deserializedData = JsonSerializer.Deserialize<List<ItemSourceDef>>(dataText);
                ItemSourceDefs.AddRange(deserializedData);
            } catch{/**/}
        }
        
        return this;
    }
    
    public IGranBreadItemSources Sources()
    {
        return this;
    }

    public ItemSourceDef FindById(string id)
    {
        return ItemSourceDefs.Find(i => i.Name.Equals(id));
    }

    public List<ItemSourceDef> All()
    {
        return ItemSourceDefs;
    }

    public IGranBreadItemSources Save()
    {
        try
        {
            var directory = new FileInfo(DataPath).Directory.FullName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var serializedData= JsonSerializer.Serialize(ItemSourceDefs);
            File.WriteAllText(DataPath, serializedData);
        }
        catch
        { /**/ }

        return this;
    }
    
    public IGranBreadItemSources Add(ItemSourceDef itemDef)
    {
        ItemSourceDefs.RemoveAll(i => i.Name.Equals(itemDef.Name));
        ItemSourceDefs.Add(itemDef);
        return this;
    }
    
    public IGranBreadItemSources Remove(ItemSourceDef itemDef)
    {
        ItemSourceDefs.RemoveAll(i => i.Name.Equals(itemDef.Name));
        return this;
    }

    
}