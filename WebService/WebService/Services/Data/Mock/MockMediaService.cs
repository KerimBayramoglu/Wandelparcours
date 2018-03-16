﻿using MongoDB.Bson;
using WebService.Models;
using WebService.Services.Exceptions;

namespace WebService.Services.Data.Mock
{
    public partial class MockMediaService : AMockDataService<MediaData>, IMediaService
    {
        public MockMediaService(IThrow iThrow) : base(iThrow)
        {
        }

        public override MediaData CreateNewItem(ObjectId id)
            => new MediaData {Id = id};

       
    }
}