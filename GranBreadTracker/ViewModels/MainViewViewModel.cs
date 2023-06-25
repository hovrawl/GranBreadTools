using System;
using System.Collections.Generic;
using Avalonia.Collections;
using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using GranBreadTracker.Pages;

namespace GranBreadTracker.ViewModels;

public class MainViewViewModel : MainPageViewModelBase
{
    public MainViewViewModel()
    {
        NavigationFactory = new NavigationFactory(this);
    }

    public NavigationFactory NavigationFactory { get; }
}

public class NavigationFactory : INavigationPageFactory
{
    public NavigationFactory(MainViewViewModel owner)
    {
        Owner = owner;
    }

    public MainViewViewModel Owner { get; }

    public Control GetPage(Type srcType)
    {
        return null;
    }

    public Control GetPageFromObject(object target)
    {
        if (target is HomePageViewModel)
        {
            return new HomePage
            {
                DataContext = target
            };
        }
        else if (target is ItemTrackerPageViewModel)
        {
            return new ItemTrackerPage()
            {
                DataContext = target
            };
        }
        else if (target is ItemsPageViewModel)
        {
            return new ItemsPage()
            {
                DataContext = target
            };
        }
        else if (target is SettingsPageViewModel)
        {
            return new SettingsPage
            {
                DataContext = target
            };
        }
        
        // Default to home page
        return new HomePage
        {
            DataContext = target
        };
    }

}