using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Controls;

public partial class GranblueObjectPickerPage : UserControl
{
    public GranblueObjectPickerPage()
    {
        
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void InitializeObjects()
    {
        if (DataContext is GranblueObjectPickerViewModel vm)
        {
            vm.Initialize(this);
        }
    }
}