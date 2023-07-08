using System;
using System.Collections.ObjectModel;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;

namespace GranBreadTracker.ViewModels;

public class NewItemTrackerDialogViewModel : ViewModelBase
{
    private readonly ContentDialog dialog;

    public NewItemTrackerDialogViewModel(ContentDialog dialog)
    {
        if (dialog is null)
        {
            throw new ArgumentNullException(nameof(dialog));
        }

        this.dialog = dialog;
        dialog.Closed += DialogOnClosed;
    }

    private void DialogOnClosed(ContentDialog sender, ContentDialogClosedEventArgs args)
    {
        dialog.Closed -= DialogOnClosed;

        
        var result = args.Result;
    }

    private string _id;

    /// <summary>
    /// Gets or sets the Id
    /// </summary>
    public string Id
    {
        get => _id;
        set
        {
            if (RaiseAndSetIfChanged(ref _id, value))
            {
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
            }
        }
    }
    
    private string _description;

    /// <summary>
    /// Gets or sets the Description
    /// </summary>
    public string Description
    {
        get => _description;
        set
        {
            if (RaiseAndSetIfChanged(ref _description, value))
            {
                
            }
        }
    }


    private GranBreadIcon _icon;

    /// <summary>
    /// Gets or sets the Item Name
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

    public ItemTrackerDef ToDef()
    {
        return new ItemTrackerDef
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Icon = Icon,
        };
    }
}