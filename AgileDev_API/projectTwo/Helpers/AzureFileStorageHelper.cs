using Microsoft.Extensions.Options;
using projectTwo.Interfaces;
using System;

using Microsoft.Extensions.Options;
using System.Drawing;
using System.IO;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace projectTwo.Helpers
{
	public class AzureFileStorageHelper : IFileStorageHelper
	{
		private IOptions<AppSettings> _appSettings;

		public AzureFileStorageHelper(IOptions<AppSettings> settings)
		{
			_appSettings = settings;
		}

		public byte[] DownloadFile(Uri filePath)
		{
			var blockblob = new CloudBlockBlob(filePath);
			blockblob.FetchAttributes();
			var file = new byte[blockblob.Properties.Length];
			blockblob.DownloadToByteArray(file, 0);
			return file;
		}

		public string GetFullPath(string fileName, string containerName)
		{
			var storageacc = CloudStorageAccount.Parse(_appSettings.Value.StorageConnectionString);
			var cloudBlobClient = storageacc.CreateCloudBlobClient();
			var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
			var cloudBlob = cloudBlobContainer.GetBlobReference(fileName);
			cloudBlob.FetchAttributes();
			return cloudBlob.Uri.ToString();
		}

		public string UploadFile(string fileName, string containerName, byte[] fileData)
		{
			var storageacc = CloudStorageAccount.Parse(_appSettings.Value.StorageConnectionString);
			var blobClient = storageacc.CreateCloudBlobClient();
			var container = blobClient.GetContainerReference(containerName);

			container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

			var blockBlob = container.GetBlockBlobReference(fileName);
			blockBlob.UploadFromByteArray(fileData, 0, fileData.Length);

			return blockBlob.Uri.ToString();
		}

		public void RemoveFile(string fileName, string containerName)
		{
			var storageacc = CloudStorageAccount.Parse(_appSettings.Value.StorageConnectionString);
			var blobClient = storageacc.CreateCloudBlobClient();
			var container = blobClient.GetContainerReference(containerName);
			var blockBlob = container.GetBlockBlobReference(fileName);
			blockBlob.DeleteIfExists();
		}

		private Image ConvertByteToImage(byte[] imageinBytes)
		{
			using (MemoryStream mStream = new MemoryStream(imageinBytes))
			{
				return Image.FromStream(mStream);
			};
		}

		public string ResizeImage(string imageUrl)
		{
			Bitmap original, resizedImage;

			try
			{
				var image = ConvertByteToImage(DownloadFile(new Uri(imageUrl)));

				original = new Bitmap(image);

				int rectHeight = 350;
				int rectWidth = 350;

				//if the image is squared set it's height and width to the smallest of the desired dimensions (our box). In the current example rectHeight<rectWidth
				if (original.Height == original.Width)
				{
					resizedImage = new Bitmap(original, rectHeight, rectHeight);
				}
				else
				{
					//calculate aspect ratio
					float aspect = original.Width / (float)original.Height;
					int newWidth, newHeight;

					//calculate new dimensions based on aspect ratio
					newWidth = (int)(rectWidth * aspect);
					newHeight = (int)(newWidth / aspect);

					//if one of the two dimensions exceed the box dimensions
					if (newWidth > rectWidth || newHeight > rectHeight)
					{
						//depending on which of the two exceeds the box dimensions set it as the box dimension and calculate the other one based on the aspect ratio
						if (newWidth > newHeight)
						{
							newWidth = rectWidth;
							newHeight = (int)(newWidth / aspect);
						}
						else
						{
							newHeight = rectHeight;
							newWidth = (int)(newHeight * aspect);
						}
					}
					resizedImage = new Bitmap(original, newWidth, newHeight);
				}

				var newImageBytes = ImageToByte(resizedImage);
				int startIndex = imageUrl.IndexOf("incidentReport/") + 12;
				var fileName = imageUrl.Substring(startIndex, (imageUrl.Length - startIndex));
				var newUrl = UploadToTempStorage(fileName, newImageBytes);

				return newUrl;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

		private byte[] ImageToByte(Image img)
		{
			using (var stream = new MemoryStream())
			{
				img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
				return stream.ToArray();
			}
		}

		private string UploadToTempStorage(string fileName, byte[] file)
		{
			return UploadFile(fileName, "incidentReport", file);
		}

		public string CheckExtension(string ext)
		{
			if (ext == "peg")
				ext = "jpg";

			return ext;
		}
	}
}
