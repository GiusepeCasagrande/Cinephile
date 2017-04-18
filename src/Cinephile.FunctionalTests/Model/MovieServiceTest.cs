//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reactive.Linq;
//using System.Threading;
//using Cinephile.Core.Model;
//using Cinephile.Core.Rest;
//using Microsoft.Reactive.Testing;
//using NUnit.Framework;
//using System.Reactive.Concurrency;


//namespace Cinephile.FunctionalTests.Model
//{
//    [TestFixture]
//    public class MovieServiceTest
//    {
//        IMovieService sut;

//        [SetUp]
//        public void Setup()
//        {
//            sut = new MovieService(new ApiService(), new Cache());
//        }

//        [Test]
//        public void GetUpcomingMovies_NoParam_10Movies()
//        {
//            IEnumerable<Movie> actual = null;
//            var scheduler = new TestScheduler();

//            sut
//                .GetUpcomingMovies()
//                .ObserveOn(scheduler)
//                .SubscribeOn(scheduler)
//                .Select(movies => actual = movies)
//                .Subscribe();

//            scheduler.AdvanceBy(1);

//            Assert.That(actual, Is.Not.Null);
//            Assert.That(actual.Count(), Is.EqualTo(10));
//            Assert.That(actual.Select(m => m.Title), Has.All.Not.Empty);
//        }
//    }
//}
