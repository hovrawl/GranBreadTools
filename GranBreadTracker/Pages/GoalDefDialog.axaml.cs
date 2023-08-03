using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Controls;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Pages;

public partial class GoalDefDialog : UserControl
{
    public ICollection<GranblueObject> SelectedItems { get; set; } = new List<GranblueObject>();

    public TypedEventHandler<ContentDialog, ContentDialogClosedEventArgs> OnDialogClose;

    private GranblueObjectPickerList _pickerList;
    
    public GoalDefDialog()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
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

        var dialogContext = DataContext as GoalDefDialogViewModel;

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
    
    private void ItemListPanel_OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (sender is not StackPanel panel) return;
        if (DataContext is not GoalDefDialogViewModel vm) return;

        var objectPickerVm = new GranblueObjectPickerViewModel(GranblueObjectType.Item);
        objectPickerVm.InitializeData();

        var selectedIds = vm.Items.Select(i => i.Key);
        
        _pickerList = new GranblueObjectPickerList(objectPickerVm.GranblueObjects, true, selectedIds);
        panel.Children.Add(_pickerList);

        // _pickerList.ObjectPickerSelectEventHandler += (sender, args) =>
        // {
        //     var selectedObjects = _pickerList.GetSelectedObjects();
        //     SelectedItems = selectedObjects;
        // };
    }
    
    public void DialogOnClosed(ContentDialog sender, ContentDialogClosedEventArgs args)
    {
        sender.Closed -= DialogOnClosed;
        if (DataContext is not GoalDefDialogViewModel vm) return;

        var selectedItems = _pickerList.GetSelectedObjects();
        
        var selectedIds = selectedItems.Select(i => i.Id);

        var itemToRemove = new List<string>();
        foreach (var keyPair in vm.Items)
        {
            if (!selectedIds.Contains(keyPair.Key))
            {
                itemToRemove.Add(keyPair.Key);
            }
        }
            
        foreach (var item in SelectedItems)
        {
            if (vm.Items.ContainsKey(item.Id))
            {
                continue;
            }
                
            vm.Items.Add(item.Id, 0);
        }
        foreach (var key in itemToRemove)
        {
            vm.Items.Remove(key);
        }
    }
}