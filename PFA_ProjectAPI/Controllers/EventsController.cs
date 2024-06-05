using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PFA_ProjectAPI.Controllers.CustomActionFilter;
using PFA_ProjectAPI.Domain.Models.Domain;
using PFA_ProjectAPI.Domain.Models.DtoEvent;
using PFA_ProjectAPI.Domain.Models.DtoImage;
using PFA_ProjectAPI.Infrastructure.Repositories;

namespace PFA_ProjectAPI.Controllers
{
    // /api/event
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository eventRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public EventsController(IMapper mapper, IEventRepository eventRepository , IImageRepository imageRepository)
        {
          
            this.mapper = mapper;
            this.eventRepository = eventRepository;
            this.imageRepository = imageRepository;
        }

        //POST To create New Event
        //POST: https://localhost:portnumber/api/events
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromForm] AddEventRequestDto addEventRequestDto, [FromForm] ImageUploadRequestDto request) 
        {

            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var eventDomainModel = mapper.Map<Event>(addEventRequestDto);

                var eventEntity = await eventRepository.CreateAsync(eventDomainModel);

                //convert dto to domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileName = request.FileName,
                    Event = eventEntity
                };
           
            await imageRepository.Upload(imageDomainModel);
          
            //Map Domain model to Dto
            return Ok(mapper.Map<EventDto>(eventDomainModel));

            }
            return BadRequest();
        }

         private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtension.Contains(Path.GetExtension(request.File.FileName))) {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
        }

        //GET Events
        [HttpGet("events/user")]
        public async Task<IActionResult> GetAll(Guid userId)
        {
           var eventsDomainModel= await eventRepository.GetAllAsync(userId);
            //Map Domain Model to Dto 
            return Ok(mapper.Map<List<EventDto>>(eventsDomainModel));

        }

       

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var eventsDomainModel = await eventRepository.GetAllAsync();
            //Map Domain Model to Dto 
            return Ok(mapper.Map<List<EventDto>>(eventsDomainModel));

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
