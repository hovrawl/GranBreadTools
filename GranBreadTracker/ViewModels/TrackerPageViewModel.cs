﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class TrackerPageViewModel : MainPageViewModelBase
{
    public TrackerPageViewModel()
    {
        // Load items from storage/settings
        Items = new ObservableCollection<ItemTrackerPageViewModel>();
        if (App.Current.Resources.TryGetResource("Tracker", null, out var sources))
        {
            if (sources is List<ItemTrackerDef> sourceDefs)
            {
                foreach (var itemSource in sourceDefs)
                {
                    Items.Add(itemSource.ToViewModel());
                }
            }
        }
        // Add ItemTrackerDef to collection

        AddItemTrackerCommand = new GeneralCommand(AddItemDefExecute);
    }
    
    public ObservableCollection<ItemTrackerPageViewModel> Items { get; }

    public GeneralCommand AddItemTrackerCommand { get; }


    private void AddItemDefExecute(object obj)
    {
        // Pop up item tracker creation dialog 
        CreateNewItemTracker();
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
                var icon = newItemViewModel.Icon;
                var returnDef = new ItemTrackerDef
                {
                    Name = itemName,
                    Icon = icon,
                    Description = "New Item Tracker, rename me, give an icon customise Drop Locations.",
                    Sources = new ObservableCollection<ItemSourceDef>
                    {
                        new ItemSourceDef
                        {
                            Name = "Source 1",
                            Description = "The First Source, edit me",
                            Icon = icon
                        }
                    }
                };
                
                var newVm = new ItemTrackerPageViewModel(returnDef);
                Items.Add(newVm);
            }
           
        }
        
    }
}