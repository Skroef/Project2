using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
    public interface IDragAndDropService
	{
		AllDragAndDropItemsDTO getAllDragAndDropItems();
		void AddItemToDragAndDrop(AddDragAndDropItemDTO addItem);
	}
}
