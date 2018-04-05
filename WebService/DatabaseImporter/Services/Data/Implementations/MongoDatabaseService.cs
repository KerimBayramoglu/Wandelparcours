﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DatabaseImporter.Services.Data.Implementations
{
    public class MongoDatabaseService : ADatabaseService, IMongoService
    {
        public override async Task<IEnumerable> GetAsync<T>(
            IEnumerable<Expression<Func<T, object>>> selectors,
            string connectionString, string database, string collection)
        {
            var foundItems = GetCollection<T>(connectionString, database, collection)
                .Find(FilterDefinition<T>.Empty);

            if (selectors == null)
                return await foundItems.ToListAsync();

            var selector = Builders<T>.Projection.Include(x => x.Id);
            selector = selectors.Aggregate(selector, (current, property) => current.Include(property));

            return foundItems
                .Project<T>(selector)
                .ToList();
        }

        public override async Task AddAsync<T>(IEnumerable<T> items, string connectionString, string database,
            string collection)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            await GetCollection<T>(connectionString, database, collection)
                .InsertManyAsync(items);
        }

        private static IMongoCollection<T> GetCollection<T>(string connectionString, string database, string collection)
            => new MongoClient(connectionString).GetDatabase(database).GetCollection<T>(collection);
    }
}