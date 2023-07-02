using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Pages;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Classes.Templates;

public class ItemTrackerPageTemplate : IDataTemplate
{
    [Content]
    public object? Content { get; set; }
    
    [DataType]
    public ItemTrackerPageViewModel DataContext { get; set; }
    
    public Control? Build(object? param)
    {
        // build the control to display
        var vm = param as ItemTrackerPageViewModel;
        DataContext = vm;

        var itemTrackerPage = new ItemTrackerPage
        {
            DataContext = vm
        };
        
        // Setup Tab Item
        var tabItem = new TabViewItem
        {
            Header = vm.ItemTrackerDef.Name,
            IconSource = vm.ItemTrackerDef.Icon.IconSource,
            // Content of the tab item is the item source page
            Content = itemTrackerPage
        };

        return tabItem;
    }

    public bool Match(object? data)
    {
        // Check if we can accept the provided data
        return data is ItemTrackerPageViewModel;
    }
}