using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GranBreadTracker.Views;

public partial class MainAppSplashContent : UserControl
{
    public MainAppSplashContent()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}