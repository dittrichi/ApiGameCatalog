using ApiGameCatalog.Exceptions;
using ApiGameCatalog.InputModel.Publisher;
using ApiGameCatalog.Services;
using ApiGameCatalog.ViewModel.Publisher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]        
        public async Task<ActionResult<List<PublisherViewModel>>> Retrieve()
        {
            var publishers = await _publisherService.Retrieve();
            if (publishers.Count() == 0)
                return NoContent();
            return Ok(publishers);
        }

        [HttpGet("{idPublisher:guid}")]        
        public async Task<ActionResult<PublisherViewModel>> Retrieve(Guid idPublisher)
        {
            var publisher = await _publisherService.Retrieve(idPublisher);
            if (publisher == null)
                return NoContent();
            return Ok(publisher);
        }

        [HttpGet]
        [Route("retrievebyname")]
        public async Task<ActionResult<PublisherViewModel>> Retrieve(string name)
        {
            var publisher = await _publisherService.Retrieve(name);
            if (publisher == null)
                return NoContent();
            return Ok(publisher);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create([FromBody] PublisherInputModel publisherInputModel)
        {
            try
            {
                await _publisherService.Create(publisherInputModel);
                return Ok();
            }
            catch(PublisherAlreadyExistsException ex)
            { 
                return UnprocessableEntity("Publisher already exists");
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Update(Guid idPublisher, string name)
        {
            try
            {
                await _publisherService.Update(idPublisher, name);
                return Ok();
            }
            catch(PublisherNotFoundException ex)
            {
                return NotFound("Publisher not exists");
            }            
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(Guid idPublisher)
        {
            try
            {
                await _publisherService.Remove(idPublisher);
                return Ok();
            }
            catch (PublisherNotFoundException ex)
            {
                return NotFound("Publisher not exists");
            }
        }
    }
}
