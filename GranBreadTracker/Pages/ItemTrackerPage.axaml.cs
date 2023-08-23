using System.Collections.Specialized;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Classes.Data;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Pages;

public partial class ItemTrackerPage : UserControl
{
    public ItemTrackerPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void ItemSourcePage_AttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        
    }

    private async void BindingTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        if (args.Item is not ItemSourcePageViewModel sourceVm) return;
        if (DataContext is not ItemTrackerPageViewModel trackerVm) return;

        // Prompt if user wants to remove
        var dialog = new ContentDialog
        {
            Title = "Remove Raid",
            PrimaryButtonText = "Remove",
            CloseButtonText = "Cancel",
            Content = $"Are you sure you wish to remove: {sourceVm.Source.Name}?"
        };
        
        var dialogResult = await dialog.ShowAsync();
        if (dialogResult != ContentDialogResult.Primary)
        {
            // User cancel
            return;
        }
        
        var trackerDef = trackerVm.ItemTrackerDef;
        var sourceId = sourceVm.Source.Id;
        
        // Remove item from view model
        trackerVm.Sources.Remove(sourceVm);

        // remove source model
        var existingSourceModel = trackerDef.Sources.FirstOrDefault(i => i.Id.Equals(sourceId));
        if (existingSourceModel != null) trackerDef.Sources.Remove(existingSourceModel);

        // remove source id
        trackerDef.SourceIds.Remove(sourceId);
        
        // upsert
        DataManager.ItemTrackerDefs().Upsert(trackerVm.ItemTrackerDef);
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