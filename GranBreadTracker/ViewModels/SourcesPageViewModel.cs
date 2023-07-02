using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Classes.Data;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class SourcesPageViewModel : MainPageViewModelBase
{
    public SourcesPageViewModel()
    {
        // Load items from storage/settings
        Sources = new ObservableCollection<SourceDefDialogViewModel>();
        var sourceDefs = DataManager.ItemSources().All();
        foreach (var itemSource in sourceDefs)
        {
            Sources.Add(itemSource.ToViewModel());
        }
        
        AddCommand = new GeneralCommand(SourceDefDialogExecute);

    }
    
    public ObservableCollection<SourceDefDialogViewModel> Sources { get; }

    public GeneralCommand AddCommand { get; }
    
    private void SourceDefDialogExecute(object obj)
    {
        // Pop up item tracker creation dialog 
        var existing = obj as SourceDefDialogViewModel;
        SourceDefDialog(existing);
    }
    
    private async void SourceDefDialog(SourceDefDialogViewModel existing)
    {
        var dialog = new ContentDialog
        {
            Title = "New Item",
            PrimaryButtonText = "Create",
            CloseButtonText = "Cancel"
        };

        // Pass the dialog if you need to hide it from the ViewModel.
        var viewModel = new SourceDefDialogViewModel
        {
            Id = Guid.NewGuid().ToString()
        };
        if (existing != null)
        {
            // If editing an existing item, pre-fill details
            viewModel.Name = existing.Name;
            viewModel.Icon = existing.Icon;
            dialog.PrimaryButtonText = "Save";
        }
        
        // In our case the Content is a UserControl, but can be anything.
        dialog.Content = new SourceDefDialog()
        {
            DataContext = viewModel
        };

        var dialogResult = await dialog.ShowAsync();
        
        if (dialogResult == ContentDialogResult.Primary)
        {
            var newItemDialog = dialog.Content as SourceDefDialog;
            var newItemViewModel = newItemDialog.DataContext as SourceDefDialogViewModel;
            var itemName = newItemViewModel.Name;
            if (!string.IsNullOrEmpty(itemName))
            {
                // If we had added an item and it wasnt existed, add to view model, otherwise it will update
                if (existing == null)
                {
                    Sources.Add(viewModel);
                }
                else
                {
                    // If we were editing an existing model, update its properties
                    existing.Name = viewModel.Name;
                    existing.Icon = viewModel.Icon;
                }
                DataManager.ItemSources().Upsert(viewModel.ToDef());
                DataManager.ItemSources().Save();
            }
        }
    }
}