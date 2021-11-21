using System;
using projectTwo.DTOs;

namespace projectTwo.DTOs
{
	public class DragAndDropItemDTO
	{
		public string Topic { get; set; }
		public string ToDo { get; set; }
		public int Placement { get; set; }
		public int Number { get; set; }
	}
}
