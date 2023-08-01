namespace GranBreadTracker.Classes;

public class GoalDef
{
    /// <summary>
    /// Unique Id
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Name of Item
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
}