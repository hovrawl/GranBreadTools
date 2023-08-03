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
    public GranblueObjectPickerList(ICollection<GranblueObject> objects, bool multiSelect, IEnumerable<string> selectedIds = null)
    {
        InitializeComponent();
        
        _objectPickerList = this.FindControl<ListBox>("ObjectPicker");

        // Base selection
        var selectionMode = SelectionMode.Toggle;

        // If the List if not for a flyout, 
        if (multiSelect)
        {
            // add multi-select
            selectionMode = selectionMode|SelectionMode.Multiple;
        }
        else
        {
            // add single-select
            selectionMode = selectionMode|SelectionMode.Single|SelectionMode.AlwaysSelected;
        }
        _objectPickerList.SelectionMode = selectionMode;
        
        PreloadObjectList(objects);

        if (selectedIds != null)
        {
            PreSelectObjects(selectedIds);
        }
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

    private void PreSelectObjects(IEnumerable<string> selectedIds)
    {
        if (!selectedIds.Any()) return;

        var loadedItems = _objectPickerList.Items.OfType<GranblueObject>().ToList();
        var selectedItems = loadedItems.Where(i => selectedIds.Contains(i.Id)).ToList();

        _objectPickerList.SelectedItems = selectedItems;
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