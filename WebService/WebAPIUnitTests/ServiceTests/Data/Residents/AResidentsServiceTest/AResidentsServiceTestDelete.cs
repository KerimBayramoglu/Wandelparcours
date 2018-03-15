﻿using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using WebAPIUnitTests.TestHelpers.Extensions;
using WebService.Models;

// ReSharper disable once CheckNamespace
namespace WebAPIUnitTests.ServiceTests.Data.Residents
{
    public abstract partial class AResidentsServiceTest
    {
        [TestMethod]
        public void RemoveKnownMediaWithUnknownId()
        {
            ActionExtensions.ShouldCatchNotFoundException(
                () => CreateNewDataService()
                    .RemoveMediaAsync(ObjectId.GenerateNewId(), ObjectId.GenerateNewId(), EMediaType.Color)
                    .Wait(),
                "there is no resident with that id");
        }

        [TestMethod]
        public void RemoveKnownMediaWithKnownId()
        {
            var datatService = CreateNewDataService();

            var original = datatService.GetFirst();

            datatService
                .RemoveMediaAsync(original.Id, original.Colors.First().Id, EMediaType.Color)
                .Wait();

            datatService
                .GetAll()
                .Should()
                .NotContain(x => x.Colors != null && x.Colors.Any(c => c.Id == original.Id),
                    "that color has just been removed");
        }

        [TestMethod]
        public void RemoveUnknownMediaWithKnownId()
        {
            var dataService = CreateNewDataService();

            var original = dataService.GetFirst();

            ActionExtensions.ShouldCatchNotFoundException(
                () => dataService.RemoveMediaAsync(original.Id, ObjectId.GenerateNewId(), EMediaType.Color)
                    .Wait(),
                "there is no media with the given id");
        }
    }
}