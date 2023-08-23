using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using GranBreadTracker.Classes;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Pages;

public partial class ItemSourcePage : UserControl
{
    private readonly ListBox? _itemDropList;
    private readonly ListBox? _blueChestList;

    public ItemSourcePage()
    {
        InitializeComponent();
        
        _itemDropList = this.FindControl<ListBox>("ItemList");
        _blueChestList = this.FindControl<ListBox>("BlueChestList");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void SetupData()
    {
        var vm = DataContext as ItemSourcePageViewModel;
        LoadDrops(vm?.Drops);
        LoadBlueChest(vm?.BlueChest);
    }
    
    private void LoadDrops(ICollection<ItemCounter> itemDefs)
    {
        if (itemDefs == null) return;
        foreach (var icon in itemDefs)
        {
            _itemDropList.Items.Add(icon);
        }
    }
    
    private void LoadBlueChest(ICollection<ItemCounter> itemDefs)
    {
        if (itemDefs == null) return;
        foreach (var icon in itemDefs)
        {
            _blueChestList.Items.Add(icon);
        }
    }
    
    private void ItemList_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox listBox) return;

        // Cache selected value before clearing 
        var itemCounter = listBox.SelectedValue as ItemCounter;
        
        // Clear list selection so event can be triggered on every selection
        listBox.SelectedItems.Clear();
        
        if (DataContext is not ItemSourcePageViewModel vm) return;
        if (itemCounter == null) return;

        // Execute Item Def command
        vm.ItemClickCommand.Execute(itemCounter);

       
    }

    private void BlueChestList_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox listBox) return;
        
        // Cache selected value before clearing 
        var itemCounter = listBox.SelectedValue as ItemCounter;

        // Clear list selection so event can be triggered on every selection
        listBox.SelectedItems.Clear();
        
        if (DataContext is not ItemSourcePageViewModel vm) return;
        if (itemCounter == null) return;

        // Execute Item Def command
        vm.ItemClickCommand.Execute(itemCounter);
    }

    private void BlueChestList_OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        SetupData();
    }
}