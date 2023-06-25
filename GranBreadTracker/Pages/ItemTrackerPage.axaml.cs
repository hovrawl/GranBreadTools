using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GranBreadTracker.Pages;

public partial class ItemTrackerPage : UserControl
{
    public ItemTrackerPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}