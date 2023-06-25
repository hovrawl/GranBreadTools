using System.Collections.ObjectModel;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;

namespace GranBreadTracker.ViewModels;

public class ItemsPageViewModel : MainPageViewModelBase
{
    public ItemsPageViewModel()
    {
        // TODO - Load items from storage/settings
        Items = new ObservableCollection<ItemTrackerDef>();
        // Add ItemTrackerDef to collection

        AddDocumentCommand = new GeneralCommand(AddItemDefExecute);
    }
    
    public ObservableCollection<ItemTrackerDef> Items { get; }

    public GeneralCommand AddDocumentCommand { get; }

    
    private void AddItemDefExecute(object obj)
    {
        Items.Add(AddItemTracker(Items.Count));
    }
    
    // Add Blank Item tracker
    private ItemTrackerDef AddItemTracker(int itemCount)
    {
        // Generate blank item def so it can be customised later
        var tab = new ItemTrackerDef
        {
            Header = $"New Item Tracker - {itemCount}",
            IconSource = new SymbolIconSource { Symbol = Symbol.Document },
            Description = "New Item Tracker, rename me, give an icon customise Drop Locations."
        };

        return tab;
    }
}