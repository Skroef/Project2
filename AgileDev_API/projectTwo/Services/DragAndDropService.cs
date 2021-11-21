using projectTwo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using projectTwo.DTOs;
using projectTwo.Database;
using projectTwo.Models;
using Microsoft.EntityFrameworkCore;

namespace projectTwo.Services
{
    public class DragAndDropService : IDragAndDropService
    {
        private readonly ProjectTwoContext _projectTwoContext;
        private readonly IProjectTwoUnitOfWork _projectTwoUnitOfWork;

        public DragAndDropService(ProjectTwoContext projectTwoContext, IProjectTwoUnitOfWork projectTwoUnitOfWork)
        {
            _projectTwoContext = projectTwoContext;
            _projectTwoUnitOfWork = projectTwoUnitOfWork;
        }

        public AllDragAndDropItemsDTO getAllDragAndDropItems()
        {
            var allItems = _projectTwoUnitOfWork.DragAndDrop.Query().ToList().Select(x => x.DragAndDropItemDto).ToList();

            return new AllDragAndDropItemsDTO
            {
                ToDo = allItems.Where(x => x.Placement == 1).ToList().OrderBy(x => x.Number).ToList(),
                Doing = allItems.Where(x => x.Placement == 2).ToList().OrderBy(x => x.Number).ToList(),
                Done = allItems.Where(x => x.Placement == 3).ToList().OrderBy(x => x.Number).ToList()
            };

        }

        public void AddItemToDragAndDrop(AddDragAndDropItemDTO addItem)
        {
            var totalToDo = _projectTwoUnitOfWork.DragAndDrop.Count(x => x.Placement == 1);

            var newItemToAdd = new DragAndDrop
            {
                Topic = addItem.Topic,
                ToDo = addItem.ToDo,
                Placement = 1,
                Number = (int)(totalToDo + 1),
            };

            _projectTwoUnitOfWork.DragAndDrop.Add(newItemToAdd);

            _projectTwoUnitOfWork.Save();
        }
    }
}
