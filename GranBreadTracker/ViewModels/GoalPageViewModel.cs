using System;
using System.Collections.ObjectModel;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes.Data;
using GranBreadTracker.Classes.Extensions;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class GoalPageViewModel : MainPageViewModelBase
{
    public ObservableCollection<GoalDefDialogViewModel> Goals { get; }

    public GeneralCommand AddItemCommand { get; }
    
    public GoalPageViewModel()
    {
        // Load items from storage/settings
        Goals = new ObservableCollection<GoalDefDialogViewModel>();
        var goals = DataManager.Goals().All();
        foreach (var item in goals)
        {
            Goals.Add(item.ToViewModel());
        }
        
        AddItemCommand = new GeneralCommand(GoalDefDialogExecute);
    }
    
    private void GoalDefDialogExecute(object obj)
    {
        // Pop up item tracker creation dialog 
        var existing = obj as GoalDefDialogViewModel;
        GoalDefDialog(existing);
    }
    
    private async void GoalDefDialog(GoalDefDialogViewModel existing)
    {
        var dialog = new ContentDialog
        {
            Title = "New Goal",
            PrimaryButtonText = "Create",
            CloseButtonText = "Cancel"
        };

        // Pass the dialog if you need to hide it from the ViewModel.
        var viewModel = new GoalDefDialogViewModel();
        
        if (existing != null)
        {
            // If editing an existing item, update dialog
            dialog.Title = "Configure Goal";
            dialog.PrimaryButtonText = "Save";
            // Change view model to the existing one
            viewModel = existing;
        }
        
        // In our case the Content is a UserControl, but can be anything.
        var goalDefDialog = new GoalDefDialog
        {
            DataContext = viewModel
        };
        dialog.Content = goalDefDialog;

        dialog.Closed += goalDefDialog.DialogOnClosed;
        
        var dialogResult = await dialog.ShowAsync();
        
        if (dialogResult == ContentDialogResult.Primary)
        {
            var newItemDialog = dialog.Content as GoalDefDialog;
            var newItemViewModel = newItemDialog.DataContext as GoalDefDialogViewModel;
            var itemName = newItemViewModel.Name;
            if (!string.IsNullOrEmpty(itemName))
            {
                // If we had added an item and it wasnt existed, add to view model, otherwise it will update
                if (existing == null)
                {
                    Goals.Add(viewModel);
                }
                
                DataManager.Goals().Upsert(viewModel.ToDef());
                DataManager.Goals().Save();
            }
           
        }
    }
}