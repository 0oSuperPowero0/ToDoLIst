using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;

namespace TodoList;

//Struktur instabil aufgrund verschachtelter Schleifen -> while(auswahl == 1) never ending -> ture or false
//Keine Überprüfung des Löschbereichs -> nummer >= 1 && nummer =< todoList.count
//Leere Werte zulässig -> string.IsNullOrWhiteSpace
//Fehler bei der Zahlenausgabe -> i
//Doppelter Code -> Methode

class Program
{
	static void Main(string[] args)
	{
		List<TodoItem> todoList = LoadTodo(); //json file
		bool start = true;
		
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
			for (int i = 0; i < todoList.Count; i++)
			{
				string status = todoList[i].IsDone ? "[x]" : "[ ]";
				Console.WriteLine($"Nr.{i + 1} {status} {todoList[i].TodoText}");
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
		static void FinishTodo(List<TodoItem> todoList)
		{
			Console.Clear();
			for (int i = 0; i < todoList.Count; i++)
			{
				string status = todoList[i].IsDone ? "[x]" : "[ ]";
				Console.WriteLine($"Nr.{i + 1} {status} {todoList[i].TodoText}");
			}
			Console.Write("Erledigte Nr.: ");

			if (int.TryParse(Console.ReadLine(), out int nummer))
			{
				if (nummer >= 1 && nummer <= todoList.Count)
				{
					todoList[nummer - 1].IsDone = true;
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
		}
		static void NotFinishTodo(List<TodoItem> todoList) 
		{
			Console.Clear();
			for (int i = 0; i < todoList.Count; i++)
			{
				string status = todoList[i].IsDone ? "[x]" : "[ ]";
				Console.WriteLine($"Nr.{i + 1} {status} {todoList[i].TodoText}");
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
		}
		static void SaveTodo(List<TodoItem> todoList) //json serialize
		{
			string filePath = "todolist.json";

			// Objekt -> string JsonSerializerOptions steurt das Serialisierungs-/Deserialisierungsverhalten von JSON
			string json = JsonSerializer.Serialize(todoList, new JsonSerializerOptions
			{
				WriteIndented = true //white space
			}); 
			File.WriteAllText(filePath, json);
		}
		static List<TodoItem> LoadTodo()
		{
			string filePath = "todolist.json";

			if (!File.Exists(filePath))
			{
				return new List<TodoItem>();
			}
			string json = File.ReadAllText(filePath);

			List<TodoItem>? loadedList = JsonSerializer.Deserialize<List<TodoItem>>(json);

			return loadedList ?? new List<TodoItem>();
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
						if (todoList.Count == 0)
						{

							Console.WriteLine("Keine ToDos vorhanden...\n");

							Console.WriteLine("1. ToDo hinzufügen\n");
							Console.WriteLine("0. zurück zum Menü\n");
							Console.Write("Auswahl: ");

							int.TryParse(Console.ReadLine(), out int auswahl1);

							Console.Clear();

							if (auswahl1 == 1)
							{
								AddTodo(todoList);
							}
							else if (auswahl1 == 0)
							{
								Console.ReadKey();
								break;
							}
						}
						else if (todoList.Count > 0)
						{
							for (int i = 0; i < todoList.Count; i++) // Es ist besser, mit i zu rechnen.
							{
								string status = todoList[i].IsDone ? "[x]" : "[ ]";
								Console.WriteLine($"Nr.{i + 1} {status} {todoList[i].TodoText}");
							}

							Console.WriteLine("1. ToDo hinzufügen\n");
							Console.WriteLine("2. ToDo löschen\n");
							Console.WriteLine("3. ToDo erledigt\n");
							Console.WriteLine("4. ToDo unerledigt\n");
							Console.WriteLine("0. zurück zum Menü\n");
							Console.Write("Auswahl: ");

							int.TryParse(Console.ReadLine(), out int auswahl2);

							if (auswahl2 == 1)
							{
								AddTodo(todoList);
							}
							else if (auswahl2 == 2)
							{
								DeleteTodo(todoList);
							}
							else if (auswahl2 == 3)
							{
								FinishTodo(todoList);
							}
							else if (auswahl2 == 4)
							{
								NotFinishTodo(todoList);
							}
							else if (auswahl2 == 0)
							{
								break;
							}
							//ausgewälte Aufgabe anzeigen
							//erldigt oder löschen

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