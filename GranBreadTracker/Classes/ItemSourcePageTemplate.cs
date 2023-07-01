using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Pages;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Classes;

public class ItemSourcePageTemplate : IDataTemplate
{
    [Content]
    public object? Content { get; set; }
    
    [DataType]
    public ItemSourcePageViewModel DataContext { get; set; }
    
    public Control? Build(object? param)
    {
        // build the control to display
        var vm = param as ItemSourcePageViewModel;
        DataContext = vm;

        var itemSourcePage = new ItemSourcePage
        {
            DataContext = vm
        };
        
        // Setup Tab Item
        var tabItem = new TabViewItem
        {
            Header = vm.Source.Name,
            IconSource = vm.Source.IconSource,
            // Content of the tab item is the item source page
            Content = itemSourcePage
        };

        return tabItem;
    }

    public bool Match(object? data)
    {
        // Check if we can accept the provided data
        return data is ItemSourcePageViewModel;
    }
}