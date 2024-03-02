using System;
using System.Windows.Input;

namespace GranBreadTracker.ViewModels;

public class PointerCommand : ICommand
{
    public bool IsPrimary { private get; set; }
    
    public PointerCommand(Action<object, bool> executeMethod)
    {
        _executeMethod = executeMethod;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
        _executeMethod.Invoke(parameter, IsPrimary);
    }

    private Action<object, bool> _executeMethod;
}