using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
    public interface IFileStorageHelper
	{
        string UploadFile(string fileName, string containerName, byte[] fileData);
        void RemoveFile(string fileName, string containerName);
        byte[] DownloadFile(Uri filePath);
        string CheckExtension(string ext);
        string ResizeImage(string imageUrl);
    }
}
