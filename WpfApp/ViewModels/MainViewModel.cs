using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;

namespace WpfApp.ViewModels;

public class MainViewModel : INotifyPropertyChanged //ändern Daten auf dem Bildschirm
{
	//backing field
	private string _newTodoText = string.Empty;
	private TodoItem? _selectedTodo;

	public ObservableCollection<TodoItem> Todos { get; } = new(); // ListBox automatisch aktualisieren

	//property
	public string NewTodoText //Textbox
	{
		get
		{
			return _newTodoText;
		}
		set
		{
			if (_newTodoText != value)
			{
				_newTodoText = value;
				OnPropertyChanged();

				if(AddCommand is RelayCommand relayCommand)
				{
					relayCommand.RaiseCanExecuteChanged();
				}
			}
		}
	}
	public TodoItem? SelectedTodo //Listbox
	{
		get
		{
			return _selectedTodo;
		}
		set
		{
			if (_selectedTodo != value)
			{
				_selectedTodo = value;
				OnPropertyChanged();
			}
		}
	}

	public ICommand AddCommand { get; } // add button

	public MainViewModel() 
	{
		Todos = new ObservableCollection<TodoItem>();
		AddCommand = new RelayCommand(AddTodo, CanAddTodo);
	}

	private bool CanAddTodo(object? parameter)
	{
		return !string.IsNullOrWhiteSpace(NewTodoText);
	}

	private void AddTodo(object? parameter)
	{
		string trimmedText = NewTodoText.Trim();

		TodoItem newTodo = new TodoItem
		{
			TodoText = trimmedText,
			IsDone = false,
		};

		Todos.Add(newTodo);
		SelectedTodo = newTodo;
		NewTodoText = string.Empty;
	} //function
		public event PropertyChangedEventHandler? PropertyChanged; //notify
		private void OnPropertyChanged([CallerMemberName] string? propertyName = null) //notification function
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

}
