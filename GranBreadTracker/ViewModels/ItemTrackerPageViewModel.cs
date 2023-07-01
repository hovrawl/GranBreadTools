using System.Collections.ObjectModel;
using DynamicData;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class ItemTrackerPageViewModel : ViewModelBase
{
    public ItemTrackerDef ItemTrackerDef { get; }

    public ObservableCollection<ItemSourcePageViewModel> Sources { get; }
    
    public GeneralCommand AddItemSourceCommand { get; }

    public ItemTrackerPageViewModel(ItemTrackerDef trackerDef)
    {
        AddItemSourceCommand = new GeneralCommand(AddItemSoureExecute);

        Sources = new ObservableCollection<ItemSourcePageViewModel>();

        ItemTrackerDef = trackerDef;

        var sourceVms = ItemTrackerDef.GenerateSourceViewModels();
        Sources.AddRange(sourceVms);
    }
    
    private void AddItemSoureExecute(object obj)
    {
        // Pop up item tracker creation dialog 
        CreateNewItemSource();
    }
    
    private async void CreateNewItemSource()
    {
        var dialog = new ContentDialog
        {
            Title = "New Item Source",
            PrimaryButtonText = "Create",
            CloseButtonText = "Cancel"
        };

        // Pass the dialog if you need to hide it from the ViewModel.
        var viewModel = new NewItemTrackerDialogViewModel(dialog);

        // In our case the Content is a UserControl, but can be anything.
        dialog.Content = new NewItemTrackerDialog()
        {
            DataContext = viewModel
        };

        var dialogResult = await dialog.ShowAsync();
        
        if (dialogResult == ContentDialogResult.Primary)
        {
            var newItemDialog = dialog.Content as NewItemTrackerDialog;
            var newItemViewModel = newItemDialog.DataContext as NewItemTrackerDialogViewModel;
            var itemName = newItemViewModel.ItemName;
            if (!string.IsNullOrEmpty(itemName))
            {
                var iconSource = newItemViewModel.Icon;
                var returnDef = new ItemSourceDef
                {
                    Name = itemName,
                    IconSource = iconSource,
                    Description = "New Item Source, rename me, give an icon customise Drop Locations.",
                };
                
                var newVm = new ItemSourcePageViewModel(returnDef);
                Sources.Add(newVm);
            }
           
        }
        
        //return returnDef;
    }
}