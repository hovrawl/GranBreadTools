using System.Collections.ObjectModel;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class SourcesPageViewModel : MainPageViewModelBase
{
    public SourcesPageViewModel()
    {
        // TODO - Load items from storage/settings
        Sources = new ObservableCollection<SourceDefDialogViewModel>();
        
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
        var viewModel = new SourceDefDialogViewModel();
        if (existing != null)
        {
            // If editing an existing item, pre-fill details
            viewModel.SourceName = existing.SourceName;
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
            var itemName = newItemViewModel.SourceName;
            if (!string.IsNullOrEmpty(itemName))
            {
                // If we had added an item and it wasnt existed, add to view model, otherwise it will update
                if(existing == null) Sources.Add(viewModel);
                else
                {
                    // If we were editing an existing model, update its properties
                    existing.SourceName = viewModel.SourceName;
                    existing.Icon = viewModel.Icon;
                }
            }
        }
    }
}