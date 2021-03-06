﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using WebService.Helpers.Extensions;
using WebService.Models;
using ArgumentNullException = System.ArgumentNullException;

namespace WebService.Services.Data.Mongo
{
    public class UsersService : AMongoDataService<User>, IUsersService
    {
        public UsersService(IConfiguration config)
            : base(
                config["Database:ConnectionString"],
                config["Database:DatabaseName"],
                config["Database:UsersCollectionName"])
        {
        }


        public override async Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.Id = ObjectId.GenerateNewId();
            switch (user.UserType)
            {
                case EUserType.SysAdmin:
                    user.Password = user.Password.Hash(user.Id);
                    break;
                case EUserType.Nurse:
                case EUserType.User:
                case EUserType.Module:
                case EUserType.Guest:
                    user.Password = user.Password.Hash(user.Id, usePepper: false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            await CreateAsync(user, false);
        }

        public async Task<bool> CheckCredentialsAsync(ObjectId id, string password)
        {
            var userPassword = await GetPropertyAsync(id, x => x.Password);

            return password.EqualsToHash(id, userPassword, usePepper: false) || password.EqualsToHash(id, userPassword);
        }

        public override async Task<IEnumerable<User>> GetAsync(
            IEnumerable<Expression<Func<User, object>>> propertiesToInclude = null)
        {
            if (propertiesToInclude != null)
                return await base.GetAsync(propertiesToInclude);

            var excludor = Builders<User>.Projection.Exclude(x => x.Password);
            return await MongoCollection
                .Find(FilterDefinition<User>.Empty)
                .Project<User>(excludor)
                .ToListAsync();
        }

        public async Task UpdatePasswordAsync(ObjectId id, string password)
            => await UpdatePropertyAsync(id, x => x.Password, password.Hash(id));

        public async Task<User> GetByNameAsync(string userName,
            IEnumerable<Expression<Func<User, object>>> propertiesToInclude = null)
            => await GetByAsync(x => x.UserName == userName, propertiesToInclude);

        public async Task<T> GetPropertyByNameAsync<T>(string userName,
            Expression<Func<User, T>> propertyToSelect = null)
            => await GetPropertyByAsync(x => x.UserName == userName, propertyToSelect);
    }
}