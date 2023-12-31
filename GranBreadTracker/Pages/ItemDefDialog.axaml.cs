﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Controls;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Pages;

public partial class ItemDefDialog : UserControl
{
    public ItemDefDialog()
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

        var dialogContext = DataContext as ItemDefDialogViewModel;

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
}