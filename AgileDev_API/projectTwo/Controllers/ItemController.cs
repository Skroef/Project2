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
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IAuthService _authService;

        public ItemController(IItemService itemService, IAuthService authService)
        {
            _itemService = itemService;
            _authService = authService;
        }

        [HttpGet("getItemList")]
        public ActionResult<string> getItemList()
        {
            return new JsonResult(_itemService.getItem());
        }
    }
}
