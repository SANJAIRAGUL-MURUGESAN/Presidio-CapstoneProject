﻿namespace BloggingApp.Interfaces
{
    public interface IAzureBlobService
    {
        public Task<string> UploadAsync(Stream fileStream, string fileName);
    }
}