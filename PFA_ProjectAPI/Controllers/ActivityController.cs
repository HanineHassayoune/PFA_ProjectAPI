using API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA_ProjectAPI.CustomActionFilter;
using PFA_ProjectAPI.Models.Domain;
using PFA_ProjectAPI.Models.DTO;
using PFA_ProjectAPI.Models.Enums;
using PFA_ProjectAPI.Repositories;
using System.Net.NetworkInformation;

namespace PFA_ProjectAPI.Controllers
{
    //Get All activities
    //Get : https://localhost:portnumber/api/activities
    [Route("api/[controller]")]
    [ApiController]
   
    public class ActivityController : ControllerBase
    {
        private readonly TBDbContext dbContext;
        private readonly IActivityRepository activityRepository;
        private readonly IMapper mapper;

        public ActivityController(TBDbContext dbContext, IActivityRepository activityRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.activityRepository = activityRepository;
            this.mapper = mapper;
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
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddActivityRequestDto addActivityRequestDto)
        {

            //Map or Convert DTO to Domain Model
            var activityDomainModel = mapper.Map<Activity>(addActivityRequestDto);

            //Use Domain Model to create Region
            activityDomainModel = await activityRepository.CreateAsync(activityDomainModel);

            //Map Domain model back to DTO
            var activityDto = mapper.Map<ActivityDTO>(activityDomainModel);
            return CreatedAtAction(nameof(GetActivityById), new { id = activityDomainModel.Id }, activityDomainModel);



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
    }



}
