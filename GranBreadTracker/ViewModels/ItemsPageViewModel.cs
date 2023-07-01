using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class ItemsPageViewModel : MainPageViewModelBase
{
    public ItemsPageViewModel()
    {
        // TODO - Load items from storage/settings
        Items = new ObservableCollection<ItemTrackerPageViewModel>();
        // Add ItemTrackerDef to collection

        AddItemTrackerCommand = new GeneralCommand(AddItemDefExecute);
    }
    
    public ObservableCollection<ItemTrackerPageViewModel> Items { get; }

    public GeneralCommand AddItemTrackerCommand { get; }

    
    private void AddItemDefExecute(object obj)
    {
        // Pop up item tracker creation dialog 
        // var newItemTrackerDef = CreateNewItemTracker();
        CreateNewItemTracker();
        // if (newItemTrackerDef == null) return;
        //
        // Items.Add(newItemTrackerDef);
    }


    private async void CreateNewItemTracker()
    {
        var dialog = new ContentDialog
        {
            Title = "New Item Tracker",
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
                var returnDef = new ItemTrackerDef
                {
                    Name = itemName,
                    IconSource = iconSource,
                    Description = "New Item Tracker, rename me, give an icon customise Drop Locations.",
                    Sources = new ObservableCollection<ItemSourceDef>
                    {
                        new ItemSourceDef
                        {
                            Name = "Source 1",
                            Description = "The First Source, edit me"
                        }
                    }
                };
                
                var newVm = new ItemTrackerPageViewModel(returnDef);
                Items.Add(newVm);
            }
           
        }
        
        //return returnDef;
    }
}