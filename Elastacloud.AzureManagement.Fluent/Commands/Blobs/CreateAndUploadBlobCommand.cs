﻿/************************************************************************************************************
 * This software is distributed under a GNU Lesser License by Elastacloud Limited and it is free to         *
 * modify and distribute providing the terms of the license are followed. From the root of the source the   *
 * license can be found in /Resources/license.txt                                                           * 
 *                                                                                                          *
 * Web at: www.elastacloud.com                                                                              *
 * Email: info@elastacloud.com                                                                              *
 ************************************************************************************************************/

using System;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Elastacloud.AzureManagement.Fluent.Commands.Blobs
{
    /// <summary>
    /// Used to create an upload a blob to a specified container
    /// </summary>
    internal class CreateAndUploadBlobCommand : BlobCommand
    {
        internal CreateAndUploadBlobCommand(string containerName, string blobName, string fileNamePath)
        {
            ContainerName = containerName;
            BlobName = blobName;
            FileNamePath = fileNamePath;
            HttpVerb = HttpVerbPut;
        }

        internal string FileNamePath { get; set; }
        internal string DeploymentPath { get; set; }

        public override void Execute()
        {
            string accessContainer = DeploymentPath = String.Format("http://{0}.blob.core.windows.net/{1}/{2}", AccountName, ContainerName, BlobName);
            string baseUri = String.Format("http://{0}.blob.core.windows.net/", AccountName);

            var client = new CloudBlobClient(new Uri(baseUri), new StorageCredentials(AccountName, AccountKey));
            var container = client.GetContainerReference(ContainerName);
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(BlobName);
            blob.UploadFromStream(new FileStream(FileNamePath, FileMode.Open, FileAccess.Read));
            //int contentLength;
            //byte[] ms = GetPackageFileBytesAndLength(FileNamePath, out contentLength);

            //string canResource = String.Format("/{0}/{1}/{2}", AccountName, ContainerName, BlobName);

            //string authHeader = CreateAuthorizationHeader(canResource, "\nx-ms-blob-type:BlockBlob", contentLength);
            //SendWebRequest(accessContainer, authHeader, ms, contentLength);
        }
    }
}