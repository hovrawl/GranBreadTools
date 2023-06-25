using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia.Controls;

namespace GranBreadTracker.ViewModels;

public class ImagePickerFlyoutModel : ViewModelBase
{
    public ObservableCollection<Image> Images { get; }
}