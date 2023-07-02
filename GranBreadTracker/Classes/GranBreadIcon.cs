using System.Text.Json.Serialization;
using FluentAvalonia.UI.Controls;

namespace GranBreadTracker.Classes;

public class GranBreadIcon
{
    /// <summary>
    /// Icon Key that matches an icon resource
    /// </summary>
    public string IconKey { get; set; }
    
    /// <summary>
    /// IconSource property populated during runtime for UI
    /// </summary>
    [JsonIgnore]
    public IconSource IconSource { get; set; }
    
    /// <summary>
    /// Type of Icon for UI separation
    /// </summary>
    public IconType IconType { get; set; }

    public GranBreadIcon(IconSource iconSource, string key, IconType type)
    {
        IconSource = iconSource;
        IconKey = key;
        IconType = type;
    }

    public GranBreadIcon()
    {
        
    }
}