using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
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
        var topLevel = TopLevel.GetTopLevel(this);
        
        var extArray = new [] { "*.bmp", "*.png", "*.jpg", "*.jpeg", "*.gif", "*.ico" };
        var options = new FilePickerOpenOptions
        {
            Title = "Select Image",
            AllowMultiple = true,
            FileTypeFilter = new FilePickerFileType[]
            {
                new("Images") { Patterns = extArray },
            }
        };
        
        var result = await topLevel.StorageProvider.OpenFilePickerAsync(options);
        if (result != null)
        {
            foreach (var fileName in result)
            {
                var bitmap = new Bitmap(await fileName.OpenReadAsync());
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
        if (DataContext is not GranblueIconPickerViewModel vm) return;

        vm.AddImageToPicker(image);
    }
}