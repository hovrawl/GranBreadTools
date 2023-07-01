using System.Collections.ObjectModel;
using DynamicData;
using GranBreadTracker.Classes;

namespace GranBreadTracker.ViewModels;

public class ItemTrackerPageViewModel : ViewModelBase
{
    public ItemTrackerDef ItemTrackerDef { get; }

    public ObservableCollection<ItemSourcePageViewModel> Sources { get; }
    
    public GeneralCommand AddItemSourceCommand { get; }

    public ItemTrackerPageViewModel(ItemTrackerDef trackerDef)
    {
        Sources = new ObservableCollection<ItemSourcePageViewModel>();

        ItemTrackerDef = trackerDef;

        var sourceVms = ItemTrackerDef.GenerateSourceViewModels();
        Sources.AddRange(sourceVms);
    }
}