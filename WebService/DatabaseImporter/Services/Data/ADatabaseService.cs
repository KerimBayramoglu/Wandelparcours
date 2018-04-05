﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DatabaseImporter.Models.MongoModels.Bases;

namespace DatabaseImporter.Services.Data
{
    public abstract class ADatabaseService : IDatabaseService
    {
        public async Task<IEnumerable> GetAsync<T>(
            IEnumerable<Expression<Func<T, object>>> selectors,
            params string[] locationParameters)
            where T : IModelWithObjectID
            => locationParameters != null && locationParameters.Length >= 3
                ? await GetAsync(selectors, locationParameters[0], locationParameters[2], locationParameters[3])
                : null;

        public async Task AddAsync<T>(
            IEnumerable<T> items,
            params string[] locationParameters)
            where T : IModelWithObjectID
        {
            if (locationParameters == null || locationParameters.Length < 3)
                return;
            await AddAsync(items, locationParameters[0], locationParameters[2], locationParameters[3]);
        }


        public abstract Task<IEnumerable> GetAsync<T>(
            IEnumerable<Expression<Func<T, object>>> selectors,
            string connectionString, string database, string collection)
            where T : IModelWithObjectID;

        public abstract Task AddAsync<T>(
            IEnumerable<T> items,
            string connectionString, string database, string collection)
            where T : IModelWithObjectID;
    }
}