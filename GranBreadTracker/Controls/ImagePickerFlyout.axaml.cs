using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using DynamicData;
using FluentAvalonia.UI.Controls;

namespace GranBreadTracker.Controls;

public partial class ImagePickerFlyout : UserControl
{
    private ListBox _imageList;

    public EventHandler<SelectionChangedEventArgs> ImageListSelectionChangedEventArgs;
    public EventHandler<EventArgs> ImageListSelectEventHandler;

    public ImagePickerFlyout(ICollection<ImageIconSource> preloadIcons)
    {
        InitializeComponent();

        _imageList = this.FindControl<ListBox>("ImageList");

        PreloadImageList(preloadIcons);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void PreloadImageList(ICollection<ImageIconSource> preloadIcons)
    {
        if (preloadIcons == null) return;
        foreach (var icon in preloadIcons)
        {
            _imageList.Items.Add(icon);
        }
    }

    public void AddImage(ImageIconSource image)
    {
        _imageList?.Items?.Add(image);
    }
    
    
    private void ImageList_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        this.ImageListSelectEventHandler?.Invoke(sender, e);
    }

    private void ImageList_OnTapped(object? sender, TappedEventArgs e)
    {
        if (sender is not ListBox listBox) return;

        if (listBox.SelectedValue == null) return;
        
        this.ImageListSelectEventHandler?.Invoke(sender, e);

    }

    public void SetSelectedImage(ImageIconSource icon)
    {
        _imageList.SelectedValue = icon;
    }

    public ImageIconSource GetSelectedImage()
    {
        return _imageList.SelectedValue as ImageIconSource;
    }
}