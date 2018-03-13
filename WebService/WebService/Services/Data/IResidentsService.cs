﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using WebService.Helpers.Exceptions;
using WebService.Models;
using WebService.Models.Bases;

namespace WebService.Services.Data
{
    public interface IResidentsService : IDataService<Resident>
    {
        /// <summary>
        /// GetAsync is supposed to return the <see cref="Resident"/> with the given tag from the database. 
        /// <para/>
        /// It only fills the properties passed in the <see cref="propertiesToInclude"/> parameter. The id is always passed and 
        /// if the <see cref="propertiesToInclude"/> parameter is null (which it is by default), all the properties are included. 
        /// </summary>
        /// <param name="tag">is the tag of the <see cref="Resident"/> that needs to be fetched</param>
        /// <param name="propertiesToInclude">are the properties that should be included in the objects</param>
        /// <returns>The <see cref="Resident"/> with the given id</returns>
        /// <exception cref="NotFoundException">when there is no <see cref="Resident"/> that holds the given tag</exception>
        Task<Resident> GetAsync(int tag, IEnumerable<Expression<Func<Resident, object>>> propertiesToInclude = null);

        /// <summary>
        /// AddMediaAsync is supposed to add the <see cref="data"/> as media of the type <see cref="mediaType"/> to the <see cref="Resident"/>
        /// with as <see cref="Resident.Id"/> the passed <see cref="residentId"/>.
        /// </summary>
        /// <param name="residentId">is the id of the <see cref="Resident"/></param>
        /// <param name="data">is the data of the media to add</param>
        /// <param name="mediaType">is the type of media to add</param>
        /// <exception cref="ArgumentNullException">when the data is null</exception>
        /// <exception cref="NotFoundException">when there is no <see cref="Resident"/> found with the given <see cref="AModelWithID.Id"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">when the media type doesn't exist</exception>
        Task AddMediaAsync(ObjectId residentId, byte[] data, EMediaType mediaType);

        /// <summary>
        /// AddMediaAsync is supposed to add the <see cref="url"/> as media of the type <see cref="mediaType"/> to the <see cref="Resident"/>
        /// with as <see cref="Resident.Id"/> the passed <see cref="residentId"/>.
        /// </summary>
        /// <param name="residentId">is the id of the <see cref="Resident"/> add the media to</param>
        /// <param name="url">is the url to the media to add</param>
        /// <param name="mediaType">is the type of media to add</param>
        /// <exception cref="ArgumentNullException">when the url is null</exception>
        /// <exception cref="NotFoundException">when there is no <see cref="Resident"/> found with the given <see cref="AModelWithID.Id"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">when the media type doesn't exist</exception>
        Task AddMediaAsync(ObjectId residentId, string url, EMediaType mediaType);

        /// <summary>
        /// RemoveMediaAsync is supposed to remove the media of type <see cref="mediaType"/> with as id <see cref="mediaId"/> of the
        /// <see cref="Resident"/> with as id <see cref="residentId"/>.
        /// </summary>
        /// <param name="residentId">is the id of the <see cref="Resident"/> to remove the media from</param>
        /// <param name="mediaId">is the id of media to remove</param>
        /// <param name="mediaType">is the type of media to remove</param>
        /// <exception cref="NotFoundException">when there is no <see cref="Resident"/> found with the given <see cref="AModelWithID.Id"/></exception>
        /// <exception cref="NotFoundException">when there is no <see cref="MediaWithId"/> found with the given <see cref="AModelWithID.Id"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">when the media type doesn't exist</exception>
        Task RemoveMediaAsync(ObjectId residentId, ObjectId mediaId, EMediaType mediaType);
    }
}