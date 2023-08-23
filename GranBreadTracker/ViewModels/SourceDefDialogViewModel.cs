using System;
using System.Collections.Generic;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;

namespace GranBreadTracker.ViewModels;

public class SourceDefDialogViewModel: ViewModelBase
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

    /// <summary>
    /// Dictionary containing Item ID + how many drops
    /// </summary>
    public Dictionary<string, double> Drops { get; set; } = new();

    
    /// <summary>
    /// Dictionary containing Item ID + how many drops
    /// </summary>
    public Dictionary<string, double> BlueChest { get; set; } = new();

    public SourceDefDialogViewModel()
    {
        Id = Guid.NewGuid().ToString();
    }

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
        if (!BlueChest.ContainsKey(itemId)) return;
        var value = BlueChest[itemId];
        var updatedValue = value + countChange;
        BlueChest[itemId] = updatedValue;
    }
}