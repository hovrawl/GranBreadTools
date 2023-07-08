using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.VisualTree;
using DynamicData;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Controls;

public partial class GranblueIconPicker : UserControl
{
    private DropDownButton _imageBtn;

    public GranblueIconPicker()
    {
        InitializeComponent();
        
        var iconPickerViewModel = new GranblueIconPickerViewModel(this);

        DataContext = iconPickerViewModel;
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
                var image = new ImageIconSource
                {
                    Source = bitmap
                };
                

                var userImageId = $"user-icon-{Guid.NewGuid().ToString()}";
                App.Current.Resources.Add(userImageId, image);
                
                var icon = new GranBreadIcon
                {
                    IconKey = userImageId,
                    IconSource = image,
                    IconType = IconType.User
                };
                
                // TODO - Save image to disk to use/load when app re-launches
                AddImageToPicker(icon);
            }
        }        
    }
    
    private void AddImageToPicker(GranBreadIcon icon)
    {
        if (DataContext is not GranblueIconPickerViewModel vm) return;

        vm.AddImageToPicker(icon);
    }
}