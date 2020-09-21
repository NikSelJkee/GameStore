using Core.Entities;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;

        private GenericRepository<Game> _games;
        private GenericRepository<Company> _companies;
        private GenericRepository<Genre> _genres;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public IGenericRepository<Game> Games
        {
            get
            {
                return _games ?? (_games = new GenericRepository<Game>(_context));
            }
        }

        public IGenericRepository<Company> Companies
        {
            get
            {
                return _companies ?? (_companies = new GenericRepository<Company>(_context));
            }
        }

        public IGenericRepository<Genre> Genres
        {
            get
            {
                return _genres ?? (_genres = new GenericRepository<Genre>(_context));
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
