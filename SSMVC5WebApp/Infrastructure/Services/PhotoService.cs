using System;
using System.Web;
using System.Diagnostics;
using System.Threading.Tasks;

using System.IO;
using Microsoft.WindowsAzure.Storage;
using SSMVC5WebApp.Infrastructure.Abstract;
using SSMVC5WebApp.Infrastructure.Concrete;
using Microsoft.WindowsAzure.Storage.Blob;

namespace SSMVC5WebApp.Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        ILogger _log;
        CloudStorageAccount _storageAccount;

        public PhotoService(ILogger logger)
        {
            _log = logger;
            _storageAccount = StorageUtility.StorageAccount;
        }


        #region IPhotoService Member
        public async Task<string> UploadPhotoAsync(string category, HttpPostedFileBase photoToUpload)
        {
            if (photoToUpload == null || photoToUpload.ContentLength == 0)
            {
                return null;
            }
            string fullPath = null;
            Stopwatch timespan = Stopwatch.StartNew();

            try
            {
                //Create a blob client and retrive reference for the category container
                CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();

                #region BlobClient set for Threshold, ParallelOperationThread, RetryPolicy
                //blobClient.SingleBlobUploadThresholdInBytes = 1024 * 1024; //1 MB Minimum Deprecated
                //BlobRequestOptions bro = new BlobRequestOptions
                //{
                //    SingleBlobUploadThresholdInBytes = 1024 * 1024, //1 MB Minimum
                //    ParallelOperationThreadCount = 1, //1 thread for the work
                //    RetryPolicy = new Microsoft.WindowsAzure.Storage.RetryPolicies.ExponentialRetry(TimeSpan.FromSeconds(2), 1)
                //};
                //blobClient.DefaultRequestOptions = bro;

                #endregion

                CloudBlobContainer blobContainer = blobClient.GetContainerReference(category.ToLower());

                if (await blobContainer.CreateIfNotExistsAsync())
                {
                    await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                    _log.Information(string.Format("Succeffully created Blob Storage '{0}' Container and made it public", blobContainer.Name));
                }

                //create a unique name for the image to be uploaded
                string imageName = string.Format("product-photo-{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(photoToUpload.FileName));

                //upload image to blob storage
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(imageName);
                blockBlob.Properties.ContentType = photoToUpload.ContentType;
                await blockBlob.UploadFromStreamAsync(photoToUpload.InputStream);

                fullPath = blockBlob.Uri.ToString();
                timespan.Stop();
                _log.TraceApi("Blob Service", "PhotoService.UploadPhoto", timespan.Elapsed, "imagepath={0}", fullPath);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error Uploading the photo blob to storage");
                throw;
            }
            return fullPath;
        }

        public async Task<bool> DeletePhotoAsync(string category, string photoUrl)
        {
            if (string.IsNullOrEmpty(photoUrl))
            {
                return true;
            }
            Stopwatch timespan = Stopwatch.StartNew();
            bool deleteFlag = false;
            try
            {
                CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(category.ToLower());

                if (blobContainer.Name == category.ToLower())
                {
                    string blobName = photoUrl.Substring(photoUrl.LastIndexOf("/") + 1);
                    CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);
                    deleteFlag = await blockBlob.DeleteIfExistsAsync();

                    #region Check the container for the list of blobs
                    //foreach (var item in blobContainer.ListBlobs())
                    //{
                    //    _log.Information(string.Format("Url: {0}, Container: {1}, Parent: {2}, Name: {3}", item.Uri, item.Container, item.Parent, item.Uri.ToString().Substring(item.Uri.ToString().LastIndexOf("/") + 1)));
                    //}
                    //deleteFlag = await blobContainer.GetBlockBlobReference(photoUrl).DeleteIfExistsAsync();
                    #endregion
                }
                timespan.Stop();
                _log.TraceApi("Blob Service", "PhotoService.DeletePhoto", timespan.Elapsed, "deletedimagepath={0}", photoUrl);
                return deleteFlag;
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error in Deleting the photo blob from storage");
                throw;
            }
        }

        #endregion
    }
}