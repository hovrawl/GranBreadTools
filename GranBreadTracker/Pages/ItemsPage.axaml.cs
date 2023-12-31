﻿using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
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
        (DataContext as TrackerPageViewModel).Items.Remove(args.Item as ItemTrackerPageViewModel);
        
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



    private void Item_OnTapped(object? sender, TappedEventArgs e)
    {
        if(DataContext is not ItemsPageViewModel vm) return;
        if (sender is not ListBoxItem listItem) return;
        if (listItem.DataContext is not ItemDefDialogViewModel itemDef) return;
        
        vm.AddItemCommand.Execute(itemDef);
    }

    private void AddItem_OnClick(object? sender, RoutedEventArgs e)
    {
        if(DataContext is not ItemsPageViewModel vm) return;
        
        vm.AddItemCommand.Execute(null);

    }

    private void ItemList_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox listBox) return;
        if (DataContext is not ItemsPageViewModel vm) return;
        if (listBox.SelectedValue is not ItemDefDialogViewModel itemDef) return;

        // Execute Item Def command
        vm.AddItemCommand.Execute(itemDef);

        // Clear list selection so event can be triggered on every selection
        listBox.SelectedItems.Clear();
    }
}