using System;
using System.Windows.Input;

namespace WpfApp.Commands;

// Zwischenlink wpf button -> Methode
public class RelayCommand : ICommand
{
	//field
	private readonly Action<object?> _execute; 
	private readonly Predicate<object?>? _canExecute; 

	//Konstruktor
	public RelayCommand (Action<object?> execute, Predicate<object?>? canExecute = null)
	{
		_execute = execute ?? throw new ArgumentException(nameof(execute));
		_canExecute = canExecute;
	}

	public bool CanExecute(object? parameter)// aktivieren oder deaktivieren den Button
	{
		if(_canExecute == null)
		{
			return true;
		}

		return _canExecute(parameter);
	}

	public void Execute(object? parameter) //speichern die auszuführende Methode
	{
		_execute(parameter);
	}

	public event EventHandler? CanExecuteChanged; // informieren Zustandsänderung

	public void RaiseCanExecuteChanged() //benachrichtigen WPF, CanExecute ernuet zu überprüfen
	{
		CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
