using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUnitOfWork
    {
        IGenericRepository<Game> Games { get; }
        IGenericRepository<Company> Companies { get; }
        IGenericRepository<Genre> Genres { get; }
        Task SaveAsync(); 
    }
}
