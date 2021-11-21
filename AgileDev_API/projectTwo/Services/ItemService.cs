using projectTwo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using projectTwo.DTOs;
using projectTwo.Database;

namespace projectTwo.Services
{
    public class ItemService : IItemService
    {
        private readonly ProjectTwoContext _projectTwoContext;
        private readonly IProjectTwoUnitOfWork _projectTwoUnitOfWork;

        public ItemService(ProjectTwoContext projectTwoContext, IProjectTwoUnitOfWork projectTwoUnitOfWork)
        {
            _projectTwoContext = projectTwoContext;
            _projectTwoUnitOfWork = projectTwoUnitOfWork;
        }

        public List<ItemDisplayDTO> getItem()
		{
            var item = _projectTwoUnitOfWork.Item.Query(x => x.Id == 1).Select(x => x.ItemDisplayDTO).ToList();
            
            return item;
        }
    }
}
