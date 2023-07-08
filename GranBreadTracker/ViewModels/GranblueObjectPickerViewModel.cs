using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Classes.Data;
using GranBreadTracker.Controls;

namespace GranBreadTracker.ViewModels;

public class GranblueObjectPickerViewModel : ViewModelBase
{
    private GranblueObjectPickerPage _picker;
    private DropDownButton _btn;
    
    public GranblueObject GranblueObject { get; set; }
    
    public ObservableCollection<GranblueObject> GranblueObjects { get; set; }

    public List<GranblueObjectType> Types { get; set; } = new List<GranblueObjectType>();

    /// <summary>
    /// New GranblueObjectPickerViewModel from a specified type
    /// </summary>
    /// <param name="type">The specific Granblue Object Type</param>
    public GranblueObjectPickerViewModel(GranblueObjectType type)
    {
        Types.Add(type);
    }
    
    /// <summary>
    /// New GranblueObjectPickerViewModel from a list of specified types
    /// </summary>
    /// <param name="types">The lsit of specific Granblue Object Types</param>
    public GranblueObjectPickerViewModel(List<GranblueObjectType> types)
    {
        Types.AddRange(types);
    }
    
    public void Initialize(GranblueObjectPickerPage picker)
    {
        _picker = picker;
        GranblueObjects = new ObservableCollection<GranblueObject>();
        // Iterate over specified types and get GranblueObjects
        foreach (var type in Types)
        {
            switch (type)
            {
                case GranblueObjectType.Item:
                {
                    var allItems = DataManager.Items().All();
                    allItems.ForEach(item => GranblueObjects.Add(new GranblueObject(item)));
                    break;
                }
                case GranblueObjectType.Source:
                {
                    var allSources = DataManager.ItemSources().All();
                    allSources.ForEach(source => GranblueObjects.Add(new GranblueObject(source)));
                    break;
                }
                case GranblueObjectType.Tracker:
                {
                    var allTrackers = DataManager.ItemTrackerDefs().All();
                    allTrackers.ForEach(trackerDef => GranblueObjects.Add(new GranblueObject(trackerDef)));
                    break;
                }
            }
        }

        // Setup initial GranblueObject
        GranblueObject = GranblueObjects.FirstOrDefault();
        
        // Setup flyout
        var pickerFlyout = SetupPickerFlyout();

        _btn = _picker.FindControl<DropDownButton>("ObjectPicker");

        _btn.Flyout = pickerFlyout;
        _btn.DataContext = GranblueObject;
    }
    
    
    private Flyout SetupPickerFlyout()
    {
        var flyout = new GranblueObjectPickerFlyout(GranblueObjects);
        var returnFlyout = new Flyout
        {
            Content = flyout
        };

        
        flyout.ObjectPickerSelectEventHandler += (sender, args) =>
        {
            returnFlyout.Hide();
            var selectedImage = flyout.GetSelectedImage();
            GranblueObject = selectedImage;
            _btn.DataContext = GranblueObject;
        };
        
        return returnFlyout;
    }
}