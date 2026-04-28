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
	private string _newTodoText = "";
	private TodoItem? _selectedTodo;

	public ObservableCollection<TodoItem> Todos { get; } = new(); // statt List

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
		AddCommand = new RelayCommands(AddTodo);
	}

	private void AddTodo()
	{
		if (string.IsNullOrWhiteSpace(NewTodoText))
		{
			return;
		}

		TodoItem newTodo = new TodoItem
		{
			TodoText = NewTodoText,
			IsDone = false,
		};

		Todos.Add(newTodo);
		NewTodoText = "";
	} //function
		public event PropertyChangedEventHandler? PropertyChanged; //notify
		private void OnPropertyChanged([CallerMemberName] string? propertyName = null) //notification function
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

}
