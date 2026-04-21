using System.Collections.Generic;

namespace TodoList;
class Program
{
	static void Main(string[] args)
	{
		List<string> todoList = new List<string>();
		bool start = true;

		while (start)
		{
			//menü
			Console.WriteLine("==== ToDo List ====\n");
			Console.WriteLine("1. ToDo anzeigen\n");
			Console.WriteLine("0. Beenden\n");
			Console.Write("Auswahl: ");
			if (int.TryParse(Console.ReadLine(), out int auswahl))
			{
				Console.Clear();
				
				while (auswahl == 1) //Aufgaben anzeigen
				{					
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
							Console.Clear();

							Console.Write("Aufgabe: \n");
							string aufgaben = Console.ReadLine() ?? "";
							todoList.Add(aufgaben);
							Console.WriteLine("ToDo wurde hinzugefügt.");
							Console.ReadKey();
						}
						else if(auswahl1 == 0)
						{
							Console.ReadKey();
						}
					}
					else if (todoList.Count > 0)
					{
						int ordnung = 1;
						for (int i = 0; i < todoList.Count; i++)
						{
							ordnung = ordnung + i;
							Console.WriteLine($"Nr.{ordnung}: {todoList[i]}");
						}

						Console.WriteLine("1. ToDo hinzufügen\n");
						Console.WriteLine("2. ToDo löschen\n");
						Console.WriteLine("0. zurück zum Menü\n");
						Console.Write("Auswahl: ");

						int.TryParse(Console.ReadLine(),out int auswahl2);


						if (auswahl2 == 1)
						{
							Console.Clear();

							Console.Write("Aufgabe: \n");
							string aufgaben = Console.ReadLine() ?? "";
							todoList.Add(aufgaben);
							Console.WriteLine("ToDo wurde hinzugefügt.");
							Console.ReadKey();
						}
						else if (auswahl2 == 2)
						{							
							Console.Clear();
							Console.Write("Zu löschende Nr.: ");
							int.TryParse(Console.ReadLine(), out int nummer);
							todoList.RemoveAt(nummer -1);
							Console.WriteLine("Todo wurde gelöscht"); 
							Console.ReadKey();
						}
						else
						{
							Console.Clear();
							Console.ReadKey();
						}
						//ausgewälte Aufgabe anzeigen
						//erldigt oder löschen

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
				Console.WriteLine("Ungülitge Eingabe. Bitte geben Sie eine Zahl(1-4 oder 0) ein.");
				Console.ReadKey();
				Console.Clear();
			}
		}

	}
	//0. Programm beenden
	//Auswahl:

}