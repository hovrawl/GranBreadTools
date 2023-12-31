﻿using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Controls;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Pages;

public partial class SourceDefDialog : UserControl
{
    public SourceDefDialog()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private GranblueObjectPickerList _pickerList;
    private GranblueObjectPickerList _blueChestList;

    
    public ICollection<GranblueObject> SelectedItems { get; set; } = new List<GranblueObject>();

    
    private void InputField_OnAttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
    {
        // We will set the focus into our input field just after it got attached to the visual tree.
        if (sender is InputElement inputElement)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                inputElement.Focus(NavigationMethod.Unspecified, KeyModifiers.None);
            });
        }
    }

    private void IconPicker_OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        // Setup icon picker
        if (sender is not GranblueIconPicker iconPicker) return;
        if (iconPicker.DataContext is not GranblueIconPickerViewModel vm) return;

        var dialogContext = DataContext as SourceDefDialogViewModel;

        vm.IconChanged += (o, args) =>
        {
            dialogContext.Icon = args.Icon;
        };
        
        if (dialogContext.Icon != null)
        {
            var icon = dialogContext.Icon;
            vm.Icon = icon;
            vm.SetIcon(icon);
        }
        else
        {
            dialogContext.Icon = vm.Icon;
        }
    }

    private void DropListPanel_OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (sender is not StackPanel panel) return;
        if (DataContext is not SourceDefDialogViewModel vm) return;

        var objectPickerVm = new GranblueObjectPickerViewModel(GranblueObjectType.Item);
        objectPickerVm.InitializeData();
        
        var selectedIds = vm.Drops.Select(i => i.Key);
 
        _pickerList = new GranblueObjectPickerList(objectPickerVm.GranblueObjects, true, selectedIds);
        panel.Children.Add(_pickerList);
        
        // pickerList.ObjectPickerSelectEventHandler += (sender, args) =>
        // {
        //     var selectedObjects = pickerList.GetSelectedObjects();
        //     SelectedItems = selectedObjects;
        // };
    }

    private void BlueChestListPanel_OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (sender is not StackPanel panel) return;
        if (DataContext is not SourceDefDialogViewModel vm) return;

        var objectPickerVm = new GranblueObjectPickerViewModel(GranblueObjectType.Item);
        objectPickerVm.InitializeData();
        
        var selectedIds = vm.BlueChest.Select(i => i.Key);
        _blueChestList = new GranblueObjectPickerList(objectPickerVm.GranblueObjects, true, selectedIds);
        panel.Children.Add(_blueChestList);
    }
    
    public void DialogOnClosed(ContentDialog sender, ContentDialogClosedEventArgs args)
    {
        sender.Closed -= DialogOnClosed;
        if (DataContext is not SourceDefDialogViewModel vm) return;
        
        var selectedItems = _pickerList.GetSelectedObjects();
        var selectedIds = selectedItems.Select(i => i.Id);

        // Drops
        var itemToRemove = new List<string>();
        foreach (var keyPair in vm.Drops)
        {
            if (!selectedIds.Contains(keyPair.Key))
            {
                itemToRemove.Add(keyPair.Key);
            }
        }
            
        foreach (var item in selectedItems)
        {
            if (vm.Drops.ContainsKey(item.Id))
            {
                continue;
            }
                
            vm.Drops.Add(item.Id, 0);
        }
        
        foreach (var key in itemToRemove)
        {
            vm.Drops.Remove(key);
        }
        
        // Blue Chest
        selectedItems = _blueChestList.GetSelectedObjects();
        selectedIds = selectedItems.Select(i => i.Id);
        itemToRemove = new List<string>();
        foreach (var keyPair in vm.BlueChest)
        {
            if (!selectedIds.Contains(keyPair.Key))
            {
                itemToRemove.Add(keyPair.Key);
            }
        }
            
        foreach (var item in selectedItems)
        {
            if (vm.BlueChest.ContainsKey(item.Id))
            {
                continue;
            }
                
            vm.BlueChest.Add(item.Id, 0);
        }
        
        foreach (var key in itemToRemove)
        {
            vm.BlueChest.Remove(key);
        }
    }
}