using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinylCollection.Entities;
using Microsoft.AspNetCore.Mvc;

using VinylCollection.Mediator;
using VinylCollection.Mediator.Vinyls;

namespace VinylCollection.Controllers
{
    public class VinylsController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<Vinyl>>> getVinyls()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vinyl>> GetVinyl(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]

        public async Task<IActionResult> CreateVinyl(Vinyl vinyl)
        {
            return Ok(await Mediator.Send(new Create.Command { Vinyl = vinyl }));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditVinyl(Guid id, Vinyl vinyl)
        {
            vinyl.Id = id;
            return Ok(await Mediator.Send(new Edit.Command
            {
                vinyl = vinyl
            }));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVinyl(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command
            {
                id = id
            }));
        }
    }
}