using GranBreadTracker.Classes;

namespace GranBreadTracker.ViewModels;

public class ItemSourcePageViewModel : ViewModelBase
{
    public ItemSourceDef Source { get; }
    public ItemSourcePageViewModel(ItemSourceDef source)
    {
        Source = source;
    }
}