using System.Windows.Input;

namespace WpfApp.Commands;

// Zwischenlink wpf button -> Methode
public class RelayCommands : ICommand
{
	//field
	private readonly Action _execute; 
	private readonly Func<bool>? _canExecute; 

	//Konstruktor
	public RelayCommands (Action execute, Func<bool>? canExecute = null)
	{
		execute = _execute;
		canExecute = _canExecute;
	}

	public bool CanExecute(object? parameter)// aktivieren oder deaktivieren den Button
	{
		return _canExecute == null || _canExecute();
	}

	public void Execute(object? parameter) //speichern die auszuführende Methode
	{
		_execute();
	}

	public event EventHandler? CanExectueChanged; // informieren Zustandsänderung

	public void RaiseCanExectueChanged() //benachrichtigen WPF, CanExecute ernuet zu überprüfen
	{
		CanExectueChanged?.Invoke(this, EventArgs.Empty);
	}
}
