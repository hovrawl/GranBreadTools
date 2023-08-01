using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Pages;

public partial class GoalsPage : UserControl
{
    public GoalsPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void Item_OnTapped(object? sender, TappedEventArgs e)
    {
        if(DataContext is not GoalPageViewModel vm) return;
        if (sender is not ListBoxItem listItem) return;
        if (listItem.DataContext is not GoalDefDialogViewModel itemDef) return;
        
        vm.AddItemCommand.Execute(itemDef);
    }

    private void AddItem_OnClick(object? sender, RoutedEventArgs e)
    {
        if(DataContext is not GoalPageViewModel vm) return;
        
        vm.AddItemCommand.Execute(null);

    }

    private void ItemList_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox listBox) return;
        if (DataContext is not GoalPageViewModel vm) return;
        if (listBox.SelectedValue is not GoalDefDialogViewModel itemDef) return;

        // Execute Item Def command
        vm.AddItemCommand.Execute(itemDef);

        // Clear list selection so event can be triggered on every selection
        listBox.SelectedItems.Clear();
    }
}