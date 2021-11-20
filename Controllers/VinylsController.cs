

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VinylCollection.Dtos;
using VinylCollection.Entities;
using VinylCollection.Repositories;

namespace VinylCollection.Controllers
{
    [ApiController]
    [Route("vinyls")]
    public class VinylsController : ControllerBase
    {
        private readonly IMemVinylsRepository _repository;

        public VinylsController(IMemVinylsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<VinylDto> GetVinyls()
        {
            var vinyls = _repository.GetVinyls().Select(vinyl => vinyl.AsDto());
            return vinyls;
        }

        [HttpGet("{id}")]
        public ActionResult<VinylDto> GetVinyl(Guid id)
        {
            var vinyl = _repository.GetVinyl(id);
            if (vinyl is null)
            {
                return NotFound();
            }
            return Ok(vinyl.AsDto());
        }
        [HttpPost]
        public ActionResult<VinylDto> CreateVinyl(CreateVinylDto vinylDto)
        {
            Vinyl vinyl = new()
            {
                Id = Guid.NewGuid(),
                Title = vinylDto.Title,
                Artist = vinylDto.Artist
            };
            _repository.CreateVinyl(vinyl);
            return CreatedAtAction(nameof(GetVinyl), new { id = vinyl.Id }, vinyl.AsDto());
        }
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateVinylDto vinylDto)
        {
            var existingVinyl = _repository.GetVinyl(id);

            if (existingVinyl is null)
            {
                return NotFound();
            }

            Vinyl updatedVinyl = existingVinyl with
            {
                Title = vinylDto.Title,
                Artist = vinylDto.Artist
            };
            _repository.UpdateVinyl(updatedVinyl);

            return NoContent();
        }
        [HttpDelete("{id}")]

        public ActionResult DeleteItem(Guid id)
        {
            var existingVinyl = _repository.GetVinyl(id);

            if (existingVinyl is null)
            {
                return NotFound();
            }
            _repository.DeleteVinyl(id);
            return NoContent();

        }

    }
}