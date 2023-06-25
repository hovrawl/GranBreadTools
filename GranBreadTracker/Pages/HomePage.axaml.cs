using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GranBreadTracker.Pages;

public partial class HomePage : UserControl
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}