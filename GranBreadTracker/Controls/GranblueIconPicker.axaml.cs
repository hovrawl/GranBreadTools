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
    private DropDownButton _imageBtn;

    
    public GranblueIconPicker()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
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
    
    private void AddImageToPicker(ImageIconSource image)
    {
        if (_imageBtn == null) return;
        var flyout = _imageBtn.Flyout as Flyout;
        if (flyout.Content is not ImagePickerFlyout imagePickerFlyout) return;

        imagePickerFlyout.AddImage(image);
    }
}