using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using DynamicData;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Controls;

public partial class GranblueIconPicker : UserControl
{
    private ListBox _list;
    private DropDownButton _imageBtn;
    //public Window MainWindow;

    private ImageIconSource _icon;
    public ImageIconSource Icon {
        get => _icon;
        set
        {
            _icon = value;
            IconChanged?.Invoke(_imageBtn, new IconChangedEventArgs(_icon));
        }
    }
    
    public EventHandler<IconChangedEventArgs> IconChanged;
    
    public GranblueIconPicker()
    {
        InitializeComponent();
        
        _imageBtn = this.FindControl<DropDownButton>("ImageSelector");

        SetupImagePicker(_imageBtn);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void SetupImagePicker(DropDownButton btn)
    {
        if(btn == null) return;
        
        // Setup image list
        foreach (var iconName in IconNameList)
        {
            if(!App.Current.Resources.TryGetResource(iconName, null, out var icon)) continue;
            if(icon is not ImageIconSource iconSource) continue;
            Icons.Add(iconSource);
        }

        var userIcons = App.Current.Resources.Keys.Where(i => i.ToString().StartsWith("user-icon"));
        foreach (var iconName in userIcons)
        {
            if(!App.Current.Resources.TryGetResource(iconName, null, out var icon)) continue;
            if(icon is not ImageIconSource iconSource) continue;
            Icons.Add(iconSource);
        }

        var firstIcon = Icons.FirstOrDefault();
        _imageBtn.DataContext = firstIcon;
        Icon = firstIcon;
        
        // setup image flyout
        var imagePickerFlyout = SetupImagePickerFlyout();
        btn.Flyout = imagePickerFlyout;
        
        
        
    }
    
    private Flyout SetupImagePickerFlyout()
    {
        var imagePickerFlyoutModel = new ImagePickerFlyoutModel();
        
        var imagePickerFlyout = new ImagePickerFlyout(Icons)
        {
            DataContext = imagePickerFlyoutModel,
        };
        
        var returnFlyout = new Flyout
        {
            Content = imagePickerFlyout
        };
        
        imagePickerFlyout.ImageListSelectEventHandler += (sender, args) =>
        {
            returnFlyout.Hide();
            var selectedImage = imagePickerFlyout.GetSelectedImage();
            _imageBtn.DataContext = selectedImage;
            Icon = selectedImage;
            //IconChanged?.Invoke(_imageBtn, new IconChangedEventArgs(selectedImage));
        };
        
        return returnFlyout;
    }

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        // select image from file system to upload into app
        var dlg = new OpenFileDialog();
        dlg.Filters.Add(new FileDialogFilter() { Name = "Images", Extensions = { "bmp", "png", "jpg", "jpeg", "gif", "ico" } });
        dlg.AllowMultiple = true;

        var result = await dlg.ShowAsync(new Window());
        if (result != null)
        {
            foreach (var fileName in result)
            {
                var bitmap = new Bitmap(fileName);
                var image = new ImageIconSource()
                {
                    Source = bitmap
                };

                var userImageId = $"user-icon-{Guid.NewGuid().ToString()}";
                App.Current.Resources.Add(userImageId, image);

                AddImageToPicker(image);
            }
        }        
    }

    public ImageIconSource GetCurrentIcon()
    {
        if (_imageBtn == null) return null;
        var flyout = _imageBtn.Flyout as Flyout;
        if (flyout.Content is not ImagePickerFlyout imagePickerFlyout) return null;

        return imagePickerFlyout.GetSelectedImage();
    }

    private void AddImageToPicker(ImageIconSource image)
    {
        if (_imageBtn == null) return;
        var flyout = _imageBtn.Flyout as Flyout;
        if (flyout.Content is not ImagePickerFlyout imagePickerFlyout) return;

        imagePickerFlyout.AddImage(image);
    }

    private List<string> IconNameList = new ()
    {
        "BlueChestIcon",
        "GoldBarIcon",
        "GoldBarYoinkIcon",
        "EternitySandIcon",
        "PBHLIcon",
    };

    public ObservableCollection<ImageIconSource> Icons = new()
    {

    };
    
    public ObservableCollection<ImageIconSource> UserIcons = new()
    {

    };
}

public class IconChangedEventArgs : EventArgs
{
    public ImageIconSource Icon;
    public IconChangedEventArgs(ImageIconSource selectedImage)
    {
        Icon = selectedImage;
    }
}