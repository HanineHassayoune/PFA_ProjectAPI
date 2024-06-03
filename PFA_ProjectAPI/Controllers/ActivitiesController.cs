using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA_ProjectAPI.Controllers.CustomActionFilter;
using PFA_ProjectAPI.Domain.Models.Domain;
using PFA_ProjectAPI.Domain.Models.DtoActivity;
using PFA_ProjectAPI.Domain.Models.DtoImage;
using PFA_ProjectAPI.Infrastructure.Data;
using PFA_ProjectAPI.Infrastructure.Repositories;

//using System.Diagnostics;

namespace PFA_ProjectAPI.Controllers
{
    //Get All activities
    //Get : https://localhost:portnumber/api/activities
    [Route("api/[controller]")]
    [ApiController]

    public class ActivitiesController : ControllerBase
    {
        private readonly TBDbContext dbContext;
        private readonly IActivityRepository activityRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;
        private readonly IEventRepository eventRepository;
        public ActivitiesController(TBDbContext dbContext, IActivityRepository activityRepository, IMapper mapper, IImageRepository imageRepository , IEventRepository eventRepository)
        {
            this.dbContext = dbContext;
            this.activityRepository = activityRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
            this.eventRepository = eventRepository;
        }

        //GET all activities
        //GET: https://localhost:portnumber/api/regions
        [HttpGet]
        // [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAllActivities()
        {
            //Get Data From Database - Domain models

            var activitiesDomain = await activityRepository.GetAllAsync();
            //Map Domain Models to DTOs
            var activitiesDto = mapper.Map<List<ActivityDTO>>(activitiesDomain);

            return Ok(activitiesDto);
        }


        //Get Activity By ID
        //Get : https://localhost:portnumber/api/activities/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        // [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetActivityById(Guid id)
        {

            // Get Activity Domain Model from Database

            var activityDomain = await activityRepository.GetByIdAsync(id);
            if (activityDomain == null)
            {
                return NotFound();
            }
            //return DTO back to client
            return Ok(mapper.Map<ActivityDTO>(activityDomain));
        }



        //POST To create New Activity
        //POST: https://localhost:portnumber/api/activities
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddActivityRequestDto addActivityRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var activityDomainModel = mapper.Map<Activity>(addActivityRequestDto);
          
            // Use Domain Model to create Activity
            activityDomainModel = await activityRepository.CreateAsync(activityDomainModel);

            // Map Domain model back to DTO
          var activityDto = mapper.Map<ActivityDTO>(activityDomainModel);
         return CreatedAtAction(nameof(GetActivityById), new { id = activityDomainModel.Id }, activityDto);
            
        }

        //Update region
        //PUT: https://localhost:portnumber/api/activities/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateActivityRequestDto updateActivityRequestDto)
        {
            //Map DTO Domain Model
            var activityDomainModel = mapper.Map<Activity>(updateActivityRequestDto);

            //check if region exists
            //var activityDomainModel= await dbContext.Activities.FirstOrDefaultAsync(x=>x.Id == id);
            activityDomainModel = await activityRepository.UpdateAsync(id, activityDomainModel);

            if (activityDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain Model to DTO
            var activityDto = mapper.Map<ActivityDTO>(activityDomainModel);
            return Ok(activityDto);



        }


        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadImageRequestListDto request, Guid idActivity)
        {
            foreach (var item in request.Files)
            {
               ValidateFileUpload(item);
            }

            var activityEntity = await activityRepository.GetByIdAsync(idActivity);

            var eventEntity = await eventRepository.GetByIdAsync(activityEntity.EventId);
            if (ModelState.IsValid)
            {

                //convert dto to domain model
                foreach(var item in request.Files)
                {
                    var imageDomainModel = new Image
                    {
                        File = item,
                        FileExtension = Path.GetExtension(item.FileName),
                        FileName = item.FileName,
                        Activity= activityEntity,
                        Event= eventEntity




                    };
                    await imageRepository.Upload(imageDomainModel);
                } 
                

               
                return Ok();
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile request)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtension.Contains(Path.GetExtension(request.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
        }

        //Delete Activity
        //DELETE: https://localhost:portnumber/api/activities/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        // [Authorize(Roles = "Writer , Reader")]
        //kont ketbe [FromRoute] Guid id me7abich ya3mil l delete ki radithe Guid id
        public async Task<IActionResult> Delete(Guid id)
        {
            var activityDomainModel = await activityRepository.DeleteAsync(id);
            if (activityDomainModel == null)
            {
                return NotFound();
            }

            //return deleted Activity back
            //map Domain Model to DTO
            var activityDto = mapper.Map<ActivityDTO>(activityDomainModel);
            return Ok(activityDto);
        }



        // GET: api/Activity/event/{eventId}
        [HttpGet("event/{eventId:Guid}")]
        public async Task<IActionResult> GetByEventId([FromRoute] Guid eventId)
        {
            var activitiesDomainModel = await activityRepository.GetByEventIdAsync(eventId);

            if (activitiesDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var activitiesDto = mapper.Map<List<ActivityDTO>>(activitiesDomainModel); // Correction: Map to List<ActivityDTO>
            return Ok(activitiesDto);
        }






    }
}
