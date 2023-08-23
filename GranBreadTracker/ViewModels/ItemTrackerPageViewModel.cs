using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            Title = "Add Raids",
            PrimaryButtonText = "Add",
            CloseButtonText = "Cancel"
        };

        // Pass the dialog if you need to hide it from the ViewModel.
        var viewModel = new GranblueObjectPickerViewModel(GranblueObjectType.Source);

        viewModel.InitializeData();
        
        //var selectedIds = vm.Drops.Select(i => i.Key);
        var selectedIds = new List<string>();
 
        var pickerList = new GranblueObjectPickerList(viewModel.GranblueObjects, true, selectedIds);
        
        // var objectPicker = new GranblueObjectPickerPage
        // {
        //     DataContext = viewModel
        // };
        // objectPicker.InitializeObjects();
        
        // In our case the Content is a UserControl, but can be anything.
        dialog.Content = pickerList;
        
        var dialogResult = await dialog.ShowAsync();
        
        if (dialogResult == ContentDialogResult.Primary)
        {
            // var newItemDialog = dialog.Content as GranblueObjectPickerPage;
            // var newItemViewModel = newItemDialog.DataContext as GranblueObjectPickerViewModel;
            // var selectedObject = newItemViewModel.GranblueObject;
            var selectedItems = pickerList.GetSelectedObjects();
            selectedIds = selectedItems.Select(i => i.Id).ToList();

            // Drops
            var itemToRemove = new List<string>();
            foreach (var sourceId in ItemTrackerDef.SourceIds)
            {
                if (!selectedIds.Contains(sourceId))
                {
                    itemToRemove.Add(sourceId);
                }
            }
            foreach (var key in itemToRemove)
            {
                // Remove model 
                var existing = ItemTrackerDef.Sources.FirstOrDefault(i => i.Id.Equals(key));
                if(existing != null) ItemTrackerDef.Sources.Remove(existing);
                // remove view model
                var existingVm = Sources.FirstOrDefault(i => i.Source.Id.Equals(key));
                if(existingVm != null) Sources.Remove(existingVm);
                
                // remove source id
                ItemTrackerDef.SourceIds.Remove(key);
            }
            
            foreach (var item in selectedItems)
            {
                if (ItemTrackerDef.SourceIds.Contains(item.Id))
                {
                    continue;
                }
                
                ItemTrackerDef.SourceIds.Add(item.Id);
                
                var existingSource = DataManager.ItemSources().FindById(item.Id);
                if (existingSource != null)
                {
                    ItemTrackerDef.Sources.Add(existingSource);
                    var vm = new ItemSourcePageViewModel(existingSource);
                    Sources.Add(vm);
                }

            }
        
            DataManager.ItemTrackerDefs().Upsert(ItemTrackerDef);
            DataManager.ItemTrackerDefs().Save();
            
            
            // if (!string.IsNullOrEmpty(selectedObject?.Id))
            // {
            //     
            //     var newVm = new ItemSourcePageViewModel(existingSource);
            //     ItemTrackerDef.SourceIds.Add(existingSource.Id);
            //     ItemTrackerDef.Sources.Add(existingSource);
            //     Sources.Add(newVm);
            //     
            //     DataManager.ItemTrackerDefs().Upsert(ItemTrackerDef);
            //     DataManager.ItemTrackerDefs().Save();
            // }
           
        }
        
        //return returnDef;
    }
}