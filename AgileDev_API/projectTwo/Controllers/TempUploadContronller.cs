using System.Collections.Generic;
using projectTwo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using projectTwo.DTOs;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.IO;
using projectTwo.Models;

namespace projectTwo.Controllers
{       
    [ApiController]
    [Route("api/[controller]")]
    public class TempUploadController : Controller
    {
        private readonly IProjectTwoUnitOfWork _context;
        private readonly IAuthService _authInfo;
        private readonly IFileStorageHelper _fileStorageHelper;
        public TempUploadController(IProjectTwoUnitOfWork context, IAuthService authInfo, IFileStorageHelper fileStorageHelper)
        {
            _fileStorageHelper = fileStorageHelper;
        }


        [HttpDelete]
        [Route("~/api/TempUpload")]
        public ActionResult Delete()
        {
            return new AcceptedResult();
        }

        // POST api/background
        [HttpPost]
        public ActionResult<string> Post()
        {
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (file != null)
            {
                var fileName = file.FileName;
                byte[] buffer = new byte[file.Length];
                using (var stream = file.OpenReadStream())
                {
                    stream.Read(buffer);
                }

                //Upload file to temporary location and return File name
               ;

                var uploadedFileName = _fileStorageHelper.UploadFile(file.FileName, "imagecontainer", buffer);

                return new JsonResult(new TempUploadResultDTO
                {
                    Error = string.Empty,
                    Success = true,
                    Url = uploadedFileName
                });

            }
            return new JsonResult(new TempUploadResultDTO
            {
                Error = "An error has occurred",
                Success = false,
                Url = ""
            });

        }
    }
}
