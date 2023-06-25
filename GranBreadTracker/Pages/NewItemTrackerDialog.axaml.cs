using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using GranBreadTracker.Controls;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Pages;

public partial class NewItemTrackerDialog : UserControl
{
    public NewItemTrackerDialog()
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

        var iconPickerViewModel = new GranblueIconPickerViewModel();

        var dialogContext = DataContext as NewItemTrackerDialogViewModel;
        
        iconPicker.DataContext = iconPickerViewModel;

        iconPicker.IconChanged += (o, args) =>
        {
            dialogContext.Icon = iconPicker.Icon;
        };
        
        dialogContext.Icon = iconPicker.Icon;

    }
}