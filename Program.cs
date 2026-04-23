using System.Collections.Generic;
using System.Text.Json;

namespace TodoList;

//Struktur instabil aufgrund verschachtelter Schleifen -> while(auswahl == 1) never ending -> ture or false
//Keine Überprüfung des Löschbereichs -> nummer >= 1 && nummer =< todoList.count
//Leere Werte zulässig -> string.IsNullOrWhiteSpace
//Fehler bei der Zahlenausgabe -> i
//Doppelter Code -> Methode

class Program
{
	// Gemeinsamer Dateiname für Speichern und Laden
	const string FilePath = "todolist.json";
	// class field -> static local function capture it
	static void Main(string[] args)
	{		
		List<TodoItem> todoList = LoadTodo(); //json file
		bool start = true;

		// static // Gemeinsame Funktion, die keine Objektzustände, sondern Eingabewerte verarbeitet
		static void AddTodo(List<TodoItem> todoList) //Methode
		{
			Console.Clear();

			Console.Write("Aufgabe: \n");
			string aufgaben = Console.ReadLine() ?? "";
			//Verhindern, dass leere Aufgaben in der Liste angezeigt werden.
			if (string.IsNullOrWhiteSpace(aufgaben))
			{
				Console.WriteLine("Leere Aufgaben sind nicht erlaubt.");
			}
			else
			{
				todoList.Add(new TodoItem
				{
					TodoText = aufgaben,
					IsDone = false
				});
				SaveTodo(todoList);
				Console.WriteLine("ToDo wurde hinzugefügt.");
			}
			Console.ReadKey();
		}
		static void DeleteTodo(List<TodoItem> todoList)
		{
			Console.Clear();
			//Funktionen zum Löschen/Abschließen schützen, wenn die Liste leer ist (für Sicherheit)
			if (todoList.Count == 0)
			{
				Console.WriteLine("Keine ToDos vorhanden...\n");
				Console.ReadKey();
				return;
			}
			else
			{
				for (int i = 0; i < todoList.Count; i++)
				{
					string status = todoList[i].IsDone ? "[x]" : "[ ]";
					Console.WriteLine($"Nr.{i + 1} {status} {todoList[i].TodoText}");
				}
			}

			Console.Write("Zu löschende Nr.: ");
			if (int.TryParse(Console.ReadLine(), out int nummer))
			{
				//Bereichsprüfung beim Löschen
				if (nummer >= 1 && nummer <= todoList.Count)
				{
					todoList.RemoveAt(nummer - 1);
					SaveTodo(todoList);
					Console.WriteLine("Todo wurde gelöscht");
				}
				else
				{
					Console.WriteLine("ungültige Nummmer.");
				}
			}
			else
			{
				Console.WriteLine("Bitte eine gültige Nummer eingeben.");
			}
			Console.ReadKey();
		}
		static void DoneTodo(List<TodoItem> todoList)
		{
			Console.Clear();

			var doneYetTodo = todoList.Where(x => !x.IsDone).ToList(); //unerledigte Todos anzeigen

			if(doneYetTodo.Count == 0)
			{
				Console.WriteLine("Keine ToDos");
			}
			foreach(TodoItem item in doneYetTodo)
			{				
				for (int i = 0; i < doneYetTodo.Count; i++)
				{
					Console.WriteLine($"Nr.{i + 1} [ ] {todoList[i].TodoText}");
				}
			}
			
			Console.Write("Erledigte Nr.: ");

			if (int.TryParse(Console.ReadLine(), out int nummer))
			{
				if (nummer >= 1 && nummer <= doneYetTodo.Count)
				{
					doneYetTodo[nummer - 1].IsDone = true;
					SaveTodo(todoList);
					Console.WriteLine("Todo wurde als erledigt markiert.");
				}
				else
				{
					Console.WriteLine("ungültige Nummer.");
				}
			}
			else
			{
				Console.WriteLine("Bitte eine gültige Nummer eingeben.");
			}

			Console.ReadKey();
		}
		static void DoneYetTodo(List<TodoItem> todoList) 
		{
			Console.Clear();

			var doneTodo = todoList.Where(x => x.IsDone).ToList(); // erledigte ToDos anzeigen

			if(doneTodo.Count == 0)
			{
				Console.WriteLine("Möchten Sie wieder machen?");
			}
			foreach(TodoItem item in doneTodo)
			{
				for (int i = 0; i < doneTodo.Count; i++)
				{				
					Console.WriteLine($"Nr.{i + 1} [x] {todoList[i].TodoText}");
				}
			}
			Console.Write("Unerledigte Nr.: ");

			if (int.TryParse(Console.ReadLine(), out int nummer))
			{
				if (nummer >= 1 && nummer <= todoList.Count)
				{
					todoList[nummer - 1].IsDone = false;
					SaveTodo(todoList);
					Console.WriteLine("Todo wurde als unerledigt markiert.");
				}
				else
				{
					Console.WriteLine("ungültige Nummer.");
				}
			}
			else
			{
				Console.WriteLine("Bitte eine gültige Nummer eingeben.");
			}
			Console.ReadKey();
		}
		static void SaveTodo(List<TodoItem> todoList) //json serialize
		{
			// Objekt -> string JsonSerializerOptions steurt das Serialisierungs-/Deserialisierungsverhalten von JSON
			string json = JsonSerializer.Serialize(todoList, new JsonSerializerOptions
			{
				WriteIndented = true //white space
			}); 
			File.WriteAllText(FilePath, json);
		}
		static List<TodoItem> LoadTodo()
		{

			if (!File.Exists(FilePath))
			{
				return new List<TodoItem>();
			}

			try
			{
				string json = File.ReadAllText(FilePath);

				List<TodoItem>? loadedList = JsonSerializer.Deserialize<List<TodoItem>>(json);

				return loadedList ?? new List<TodoItem>();
			}
			catch 
			{
				Console.WriteLine("Die Datei konnte nicht geladen werden.");
				Console.ReadKey();
				return new List<TodoItem>();
			}
		}
		static void ShowTodos(List<TodoItem> todoList)
		{
			if (todoList.Count == 0)
			{
				Console.WriteLine("Keine ToDos vorhanden.\n");
				return;
			}

			//int doneCount = 0;

			for (int i = 0; i < todoList.Count; i++)
			{
				string status = todoList[i].IsDone ? "[x]" : "[ ]";
				Console.WriteLine($"Nr.{i + 1} {status} {todoList[i].TodoText}");
			}

			int doneCount = todoList.Count(x => x.IsDone);
			int doneYetCount = todoList.Count(x => !x.IsDone); //LINQ Count

			Console.WriteLine();
			Console.WriteLine($"Erledigt:     {doneCount}");
			Console.WriteLine($"Unerledigt:   {doneYetCount}");
			
		}
		

		while (start)
		{
			//menü
			Console.Clear();
			Console.WriteLine("==== ToDo List ====\n");
			Console.WriteLine("1. ToDo anzeigen\n");
			Console.WriteLine("0. Beenden\n");
			Console.Write("Auswahl: ");
			if (int.TryParse(Console.ReadLine(), out int auswahl))
			{
				Console.Clear();
				if (auswahl == 1)
				{
					bool zumMenu = true;
					while (zumMenu) 
					{
						Console.Clear();
						Console.WriteLine("== ToDo List anzeigen ==\n");
						
						ShowTodos(todoList);

						Console.WriteLine();
						Console.WriteLine("=======================\n");
						Console.WriteLine("1. ToDo hinzufügen\n");
						Console.WriteLine("2. ToDo löschen\n");
						Console.WriteLine("3. ToDo erledigt\n");
						Console.WriteLine("4. ToDo unerledigt\n");
						Console.WriteLine("0. zurück zum Menü\n");
						Console.WriteLine("=======================");
						Console.Write("Auswahl: ");

						if(int.TryParse(Console.ReadLine(), out int auswahl1))
						{
							if (auswahl1 == 1)
							{
								AddTodo(todoList);
							}
							else if (auswahl1 == 2)
							{
								DeleteTodo(todoList);
							}
							else if (auswahl1 == 3)
							{
								DoneTodo(todoList);
							}
							else if (auswahl1 == 4)
							{
								DoneYetTodo(todoList);
							}
							else if (auswahl1 == 0)
							{
								break;
							}
							//ausgewälte Aufgabe anzeigen
							//erldigt oder löschen
						}
						else
						{
							Console.WriteLine("Ungültige Eingabe.");
							Console.ReadKey();
						}
					}
				}
				if (auswahl == 0)
				{
					Console.WriteLine("Tschüss!");
					break;
				}
			}
			else
			{
				Console.WriteLine("Ungültige Eingabe. Bitte geben Sie 1 oder 0 ein.");
				Console.ReadKey();
				Console.Clear();
			}
		}
	}
}

class TodoItem //List object
{
	public string TodoText { get; set; } = "";
	public bool IsDone { get; set; } = false;
}