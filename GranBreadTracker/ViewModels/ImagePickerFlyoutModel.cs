using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia.Controls;
using GranBreadTracker.Controls;

namespace GranBreadTracker.ViewModels;

public class ImagePickerFlyoutModel : ViewModelBase
{
    public ObservableCollection<Image> Images { get; }

    private Flyout _flyout;
    private ImagePickerFlyout _flyoutContent;

    public ImagePickerFlyoutModel(ImagePickerFlyout flyoutContent, Flyout flyout)
    {
        _flyout = flyout;
        _flyoutContent = flyoutContent;
    }
}