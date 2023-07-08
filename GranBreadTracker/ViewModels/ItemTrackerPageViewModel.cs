using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Classes.Data;
using GranBreadTracker.Controls;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class ItemTrackerPageViewModel : ViewModelBase
{
    public ItemTrackerDef ItemTrackerDef { get; }

    public ObservableCollection<ItemSourcePageViewModel> Sources { get; }
    
    public GeneralCommand AddItemSourceCommand { get; }

    public ItemTrackerPageViewModel(ItemTrackerDef trackerDef)
    {
        AddItemSourceCommand = new GeneralCommand(AddItemSourceExecute);

        Sources = new ObservableCollection<ItemSourcePageViewModel>();
        
        ItemTrackerDef = trackerDef;

        var sourceVms = ItemTrackerDef.GenerateSourceViewModels();
        Sources.AddRange(sourceVms);
    }
    
    private void AddItemSourceExecute(object obj)
    {
        // Pop up item tracker creation dialog 
        CreateNewItemSource();
    }
    
    private async void CreateNewItemSource()
    {
        var dialog = new ContentDialog
        {
            Title = "Add Item Source",
            PrimaryButtonText = "Add",
            CloseButtonText = "Cancel"
        };

        // Pass the dialog if you need to hide it from the ViewModel.
        var viewModel = new GranblueObjectPickerViewModel(GranblueObjectType.Source);

        var objectPicker = new GranblueObjectPickerPage
        {
            DataContext = viewModel
        };
        objectPicker.InitializeObjects();
        
        // In our case the Content is a UserControl, but can be anything.
        dialog.Content = objectPicker;
        
        var dialogResult = await dialog.ShowAsync();
        
        if (dialogResult == ContentDialogResult.Primary)
        {
            var newItemDialog = dialog.Content as GranblueObjectPickerPage;
            var newItemViewModel = newItemDialog.DataContext as GranblueObjectPickerViewModel;
            var selectedObject = newItemViewModel.GranblueObject;
            if (!string.IsNullOrEmpty(selectedObject?.Id))
            {
                var existingSource = DataManager.ItemSources().FindById(selectedObject.Id);
                
                var newVm = new ItemSourcePageViewModel(existingSource);
                ItemTrackerDef.SourceIds.Add(existingSource.Id);
                ItemTrackerDef.Sources.Add(existingSource);
                Sources.Add(newVm);
                
                DataManager.ItemTrackerDefs().Upsert(ItemTrackerDef);
                DataManager.ItemTrackerDefs().Save();
            }
           
        }
        
        //return returnDef;
    }
}