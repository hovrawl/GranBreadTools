using System;
using System.Collections.Generic;
using GranBreadTracker.Classes;

namespace GranBreadTracker.ViewModels;

public class GoalDefDialogViewModel: ViewModelBase
{
    private string _id;

    /// <summary>
    /// Gets or sets the Item Id
    /// </summary>
    public string Id
    {
        get => _id;
        set
        {
            if (RaiseAndSetIfChanged(ref _id, value))
            {
                HandleNameChange(_id);
            }
        }
    }
    
    private string _name;

    /// <summary>
    /// Gets or sets the Item Name
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            if (RaiseAndSetIfChanged(ref _name, value))
            {
                HandleNameChange(_name);
            }
        }
    }
    
    private string _description;

    /// <summary>
    /// Gets or sets the Item Description
    /// </summary>
    public string Description
    {
        get => _description;
        set
        {
            if (RaiseAndSetIfChanged(ref _description, value))
            {
                HandleDescriptionChange(_description);
            }
        }
    }

    private GranBreadIcon _icon;

    /// <summary>
    /// Gets or sets the GranBreadIcon
    /// </summary>
    public GranBreadIcon Icon
    {
        get => _icon;
        set
        {
            if (RaiseAndSetIfChanged(ref _icon, value))
            {
                
            }
        }
    }
    
    private long _goal;

    /// <summary>
    /// Gets or sets the Item Description
    /// </summary>
    public long Goal
    {
        get => _goal;
        set
        {
            if (RaiseAndSetIfChanged(ref _goal, value))
            {
                //HandleDescriptionChange(_goal);
            }
        }
    }
    
    private long _count;

 

    /// <summary>
    /// Gets or sets the Item Description
    /// </summary>
    public long Count
    {
        get => _count;
        set
        {
            if (RaiseAndSetIfChanged(ref _count, value))
            {
                //HandleDescriptionChange(_goal);
            }
        }
    }
    
    public GoalDefDialogViewModel()
    {
        Id = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Dictionary containing Item ID + how many drops
    /// </summary>
    public Dictionary<string, long> Items { get; set; } = new();
    
    private void HandleNameChange(string newValue)
    {
        // can use this to check if the item name is already taken and prevent user from creating another tracker
    }
    
    private void HandleDescriptionChange(string newValue)
    {
        // can use this to check if the item name is already taken and prevent user from creating another tracker
    }

    /// <summary>
    /// Update count for item
    /// </summary>
    /// <param name="itemId">string item id</param>
    /// <param name="countChange">integer change for item count, positive for increase, negative for decrease</param>
    public void UpdateItemCount(string itemId, int countChange)
    {
        if (!Items.ContainsKey(itemId)) return;
        var value = Items[itemId];
        var updatedValue = value + countChange;
        Items[itemId] = updatedValue;
    }
}