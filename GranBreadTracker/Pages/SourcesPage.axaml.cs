using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Pages;

public partial class SourcesPage : UserControl
{
    public SourcesPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void AddSource_OnClick(object? sender, RoutedEventArgs e)
    {
        if(DataContext is not SourcesPageViewModel vm) return;
        
        vm.AddCommand.Execute(null);

    }

    private void ItemList_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox listBox) return;
        if (DataContext is not SourcesPageViewModel vm) return;
        if (listBox.SelectedValue is not SourceDefDialogViewModel itemDef) return;

        // Execute Item Def command
        vm.AddCommand.Execute(itemDef);

        // Clear list selection so event can be triggered on every selection
        listBox.SelectedItems.Clear();
    }
}