using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;

namespace GranBreadTracker.Styling;

public class GranBreadIconSource : ImageIconSource
{
    /// <summary>
    /// Gets or sets the <see cref="IconType"/> property
    /// </summary>
    public static readonly StyledProperty<IconType> IconTypeProperty =
        AvaloniaProperty.Register<GranBreadIconSource, IconType>(nameof(IconType));

    /// <summary>
    /// Gets or sets the <see cref="IconType"/> property for this icon
    /// </summary>
    [Content]
    public IconType IconType
    {
        get => GetValue(IconTypeProperty);
        set => SetValue(IconTypeProperty, value);
    }

}