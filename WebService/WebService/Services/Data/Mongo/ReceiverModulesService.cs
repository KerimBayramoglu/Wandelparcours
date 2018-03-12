﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WebService.Helpers.Exceptions;
using WebService.Models;

namespace WebService.Services.Data.Mongo
{
    /// <inheritdoc cref="AMongoDataService{T}"/>
    /// <summary>
    /// ReceiverModulesService is a class that extends from the <see cref="AMongoDataService{T}"/> class
    /// and by doing that implements the <see cref="IDataService{T}"/> interface.
    /// <para/>
    /// It handles the saving and retrieving residents to and from the mongo database.
    /// <para/>
    /// The connection string, db name and collections that are used are stored in the IConfiguration dependency under the Database object.
    /// </summary>
    public class ReceiverModulesService : AMongoDataService<ReceiverModule>, IReceiverModulesService
    {
        /// <summary>
        /// ReceiverModulesService is the constructor to create an instance of the <see cref="ReceiverModulesService"/> class.
        /// <para/>
        /// The connection string, db name and collections that are used are stored in the IConfiguration dependency under the Database object.
        /// </summary>
        /// <param name="config"></param>
        public ReceiverModulesService(IConfiguration config)
        {
            // create a new client and get the database from it
            var db = new MongoClient(config["Database:ConnectionString"]).GetDatabase(config["Database:DatabaseName"]);

            // get the residents mongo collection
            MongoCollection = db.GetCollection<ReceiverModule>(config["Database:ReceiverModulesCollectionName"]);
        }


        /// <inheritdoc cref="AMongoDataService{T}.MongoCollection" />
        /// <summary>
        /// MongoCollection is the mongo collection to query residents.
        /// </summary>
        public override IMongoCollection<ReceiverModule> MongoCollection { get; }


        /// <inheritdoc cref="IReceiverModulesService.GetAsync(string,IEnumerable{Expression{Func{ReceiverModule,object}}})" />
        /// <summary>
        /// GetAsync should return the receiver module with the given mac.
        /// </summary>
        /// <param name="mac">is the mac address of the receiver module to fetch</param>
        /// <param name="propertiesToInclude">are the properties that should be included in the objects</param>
        /// <returns>The receiver module with the given mac</returns>
        public async Task<ReceiverModule> GetAsync(string mac,
            IEnumerable<Expression<Func<ReceiverModule, object>>> propertiesToInclude = null)
        {
            if (mac == null)
                throw new ArgumentNullException(nameof(mac), "the mac address cannot be null");

            // get the item with the given id
            var result = MongoCollection.Find(x => x.Mac == mac);

            if (result.Count() <= 0)
                throw new NotFoundException($"the {typeof(ReceiverModule).Name} with mac address {mac} was not found");

            // convert the properties to include to a list (if not null)
            var properties = propertiesToInclude?.ToList();
            // if the properties are null or there are none, return all the properties
            if (properties == null)
                return await result.FirstOrDefaultAsync();

            // create a property filter
            var selector = Builders<ReceiverModule>.Projection.Include(x => x.Mac);

            //ReSharper disable once PossibleNullReferenceException
            // iterate over all the properties and add them to the filter
            foreach (var property in properties)
                selector = selector.Include(property);

            // return the item
            return await result
                // filter the properties
                .Project<ReceiverModule>(selector)
                // execute the query
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc cref="IReceiverModulesService.RemoveAsync(string)" />
        /// <summary>
        /// Remove removes the <see cref="ReceiverModule"/> with the given mac from the database.
        /// </summary>
        /// <param name="mac">is the mac of the <see cref="ReceiverModule"/> to remove in the database</param>
        /// <returns>
        /// - true if the <see cref="ReceiverModule"/> was removed from the database
        /// - false if the item was not removed
        /// </returns>
        public async Task<bool> RemoveAsync(string mac)
        {
            if (mac == null)
                throw new ArgumentNullException(nameof(mac), "the mac address cannot be null");

            // remove the receiver module with the given mac from the database
            var result = await MongoCollection.DeleteOneAsync(x => x.Mac == mac);

            if (!result.IsAcknowledged)
                throw new MongoException(
                    $"the {typeof(ReceiverModule).Name} with mac address {mac} could not be removed from the db");

            if (result.DeletedCount <= 0)
                throw new NotFoundException($"the {typeof(ReceiverModule).Name} with mac address {mac} was not found");

            // return true if something actually happened
            return true;
        }
    }
}