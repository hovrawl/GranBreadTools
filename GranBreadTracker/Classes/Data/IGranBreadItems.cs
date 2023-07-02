using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GranBreadTracker.Classes.Data;

public interface IGranBreadItems
{
    IGranBreadItems Initialise(string dataPath);
    
    IGranBreadItems Items();
    IGranBreadItems Save();

    IGranBreadItems Upsert(ItemDef itemDef);
    
    IGranBreadItems Remove(ItemDef itemDef);

    ItemDef FindById(string id);
    
    List<ItemDef> All();
    
    
}

public class GranBreadItems : IGranBreadItems
{
    private List<ItemDef> ItemDefs { get; set; }
    
    private string DataPath { get; set; }
    
    public IGranBreadItems Initialise(string dataPath)
    {
        ItemDefs = new List<ItemDef>();

        DataPath = dataPath;
        
        if (File.Exists(dataPath))
        {
            try
            {
                var dataText = File.ReadAllText(dataPath);
                var deserializedData = JsonSerializer.Deserialize<List<ItemDef>>(dataText);
                ItemDefs.AddRange(deserializedData);
            } catch{/**/}
        }
        
        return this;
    }
    
    public IGranBreadItems Items()
    {
        return this;
    }

    public ItemDef FindById(string id)
    {
        return ItemDefs.Find(i => i.Id.Equals(id));
    }

    public List<ItemDef> All()
    {
        return ItemDefs;
    }

    public IGranBreadItems Save()
    {
        try
        {
            var directory = new FileInfo(DataPath).Directory.FullName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var serializedData= JsonSerializer.Serialize(ItemDefs);
            File.WriteAllText(DataPath, serializedData);
        }
        catch
        { /**/ }

        return this;
    }
    
    public IGranBreadItems Upsert(ItemDef itemDef)
    {
        ItemDefs.RemoveAll(i => i.Id.Equals(itemDef.Id));
        ItemDefs.Add(itemDef);
        return this;
    }
    
    public IGranBreadItems Remove(ItemDef itemDef)
    {
        ItemDefs.RemoveAll(i => i.Id.Equals(itemDef.Id));
        return this;
    }

    
}