using System.Collections.Generic;
using System.Linq;
using GranBreadTracker.Classes;
using GranBreadTracker.Classes.Data;
using GranBreadTracker.Classes.Extensions;

namespace GranBreadTracker.ViewModels;

public class ItemSourcePageViewModel : ViewModelBase
{
    public ItemSourceDef Source { get; }

    public List<ItemCounter> Drops { get; } = new ();
    public List<ItemCounter> BlueChest { get; } = new ();
    
    public GeneralCommand ItemClickCommand { get; }

    
    public ItemSourcePageViewModel(ItemSourceDef source)
    {
        Source = source;

        var dropIds = source.Drops;
        var blueChestIds = source.BlueChest;

        foreach (var dropId in dropIds)
        {
            var existingItem = DataManager.Items().FindById(dropId.Key);
            if (existingItem == null) continue;
            Drops.Add(existingItem.ToCounter());
        }
        
        foreach (var dropId in blueChestIds)
        {
            var existingItem = DataManager.Items().FindById(dropId.Key);
            if (existingItem == null) continue;
            BlueChest.Add(existingItem.ToCounter());
        }
        
        ItemClickCommand = new GeneralCommand(ItemClicked);

    }


    private void ItemClicked(object item)
    {
        if (item is not ItemCounter itemCounter) return;
        
        // increment item count
        var existingItemId = Source.Drops.ContainsKey(itemCounter.Id);

        itemCounter.Count++;
    }
}