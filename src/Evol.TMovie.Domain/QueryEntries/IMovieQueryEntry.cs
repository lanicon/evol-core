﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Evol.TMovie.Domain.Models.AggregateRoots;
using Evol.TMovie.Domain.QueryEntries.Parameters;
using Evol.Common;
using Evol.Domain.Data;
using Evol.Common.Data;

namespace Evol.TMovie.Domain.QueryEntries
{
    public interface IMovieQueryEntry : IQueryEntry
    {
        Task<Movie> FindAsync(Guid id);

        Task<List<Movie>> SelectAsync(Guid[] ids);

        Task<List<Movie>> SelectAsync(MovieQueryParameter param);

        Task<IPaged<Movie>> PagedAsync(int index, int size);

        Task<IPaged<Movie>> PagedAsync(MovieQueryParameter param,  int index, int size);
    }
}
