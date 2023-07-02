﻿using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;

namespace GranBreadTracker.ViewModels;

public class ItemDefDialogViewModel : ViewModelBase
{
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
    /// Gets or sets the Icon
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
    
    private void HandleNameChange(string newValue)
    {
        // can use this to check if the item name is already taken and prevent user from creating another tracker
    }
    
    private void HandleDescriptionChange(string newValue)
    {
        // can use this to check if the item name is already taken and prevent user from creating another tracker
    }

    public ItemDef ToDef()
    {
        return new ItemDef
        {
            Name = Name,
            Description = Description,
            Icon = Icon
        };
    }
}