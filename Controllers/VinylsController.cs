using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinylCollection.Controllers;
using VinylCollection.Entities;
using Microsoft.AspNetCore.Mvc;
using VinylCollection.Persistence;
using Microsoft.EntityFrameworkCore;

namespace VinylCollection.Controllers
{
    public class VinylsController : BaseApiController
    {
        private readonly DataContext _context;
        public VinylsController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Vinyl>>> getVinyls()
        {
            return await _context.Vinyls.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vinyl>> GetVinyl(Guid id)
        {
            return await _context.Vinyls.FindAsync(id);
        }

    }
}