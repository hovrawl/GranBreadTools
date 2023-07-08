﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Classes.Data;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class TrackerPageViewModel : MainPageViewModelBase
{
    public TrackerPageViewModel()
    {
        // Load items from storage/settings
        Items = new ObservableCollection<ItemTrackerPageViewModel>();
        var itemTrackerDefs = DataManager.ItemTrackerDefs().All();
        foreach (var trackerDef in itemTrackerDefs)
        {
            Items.Add(trackerDef.ToViewModel());
        }
        
        // Add ItemTrackerDef to collection
        AddItemTrackerCommand = new GeneralCommand(AddItemDefExecute);
    }
    
    public ObservableCollection<ItemTrackerPageViewModel> Items { get; }

    public GeneralCommand AddItemTrackerCommand { get; }


    private void AddItemDefExecute(object obj)
    {
        // Pop up item tracker creation dialog 
        var existing = obj as ItemTrackerPageViewModel;
        CreateNewItemTracker(existing);
    }


    private async void CreateNewItemTracker(ItemTrackerPageViewModel existing)
    {
        var dialog = new ContentDialog
        {
            Title = "New Item Tracker",
            PrimaryButtonText = "Create",
            CloseButtonText = "Cancel"
        };

        // Pass the dialog if you need to hide it from the ViewModel.
        var viewModel = new NewItemTrackerDialogViewModel(dialog)
        {
            Id = Guid.NewGuid().ToString()
        };
        
        if (existing != null)
        {
            viewModel.Id = existing.ItemTrackerDef.Id;
            viewModel.Name = existing.ItemTrackerDef.Name;
            viewModel.Icon = existing.ItemTrackerDef.Icon;
        }

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
            var itemName = newItemViewModel.Name;
            if (!string.IsNullOrEmpty(itemName))
            {
                var returnDef = newItemViewModel.ToDef();
                if (existing == null)
                {
                    returnDef.Sources = new ObservableCollection<ItemSourceDef>();
                    Items.Add(returnDef.ToViewModel());
                }
                else
                {
                    // If we have an existing item tracker def, we will update its values
                    returnDef = existing.ItemTrackerDef;
                    returnDef.Icon = newItemViewModel.Icon;
                    returnDef.Name = newItemViewModel.Name;
                    returnDef.Description = newItemViewModel.Description;
                }
                
                DataManager.ItemTrackerDefs().Upsert(returnDef);
                DataManager.ItemTrackerDefs().Save();
            }
           
        }
        
    }
}