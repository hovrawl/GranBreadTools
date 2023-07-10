using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using GranBreadTracker.Classes;

namespace GranBreadTracker.Controls;

public partial class GranblueObjectPickerList : UserControl
{
    private readonly ListBox? _objectPickerList;

    public EventHandler<EventArgs> ObjectPickerSelectEventHandler;

    /// <summary>
    /// Initialize Object Picker list with objects
    /// </summary>
    /// <param name="objects">List of objected to populate the list with</param>
    /// <param name="multiSelect">Configure list for multiselect</param>
    public GranblueObjectPickerList(ICollection<GranblueObject> objects, bool multiSelect)
    {
        InitializeComponent();
        
        _objectPickerList = this.FindControl<ListBox>("ObjectPicker");
        // If the List if not for a flyout, 
        if (multiSelect)
        {
            _objectPickerList.SelectionMode = SelectionMode.Multiple;
        }
        PreloadObjectList(objects);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void ObjectPicker_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        this.ObjectPickerSelectEventHandler?.Invoke(sender, e);

    }

    private void ObjectPicker_OnTapped(object? sender, TappedEventArgs e)
    {
        if (sender is not ListBox listBox) return;

        if (listBox.SelectedValue == null) return;
        
        //this.ObjectPickerSelectEventHandler?.Invoke(sender, e);
    }
    
    private void PreloadObjectList(ICollection<GranblueObject> preloadObjects)
    {
        if (preloadObjects == null) return;
        foreach (var icon in preloadObjects)
        {
            _objectPickerList.Items.Add(icon);
        }
    }
    
    public GranblueObject GetSelectedObject()
    {
        return _objectPickerList.SelectedValue as GranblueObject;
    }
    
    public ICollection<GranblueObject> GetSelectedObjects()
    {
        return _objectPickerList.Selection.SelectedItems.OfType<GranblueObject>().ToList();
    }
}