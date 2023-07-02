using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Classes;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class ItemsPageViewModel : MainPageViewModelBase
{
    public ItemsPageViewModel()
    {
        // Load items from storage/settings
        Items = new ObservableCollection<ItemDefDialogViewModel>();
        if (App.Current.Resources.TryGetResource("Items", null, out var items))
        {
            if (items is List<ItemDef> itemDefs)
            {
                foreach (var item in itemDefs)
                {
                    Items.Add(item.ToViewModel());
                }
            }
        }
        AddItemCommand = new GeneralCommand(ItemDefDialogExecute);

    }
    
    public ObservableCollection<ItemDefDialogViewModel> Items { get; }

    public GeneralCommand AddItemCommand { get; }
    
    private void ItemDefDialogExecute(object obj)
    {
        // Pop up item tracker creation dialog 
        var existing = obj as ItemDefDialogViewModel;
        ItemDefDialog(existing);
    }
    
    private async void ItemDefDialog(ItemDefDialogViewModel existing)
    {
        var dialog = new ContentDialog
        {
            Title = "New Item",
            PrimaryButtonText = "Create",
            CloseButtonText = "Cancel"
        };

        // Pass the dialog if you need to hide it from the ViewModel.
        var viewModel = new ItemDefDialogViewModel();
        if (existing != null)
        {
            // If editing an existing item, pre-fill details
            viewModel.Name = existing.Name;
            viewModel.Icon = existing.Icon;
            dialog.PrimaryButtonText = "Save";
        }
        
        // In our case the Content is a UserControl, but can be anything.
        dialog.Content = new ItemDefDialog()
        {
            DataContext = viewModel
        };

        var dialogResult = await dialog.ShowAsync();
        
        if (dialogResult == ContentDialogResult.Primary)
        {
            var newItemDialog = dialog.Content as ItemDefDialog;
            var newItemViewModel = newItemDialog.DataContext as ItemDefDialogViewModel;
            var itemName = newItemViewModel.Name;
            if (!string.IsNullOrEmpty(itemName))
            {
                // If we had added an item and it wasnt existed, add to view model, otherwise it will update
                if(existing == null) Items.Add(viewModel);
                else
                {
                    // If we were editing an existing model, update its properties
                    existing.Name = viewModel.Name;
                    existing.Icon = viewModel.Icon;
                }
                
                SaveItems();
            }
           
        }
    }

    private void SaveItems()
    {
        Stream stream = null;
        try
        {
            var items = Items.Select(item => item.ToDef()).ToList();
            var text = JsonSerializer.Serialize(items);
            
            var uri = new Uri("avares://GranBreadTracker/Assets/Data/Items.json");
            stream = AssetLoader.Open(uri);

            //var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //var filePath = uri.LocalPath;
            //var writePath = Path.Combine(baseDirectory, filePath);
            //var writePath = $"{AppDomain.CurrentDomain.BaseDirectory}/{filePath}";
            //File.WriteAllText(writePath,text);
            // stream.Write(writeStream);

            // var file = TopLevel.GetTopLevel(new Window()).StorageProvider.TryGetFileFromPathAsync(uri);
            //
            // var writeStream = JsonSerializer.SerializeToUtf8Bytes(items);
            // var writeFile = file.Result.OpenWriteAsync();
            // var writeFileResult =  writeFile.Result;
            // writeFileResult.Write(writeStream);


            stream.WriteAsync(new byte[] {});
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Dispose();
        }
        catch
        {

        }
        finally
        {
            stream?.Dispose();
        }
        
    }
}