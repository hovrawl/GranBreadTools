using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Classes;

public class ItemTrackerDef
{
    public string Name { get; set; }

    public string Description { get; set; }
    
    public IconSource IconSource { get; set; }

    public ObservableCollection<ItemSourceDef> Sources { get; set; } = new ();

    public List<ItemSourcePageViewModel> GenerateSourceViewModels()
    {
        var returnList = new List<ItemSourcePageViewModel>();

        foreach (var source in Sources)
        {
            var vm = new ItemSourcePageViewModel(source);
            returnList.Add(vm);
        }
        
        return returnList;
    }
}