using System;
using System.Collections;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using GranBreadTracker.Classes;

namespace GranBreadTracker.Controls;

public partial class GranblueObjectPickerFlyout : UserControl
{
    private readonly ListBox? _objectPickerList;

    public EventHandler<EventArgs> ObjectPickerSelectEventHandler;

    
    public GranblueObjectPickerFlyout(ICollection<GranblueObject> objects)
    {
        InitializeComponent();
        
        _objectPickerList = this.FindControl<ListBox>("ObjectPicker");

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
        
        this.ObjectPickerSelectEventHandler?.Invoke(sender, e);
    }
    
    private void PreloadObjectList(ICollection<GranblueObject> preloadObjects)
    {
        if (preloadObjects == null) return;
        foreach (var icon in preloadObjects)
        {
            _objectPickerList.Items.Add(icon);
        }
    }
    
    public GranblueObject GetSelectedImage()
    {
        return _objectPickerList.SelectedValue as GranblueObject;
    }
}