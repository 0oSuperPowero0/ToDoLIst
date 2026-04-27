using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models;

public class TodoItem
{
	public string TodoText { get; set; } = "";
	public bool IsDone { get; set; } = false;
}
