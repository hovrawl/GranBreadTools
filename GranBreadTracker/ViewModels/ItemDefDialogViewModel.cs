using FluentAvalonia.UI.Controls;

namespace GranBreadTracker.ViewModels;

public class ItemDefDialogViewModel : ViewModelBase
{
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
    
    private void HandleUserInput(string itemName)
    {
        // can use this to check if the item name is already taken and prevent user from creating another tracker
    }
}