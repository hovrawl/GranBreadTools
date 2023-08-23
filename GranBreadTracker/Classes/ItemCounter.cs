using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Classes;

public class ItemCounter: ViewModelBase
{
    private string _id;
    
    public string Id 
    {  
        get => _id;
        set
        {
            if (RaiseAndSetIfChanged(ref _id, value))
            {
                
            }
        }
    }

    private string _name;
    public string Name 
    {  
        get => _name;
        set
        {
            if (RaiseAndSetIfChanged(ref _name, value))
            {
                
            }
        }
    }

    private long _count;
    public long Count 
    {  
        get => _count;
        set
        {
            if (RaiseAndSetIfChanged(ref _count, value))
            {
                
            }
        }
    }

    private double _dropRate;
    public double DropRate 
    {  
        get => _dropRate;
        set
        {
            if (RaiseAndSetIfChanged(ref _dropRate, value))
            {
                
            }
        }
    }

    private GranBreadIcon _icon;
    public GranBreadIcon Icon 
    {  
        get => _icon;
        set
        {
            if (RaiseAndSetIfChanged(ref _icon, value))
            {
                
            }
        }
    }

}