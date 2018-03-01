﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using WebService.Helpers;

namespace WebService.Models
{
    /// <summary>
    /// Resident is a representation of a resident in the home to use for detection and animation at the kiosk.
    /// </summary>
    public class Resident
    {
        /// <summary>
        /// ID is the id of the Resident
        /// <para/>
        /// When serialized to json it is converted to a string.
        /// <para/>
        /// In the database the value is stored under the field "id"
        /// </summary>
        [BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId ID { get; set; }

        /// <summary>
        /// FirstName is the first name of the Resident
        /// <para/>
        /// In the database the value is stored under the field "firstName"
        /// </summary>
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName is the last name of the Resident
        /// <para/>
        /// In the database the value is stored under the field "lastName"
        /// </summary>
        [BsonElement("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Picture is a picture of the Resident
        /// <para/>
        /// In the database the value is stored under the field "picture"
        /// </summary>
        [BsonElement("picture")]
        public byte[] Picture { get; set; }

        /// <summary>
        /// Room is the room of the Resident
        /// <para/>
        /// In the database the value is stored under the field "room"
        /// </summary>
        [BsonElement("room")]
        public string Room { get; set; }

        /// <summary>
        /// Birthday is the birthday of the Resident
        /// <para/>
        /// In the database the value is stored under the field "birthday"
        /// </summary>
        [BsonElement("birthday")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Doctor is the doctor that takes care of the Resident
        /// <para/>
        /// In the database the value is stored under the field "doctor"
        /// </summary>
        [BsonElement("doctor")]
        public Doctor Doctor { get; set; }

        /// <summary>
        /// Tags are the tags to track the Resident
        /// <para/>
        /// In the database the value is stored under the field "tags"
        /// </summary>
        [BsonElement("tags")]
        public IEnumerable<int> Tags { get; set; }

        /// <summary>
        /// Music is the musiccollection of the Resident
        /// <para/>
        /// In the database the value is stored under the field "music"
        /// </summary>
        [BsonElement("music")]
        public IEnumerable<byte[]> Music { get; set; }

        /// <summary>
        /// Videos is the videocollection of the Resident
        /// <para/>
        /// In the database the value is stored under the field "videos"
        /// </summary>
        [BsonElement("videos")]
        public IEnumerable<byte[]> Videos { get; set; }

        /// <summary>
        /// Images is the imagecollection of the Resident
        /// <para/>
        /// In the database the value is stored under the field "images"
        /// </summary>
        [BsonElement("images")]
        public IEnumerable<byte[]> Images { get; set; }

        /// <summary>
        /// Colors are the favorite colors of the Resident
        /// <para/>
        /// In the database the value is stored under the field "colors"
        /// </summary>
        [BsonElement("colors")]
        public IEnumerable<byte[]> Colors { get; set; }

        /// <summary>
        /// LastRecordedPosition is position where the resident has last been tracked
        /// <para/>
        /// In the database the value is stored under the field "lastRecordedPosition"
        /// </summary>
        [BsonElement("lastRecordedPosition")]
        public Point LastRecordedPosition { get; set; }

        /// <summary>
        /// Locations are the locations where the residents has been tracked.
        /// <para/>
        /// In the database the value is stored under the field "locations"
        /// </summary>
        [BsonElement("locations")]
        public IEnumerable<Point> Locations { get; set; }
    }
}