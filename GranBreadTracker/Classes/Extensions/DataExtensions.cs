using GranBreadTracker.ViewModels;

namespace GranBreadTracker.Classes.Extensions;

public static class DataExtensions
{
    /// <summary>
    /// Get this item as a view model
    /// </summary>
    /// <returns></returns>
    public static ItemDefDialogViewModel ToViewModel(this ItemDef itemDef)
    {
        return new ItemDefDialogViewModel
        {
            Id = itemDef.Id,
            Icon = itemDef.Icon,
            Name = itemDef.Name,
            Description = itemDef. Description
        };
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    public static ItemDef ToDef(this ItemDefDialogViewModel viewModel)
    {
        return new ItemDef
        {
            Id = viewModel.Id,
            Icon = viewModel.Icon,
            Name = viewModel.Name,
            Description = viewModel. Description
        };
    }
    
    
    /// <summary>
    /// Get this item source as a view model
    /// </summary>
    /// <returns></returns>
    public static SourceDefDialogViewModel ToViewModel(this ItemSourceDef itemSourceDef)
    {
        return new SourceDefDialogViewModel
        {
            Id = itemSourceDef.Id,
            Icon = itemSourceDef.Icon,
            Name = itemSourceDef.Name,
            Description = itemSourceDef. Description
        };
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    public static ItemSourceDef ToDef(this SourceDefDialogViewModel viewModel)
    {
        return new ItemSourceDef
        {
            Id = viewModel.Id,
            Icon = viewModel.Icon,
            Name = viewModel.Name,
            Description = viewModel. Description
        };
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="goalDef"></param>
    /// <returns></returns>
    public static GoalDefDialogViewModel ToViewModel(this GoalDef goalDef)
    {
        return new GoalDefDialogViewModel
        {
            Id = goalDef.Id,
            Icon = goalDef.Icon,
            Name = goalDef.Name,
            Description = goalDef. Description
        };
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    public static GoalDef ToDef(this GoalDefDialogViewModel viewModel)
    {
        return new GoalDef
        {
            Id = viewModel.Id,
            Icon = viewModel.Icon,
            Name = viewModel.Name,
            Description = viewModel. Description
        };
    }
}