﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WebService.Helpers.Exceptions;

namespace WebService.Models
{
    public class MultiPartFile
    {
        public IFormFile File { get; set; }

        public byte[] ConvertToBytes(int maxFileSize = int.MaxValue)
        {
            using (var stream = File.OpenReadStream())
            {
                if (!stream.CanRead)
                    throw new Exception("cannot read stream");

                if (stream.Length > maxFileSize)
                    throw new FileToLargeException(maxFileSize);

                var bytes = new List<byte>((int) stream.Length);
                for (var i = 0; i < stream.Length; i++)
                    bytes.Add((byte) stream.ReadByte());

                return bytes.ToArray();
            }
        }
    }
}