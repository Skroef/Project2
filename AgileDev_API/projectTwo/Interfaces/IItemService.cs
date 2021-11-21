using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
    public interface IItemService
	{
		List<ItemDisplayDTO> getItem();
	}
}
