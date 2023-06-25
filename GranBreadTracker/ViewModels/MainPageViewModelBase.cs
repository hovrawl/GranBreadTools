namespace GranBreadTracker.ViewModels;

public class MainPageViewModelBase : ViewModelBase
{
    public string NavHeader { get; set; }

    public string IconKey { get; set; }

    public bool ShowsInFooter { get; set; }
}