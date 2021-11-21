using System.Collections.Generic;
using projectTwo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using projectTwo.DTOs;
using Microsoft.AspNetCore.Authorization;
using System;


namespace projectTwo.Controllers
{       
    [ApiController]
    [Route("api/[controller]")]
    public class DragAndDropController : Controller
    {
        private readonly IDragAndDropService _dragAndDropService;
        private readonly IAuthService _authService;

        public DragAndDropController(IDragAndDropService dragAndDropService, IAuthService authService)
        {
            _dragAndDropService = dragAndDropService;
            _authService = authService;
        }

        [HttpGet]
        [Route("~/api/DragAndDrop/GetAllDragAndDropItems")]
        public ActionResult GetAllDragAndDropItems()
        {
            return new JsonResult(_dragAndDropService.getAllDragAndDropItems());
        }

        [HttpPost]
        [Route("~/api/DragAndDrop/GetAllDragAndDropItems")]
        public void AddItemToDragAndDrop(AddDragAndDropItemDTO addItem)
        {
            _dragAndDropService.AddItemToDragAndDrop(addItem);
        }
    }
}
