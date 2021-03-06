﻿using Evol.Common;
using Evol.EntityFramework.Repository;
using Evol.TMovie.Domain.Models.AggregateRoots;
using Evol.TMovie.Domain.QueryEntries;
using Evol.TMovie.Domain.QueryEntries.Parameters;
using Evol.TMovie.Domain.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evol.Common.Data;

namespace Evol.TMovie.Data.QueryEntries
{
    public class CinemaQueryEntry : BaseEntityFrameworkQuery<Cinema, TMovieDbContext>, ICinemaQueryEntry
    {
        public IScheduleQueryEntry ScheduleQuery { get; private set; }

        public IMovieQueryEntry MovieQuery { get; private set; }

        public CinemaQueryEntry( IEfDbContextProvider efDbContextProvider, IScheduleQueryEntry scheduleQuery, IMovieQueryEntry movieQuery) 
            : base(efDbContextProvider)
        {
            ScheduleQuery = scheduleQuery;
            MovieQuery = movieQuery;
        }

        public async Task<List<Cinema>> SelectAsync(CinemaQueryParameter param)
        {
            if (param == null || string.IsNullOrWhiteSpace(param.Name))
                return new List<Cinema>();
            var items = await base.SelectAsync(e => e.Name.Contains(param.Name));
            return items.ToList();
        }


        public async Task<IPaged<Cinema>> PagedAsync(CinemaQueryParameter param, int pageIndex, int pageSize)
        {
            if (param == null || string.IsNullOrWhiteSpace(param.Name))
                return await base.PagedAsync(pageIndex, pageSize);
            return await base.PagedAsync(e => e.Name.Contains(param.Name), pageIndex, pageSize);
        }

        public async Task<List<Movie>> SelectShowingMoiveAsync(Guid cinemaId)
        {
            var screens = await ScheduleQuery.SelectAsync(new ScheduleQueryParameter { CinemaId = cinemaId });
            var movieIds = screens.Select(e => e.MovieId).ToArray();
            var movies = await MovieQuery.SelectAsync(movieIds);
            return movies;
        }
    }
}
