﻿using System.Collections.ObjectModel;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class ItemsPageViewModel : MainPageViewModelBase
{
    public ItemsPageViewModel()
    {
        // TODO - Load items from storage/settings
        Items = new ObservableCollection<ItemDefDialogViewModel>();
        
        AddItemCommand = new GeneralCommand(ItemDefDialogExecute);

    }
    
    public ObservableCollection<ItemDefDialogViewModel> Items { get; }

    public GeneralCommand AddItemCommand { get; }
    
    private void ItemDefDialogExecute(object obj)
    {
        // Pop up item tracker creation dialog 
        var existing = obj as ItemDefDialogViewModel;
        ItemDefDialog(existing);
    }
    
    private async void ItemDefDialog(ItemDefDialogViewModel existing)
    {
        var dialog = new ContentDialog
        {
            Title = "New Item",
            PrimaryButtonText = "Create",
            CloseButtonText = "Cancel"
        };

        // Pass the dialog if you need to hide it from the ViewModel.
        var viewModel = new ItemDefDialogViewModel();
        if (existing != null)
        {
            // If editing an existing item, pre-fill details
            viewModel.ItemName = existing.ItemName;
            viewModel.Icon = existing.Icon;
            dialog.PrimaryButtonText = "Save";
        }
        
        // In our case the Content is a UserControl, but can be anything.
        dialog.Content = new ItemDefDialog()
        {
            DataContext = viewModel
        };

        var dialogResult = await dialog.ShowAsync();
        
        if (dialogResult == ContentDialogResult.Primary)
        {
            var newItemDialog = dialog.Content as ItemDefDialog;
            var newItemViewModel = newItemDialog.DataContext as ItemDefDialogViewModel;
            var itemName = newItemViewModel.ItemName;
            if (!string.IsNullOrEmpty(itemName))
            {
                // If we had added an item and it wasnt existed, add to view model, otherwise it will update
                if(existing == null) Items.Add(viewModel);
                else
                {
                    // If we were editing an existing model, update its properties
                    existing.ItemName = viewModel.ItemName;
                    existing.Icon = viewModel.Icon;
                }
            }
           
        }
    }
}