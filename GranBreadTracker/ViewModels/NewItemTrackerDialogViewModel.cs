using System;
using FluentAvalonia.UI.Controls;

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

    private string _itemName;

    /// <summary>
    /// Gets or sets the Item Name
    /// </summary>
    public string ItemName
    {
        get => _itemName;
        set
        {
            if (RaiseAndSetIfChanged(ref _itemName, value))
            {
                HandleUserInput(_itemName);
            }
        }
    }
    
    private void HandleUserInput(string itemName)
    {
        // can use this to check if the item name is already taken and prevent user from creating another tracker
    }

    private IconSource _icon;

    /// <summary>
    /// Gets or sets the Item Name
    /// </summary>
    public IconSource Icon
    {
        get => _icon;
        set
        {
            if (RaiseAndSetIfChanged(ref _icon, value))
            {
                
            }
        }
    }
}