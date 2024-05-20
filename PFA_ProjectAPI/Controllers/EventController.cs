using API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFA_ProjectAPI.CustomActionFilter;
using PFA_ProjectAPI.Models.Domain;
using PFA_ProjectAPI.Models.DTO;
using PFA_ProjectAPI.Models.DtoEvent;
using PFA_ProjectAPI.Repositories;

namespace PFA_ProjectAPI.Controllers
{
    // /api/event
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository eventRepository;
        private readonly IMapper mapper;

        public EventController(IMapper mapper, IEventRepository eventRepository)
        {
          
            this.mapper = mapper;
            this.eventRepository = eventRepository;
        }

        //POST To create New Event
        //POST: https://localhost:portnumber/api/events
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddEventRequestDto addEventRequestDto) 
        {
            //Map or Convert DTO to Domain Model
            var eventDomainModel = mapper.Map<Event>(addEventRequestDto);

            await eventRepository.CreateAsync(eventDomainModel);
            //Map Domain model to Dto
            return Ok(mapper.Map<EventDto>(eventDomainModel));
          

        }

        //GET Events
        //GET: /api/Events?filterOn=Name&filterQueru=Track
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]String? filterOn, [FromQuery]String? filterQuery)
        {
           var eventsDomainMoel= await eventRepository.GetAllAsync();
            //Map Domain Model to Dto 
            return Ok(mapper.Map<List<EventDto>>(eventsDomainMoel));

        }

        //GET Event By Id
        //GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var eventDomainModel = await eventRepository.GetByIdAsync(id);
            if (eventDomainModel == null)
            {
                return NotFound();
            }
            //Map Domain Model to DTO
            return Ok(mapper.Map<EventDto>(eventDomainModel));
        }

        //Update Event By Id
        //PUT : /api/Events/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id , UpdateEventRequestDto updateEventRequestDto)
        {

            //Map DTO to Domain Model
            var eventDomailModel = mapper.Map<Event>(updateEventRequestDto);

            eventDomailModel= await eventRepository.UpdateAsync(id, eventDomailModel);

            if(eventDomailModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO
            return Ok(mapper.Map<EventDto>(eventDomailModel));


        }

        //Delete an event by id 
        //DELETE: /api/Events/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var deleteEventDomainModel = await eventRepository.DeleteAsync(id);
            if(deleteEventDomainModel == null)
            {
                return NotFound();
            }
            //Map  Domain Model to DTO
            return Ok(mapper.Map<EventDto>(deleteEventDomainModel));
        }
    }
}
