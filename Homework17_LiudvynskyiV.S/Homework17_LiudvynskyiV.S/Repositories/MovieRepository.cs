using AutoMapper;
using Homework17_LiudvynskyiV.S.Data;
using Homework17_LiudvynskyiV.S.Models.Domain;
using Homework17_LiudvynskyiV.S.Models.ViewModels;
using Homework17_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homework17_LiudvynskyiV.S.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly CinemaNetworkDbContext _dbContext;
    private readonly IMapper _mapper;

    public MovieRepository(CinemaNetworkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<List<MovieViewModel>> GetAll()
    {
        var movies = await _dbContext.Movies.ToListAsync();
        return _mapper.Map<List<MovieViewModel>>(movies);
    }

    public async Task<MovieViewModel?> Get(Guid id)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
        return movie is null ? default : _mapper.Map<MovieViewModel>(movie);
    }

    public async Task<MovieViewModel?> Add(MovieViewModel movieViewModel)
    {
        if (movieViewModel is null) return default;
        var movie = _mapper.Map<Movie>(movieViewModel);
        movie.Id = new Guid();
        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();
        return movieViewModel;
    }

    public async Task<MovieViewModel?> Update(Guid id, MovieViewModel movieViewModel)
    {
        if (movieViewModel is null) return default;
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
        if (movie is null) return default;
        _mapper.Map<Movie>(movieViewModel);
        await _dbContext.SaveChangesAsync();
        return movieViewModel;
    }

    public async Task<MovieViewModel?> Delete(Guid id)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
        if (movie is null) return default;
        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<MovieViewModel>(movie);
    }
}