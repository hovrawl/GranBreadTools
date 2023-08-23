using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes.Data;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Pages;

public partial class TrackerPage : UserControl
{
    public TrackerPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private async void BindingTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        if (DataContext is not TrackerPageViewModel trackerVm) return;
        if (args.Item is not ItemTrackerPageViewModel trackerPageVm) return;
        
        // Prompt if user wants to remove
        var dialog = new ContentDialog
        {
            Title = "Remove Tracker",
            PrimaryButtonText = "Remove",
            CloseButtonText = "Cancel",
            Content = $"Are you sure you wish to remove: {trackerPageVm.ItemTrackerDef.Name}?"
        };
        
        var dialogResult = await dialog.ShowAsync();
        if (dialogResult != ContentDialogResult.Primary)
        {
            // User cancel
            return;
        }
        
        // Remove item from view model
        trackerVm.Items.Remove(trackerPageVm);
        
        // upsert
        DataManager.ItemTrackerDefs().Remove(trackerPageVm.ItemTrackerDef);
        DataManager.ItemTrackerDefs().Save();
    }

    private void TabView_OnTabItemsChanged(TabView sender, NotifyCollectionChangedEventArgs args)
    {
        switch (args.Action)
        {
            case NotifyCollectionChangedAction.Add:
            {
                // if a tab is added, we will move to the new tab
                var newIndex = args.NewStartingIndex;
                if(newIndex < sender.TabItems.Count()) sender.SelectedIndex = newIndex;
                break;
            }
        }
    }
}