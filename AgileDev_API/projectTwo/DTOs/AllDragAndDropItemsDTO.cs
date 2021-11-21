using System;
using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.DTOs
{
	public class AllDragAndDropItemsDTO
	{
		public List<DragAndDropItemDTO> ToDo { get; set; }
		public List<DragAndDropItemDTO> Doing { get; set; }
		public List<DragAndDropItemDTO> Done { get; set; }
	}
}
