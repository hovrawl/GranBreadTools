using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GranBreadTracker.Pages;

public partial class DropSourceTrackerPage : UserControl
{
    public DropSourceTrackerPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}