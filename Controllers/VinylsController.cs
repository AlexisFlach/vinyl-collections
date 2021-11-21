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
        public async Task<IActionResult> getVinyls()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVinyl(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
          
        }

        [HttpPost]
        public async Task<IActionResult> CreateVinyl(Vinyl vinyl)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Vinyl = vinyl }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditVinyl(Guid id, Vinyl vinyl)
        {
            vinyl.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command
            {
                Vinyl = vinyl
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVinyl(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command
            {
                id = id
            }));
        }
    }
}