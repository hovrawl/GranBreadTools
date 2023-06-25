﻿using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Pages;

public partial class ItemsPage : UserControl
{
    public ItemsPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void BindingTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        // Remove item from view model
        (DataContext as ItemsPageViewModel).Items.Remove(args.Item as ItemTrackerDef);
        
        // Need to save collection back to settings after removal 
        // TODO - Save state of item trackers
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