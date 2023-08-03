using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Classes.Data;
using GranBreadTracker.Classes.Extensions;
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
            Title = "New Raid",
            PrimaryButtonText = "Create",
            CloseButtonText = "Cancel"
        };

        // Pass the dialog if you need to hide it from the ViewModel.
        var viewModel = new SourceDefDialogViewModel();
        if (existing != null)
        {
            // If editing an existing item, update dialog
            dialog.Title = "Configure Raid";
            dialog.PrimaryButtonText = "Save";
            // Change view model to the existing one
            viewModel = existing;
        }

        var defDialog = new SourceDefDialog()
        {
            DataContext = viewModel
        };
        // In our case the Content is a UserControl, but can be anything.
        dialog.Content = defDialog;
        dialog.Closed += defDialog.DialogOnClosed;
        
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
                
                DataManager.ItemSources().Upsert(viewModel.ToDef());
                DataManager.ItemSources().Save();
            }
        }
    }
}