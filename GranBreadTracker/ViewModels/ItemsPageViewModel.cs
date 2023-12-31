﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Classes.Data;
using GranBreadTracker.Classes.Extensions;
using GranBreadTracker.Pages;
using Microsoft.Extensions.Configuration;

namespace GranBreadTracker.ViewModels;

public class ItemsPageViewModel : MainPageViewModelBase
{
    public ObservableCollection<ItemDefDialogViewModel> Items { get; }

    public GeneralCommand AddItemCommand { get; }
    
    public ItemsPageViewModel()
    {
        // Load items from storage/settings
        Items = new ObservableCollection<ItemDefDialogViewModel>();
        var items = DataManager.Items().All();
        foreach (var item in items)
        {
            Items.Add(item.ToViewModel());
        }
        AddItemCommand = new GeneralCommand(ItemDefDialogExecute);

    }
    
   
    
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
            // If editing an existing item, update dialog
            dialog.Title = "Configure Item";
            dialog.PrimaryButtonText = "Save";
            // Change view model to the existing one
            viewModel = existing;
        }
        
        // In our case the Content is a UserControl, but can be anything.
        dialog.Content = new ItemDefDialog
        {
            DataContext = viewModel
        };

        var dialogResult = await dialog.ShowAsync();
        
        if (dialogResult == ContentDialogResult.Primary)
        {
            var newItemDialog = dialog.Content as ItemDefDialog;
            var newItemViewModel = newItemDialog.DataContext as ItemDefDialogViewModel;
            var itemName = newItemViewModel.Name;
            if (!string.IsNullOrEmpty(itemName))
            {
                // If we had added an item and it wasnt existed, add to view model, otherwise it will update
                if (existing == null)
                {
                    Items.Add(viewModel);
                }

                DataManager.Items().Upsert(viewModel.ToDef());
                DataManager.Items().Save();
            }
           
        }
    }
}