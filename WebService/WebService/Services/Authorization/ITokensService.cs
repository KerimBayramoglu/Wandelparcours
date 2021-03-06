﻿using System.Threading.Tasks;
using MongoDB.Bson;

namespace WebService.Services.Authorization
{
    public interface ITokensService
    {
        Task<string> CreateTokenAsync(ObjectId id, string password);
        Task<ObjectId> GetIdFromToken(string token);

        bool ValidateToken(string strToken);
    }
}