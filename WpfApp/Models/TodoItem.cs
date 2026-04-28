using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models;

public class TodoItem
{
	//auto property
	public string TodoText { get; set; } = string.Empty;
	public bool IsDone { get; set; } = false;
}
