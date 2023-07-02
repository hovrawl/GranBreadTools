﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Controls;

namespace GranBreadTracker.ViewModels;

public class GranblueIconPickerViewModel : ViewModelBase
{
    private GranblueIconPicker _picker;
    
    private ListBox _list;
    private DropDownButton _imageBtn;
    
    public EventHandler<IconChangedEventArgs> IconChanged;

    private GranBreadIcon _icon;
    public GranBreadIcon Icon {
        get => _icon;
        set
        {
            _icon = value;
            IconChanged?.Invoke(this, new IconChangedEventArgs(_icon));
        }
    }
    
    public GranblueIconPickerViewModel(GranblueIconPicker picker)
    {
        _picker = picker;
        
        _imageBtn = _picker.FindControl<DropDownButton>("ImageSelector");

        SetupImagePicker(_imageBtn);
    }
    
    public void AddImageToPicker(GranBreadIcon icon)
    {
        if (_imageBtn == null) return;
        var flyout = _imageBtn.Flyout as Flyout;
        if (flyout.Content is not ImagePickerFlyout imagePickerFlyout) return;

        imagePickerFlyout.AddImage(icon);
    }
    
    
    private void SetupImagePicker(DropDownButton btn)
    {
        if(btn == null) return;
        
        // Setup image list
        foreach (var iconName in IconNameList)
        {
            if(!App.Current.Resources.TryGetResource(iconName, null, out var icon)) continue;
            if(icon is not ImageIconSource iconSource) continue;
            Icons.Add(new GranBreadIcon(iconSource, iconName as string, IconType.Item));
        }

        var userIcons = App.Current.Resources.Keys.Where(i => i.ToString().StartsWith("user-icon"));
        foreach (var iconName in userIcons)
        {
            if(!App.Current.Resources.TryGetResource(iconName, null, out var icon)) continue;
            if(icon is not ImageIconSource iconSource) continue;
            Icons.Add(new GranBreadIcon(iconSource, iconName as string, IconType.User));
        }

        var firstIcon = Icons.FirstOrDefault();
        if (Icon is not null)
        {
            _imageBtn.DataContext = Icon;
        }
        else 
        {
            // Set default icon if null
            // Only null if creating new item, edit should be set
            Icon = firstIcon;
            _imageBtn.DataContext = firstIcon;
        }
       
        
        // setup image flyout
        var imagePickerFlyout = SetupImagePickerFlyout();
        btn.Flyout = imagePickerFlyout;
    }
    
    private Flyout SetupImagePickerFlyout()
    {
        var imagePickerFlyout = new ImagePickerFlyout(Icons);
        var returnFlyout = new Flyout
        {
            Content = imagePickerFlyout
        };
            
        var imagePickerFlyoutModel = new ImagePickerFlyoutModel(imagePickerFlyout, returnFlyout);
        imagePickerFlyout.DataContext = imagePickerFlyoutModel;
        
        imagePickerFlyout.ImageListSelectEventHandler += (sender, args) =>
        {
            returnFlyout.Hide();
            var selectedImage = imagePickerFlyout.GetSelectedImage();
            _imageBtn.DataContext = selectedImage;
            Icon = selectedImage;
        };
        
        return returnFlyout;
    }

    private List<string> IconNameList = new ()
    {
        "BlueChestIcon",
        "GoldBarIcon",
        "GoldBarYoinkIcon",
        "EternitySandIcon",
        "PBHLIcon",
    };

    public ObservableCollection<GranBreadIcon> Icons = new()
    {

    };
    
    public ObservableCollection<GranBreadIcon> UserIcons = new()
    {

    };

    public void SetIcon(GranBreadIcon iconSource)
    {
        if (_imageBtn == null) return;
        _imageBtn.DataContext = iconSource;
    }
}

public class IconChangedEventArgs : EventArgs
{
    public GranBreadIcon Icon;
    public IconChangedEventArgs(GranBreadIcon selectedImage)
    {
        Icon = selectedImage;
    }
}