using System.Collections.Generic;

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
    /// Goal Description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// GranBreadIcon def that includes ItemKey and ItemType
    /// </summary>
    public GranBreadIcon Icon { get; set; }
    
    /// <summary>
    /// The amount to reach goal
    /// </summary>
    public long Goal { get; set; }
    
    /// <summary>
    /// The current amount
    /// </summary>
    public long Count { get; set; }

    /// <summary>
    /// Has the goal been met
    /// </summary>
    public bool GoalMet => Count >= Goal;

    public Dictionary<string, long> Items { get; set; } = new();
}