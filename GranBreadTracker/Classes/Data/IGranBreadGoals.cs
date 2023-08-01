using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GranBreadTracker.Classes.Data;

public interface IGranBreadGoals
{
    IGranBreadGoals Initialise(string dataPath);
    
    IGranBreadGoals Goals();
    
    IGranBreadGoals Save();

    IGranBreadGoals Upsert(GoalDef goalDef);
    
    IGranBreadGoals Remove(GoalDef itemDef);

    GoalDef FindById(string id);
    
    List<GoalDef> All();
}

public class GranBreadGoals : IGranBreadGoals
{
    private List<GoalDef> GoalDefs { get; set; }
    
    private string DataPath { get; set; }
    
    public IGranBreadGoals Initialise(string dataPath)
    {
        GoalDefs = new List<GoalDef>();

        DataPath = dataPath;
        
        if (File.Exists(dataPath))
        {
            try
            {
                var dataText = File.ReadAllText(dataPath);
                var deserializedData = JsonSerializer.Deserialize<List<GoalDef>>(dataText);
                GoalDefs.AddRange(deserializedData);
            } catch{/**/}
        }
        
        return this;
    }
    
    public IGranBreadGoals Goals()
    {
        return this;
    }

    public GoalDef FindById(string id)
    {
        return GoalDefs.Find(i => i.Id.Equals(id));
    }

    public List<GoalDef> All()
    {
        return GoalDefs;
    }

    public IGranBreadGoals Save()
    {
        try
        {
            var directory = new FileInfo(DataPath).Directory.FullName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var serializedData= JsonSerializer.Serialize(GoalDefs);
            File.WriteAllText(DataPath, serializedData);
        }
        catch
        { /**/ }

        return this;
    }
    
    public IGranBreadGoals Upsert(GoalDef goalDef)
    {
        GoalDefs.RemoveAll(i => i.Id.Equals(goalDef.Id));
        GoalDefs.Add(goalDef);
        return this;
    }
    
    public IGranBreadGoals Remove(GoalDef goalDef)
    {
        GoalDefs.RemoveAll(i => i.Id.Equals(goalDef.Id));
        return this;
    }

    
}