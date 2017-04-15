using System;
using System.Collections.Generic;
using System.Linq;
using Cinephile.Core.Domain;
using NUnit.Framework;

namespace Cinephile.UnitTests.Domain
{
    [TestFixture]
    public class MovieServiceTest
    {
        IMovieService sut;

        [SetUp]
        public void Setup()
        {
            sut = new MovieService();
        }

        [Test]
        public void GetUpcomingMovies_NoParam_10Movies()
        {
            IEnumerable<Movie> actual = null;

            sut
                .GetUpcomingMovies()
                .Subscribe(movies => actual = movies);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count(), Is.EqualTo(10));
            Assert.That(actual.Select(m => m.Title), Has.All.Not.Empty);
        }
    }
}
