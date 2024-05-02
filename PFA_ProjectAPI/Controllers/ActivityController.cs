using API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA_ProjectAPI.Models.ActivityModels;
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
    public  class ActivityController : ControllerBase
    {
        private readonly TBDbContext dbContext;
        private readonly IActivityRepository activityRepository;
        public ActivityController(TBDbContext dbContext , IActivityRepository activityRepository)
        {
            this.dbContext = dbContext;
            this.activityRepository = activityRepository;
        }
        [HttpGet]
        public async Task<IActionResult>  GetAllActivities()
        {
            //Get Data From Database - Domain models
            //var activitiesDomain = await dbContext.Activities.ToListAsync();
            var activitiesDomain = await activityRepository.GetAllAsync();

            // Map Domain models to DTOs
            var activitiesDto = new List<ActivityDTO>();
            foreach (var activityDomain in activitiesDomain)
            {
                activitiesDto.Add(new ActivityDTO()
                {
                    Id = activityDomain.Id,
                    Title = activityDomain.Title,
                    StartDate = activityDomain.StartDate,
                    EndDate = activityDomain.EndDate,
                    Animator = activityDomain.Animator,
                    Pictures = activityDomain.Pictures,
                    TeamBuilding = activityDomain.TeamBuilding,
                    Status = activityDomain.Status,
                    Description = activityDomain.Description,

                });
            }
            //return DTOs
            //return an ok response
            return Ok(activitiesDto);
        }


        //Get Activity By ID
        //Get : https://localhost:portnumber/api/activities/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetActivityById(Guid id)
        {

            //var activity= dbContext.Activities.Find(id);
            // Get Activity Domain Model from Database

            var activityDomain = await dbContext.Activities.FirstOrDefaultAsync(x => x.Id == id);
            if (activityDomain == null)
            {
                return NotFound();
            }
            //Map/Convert Activity Domain Model To Model DTO
            //
            var activityDTO = new ActivityDTO
            {
                Id = activityDomain.Id,
                Title = activityDomain.Title,
                StartDate = activityDomain.StartDate,
                EndDate = activityDomain.EndDate,
                Animator = activityDomain.Animator,
                Pictures = activityDomain.Pictures,
                TeamBuilding = activityDomain.TeamBuilding,
                Status = activityDomain.Status,
                Description = activityDomain.Description,
            };
            //return DTO back to client
            return Ok(activityDTO);
        }

        //POST To create New Activity
        //POST: https://localhost:portnumber/api/activities
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddActivityRequestDto addActivityRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var activityDomainModel = new Activity
            {

                Title = addActivityRequestDto.Title,
                StartDate = addActivityRequestDto.StartDate,
                EndDate = addActivityRequestDto.EndDate,
                Animator = addActivityRequestDto.Animator,
                Pictures = addActivityRequestDto.Pictures,
                TeamBuilding = addActivityRequestDto.TeamBuilding,
                Status = addActivityRequestDto.Status,
                Description = addActivityRequestDto.Description,

            };
            //Use Domain Model To create Region
            await dbContext.Activities.AddAsync(activityDomainModel);
            await dbContext.SaveChangesAsync();
            //Map Domain model back to DTO
            var activityDto = new ActivityDTO
            {
                Id = activityDomainModel.Id,
                Title = activityDomainModel.Title,
                StartDate = activityDomainModel.StartDate,
                EndDate = activityDomainModel.EndDate,
                Animator = activityDomainModel.Animator,
                Pictures = activityDomainModel.Pictures,
                TeamBuilding = activityDomainModel.TeamBuilding,
                Status = activityDomainModel.Status,
                Description = activityDomainModel.Description,

            };
            return CreatedAtAction(nameof(GetActivityById), new { id = activityDomainModel.Id }, activityDomainModel);

        }

        //Update region
        //PUT: https://localhost:portnumber/api/activities/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateActivityRequestDto updateActivityRequestDto)
        {
            //check if region exists
            var activityDomainModel= await dbContext.Activities.FirstOrDefaultAsync(x=>x.Id == id);
            if (activityDomainModel == null)
            {
                return NotFound();
            }
            //Map DTO to Domain model 
            activityDomainModel.Title = updateActivityRequestDto.Title;
            activityDomainModel.StartDate = updateActivityRequestDto.StartDate;
            activityDomainModel.EndDate = updateActivityRequestDto.EndDate; 
            activityDomainModel.Animator=updateActivityRequestDto.Animator;
            activityDomainModel.Pictures= updateActivityRequestDto.Pictures;
            activityDomainModel.TeamBuilding= updateActivityRequestDto.TeamBuilding;
            activityDomainModel.Status= updateActivityRequestDto.Status;

            dbContext.SaveChanges();

            //Convert Domain Model to DTO
            var activityDto = new ActivityDTO
            {
                Id = activityDomainModel.Id,
                Title = activityDomainModel.Title,
                StartDate = activityDomainModel.StartDate,
                EndDate = activityDomainModel.EndDate,
                Animator = activityDomainModel.Animator,
                Pictures = activityDomainModel.Pictures,
                TeamBuilding = activityDomainModel.TeamBuilding,
                Status = activityDomainModel.Status
            };
            return Ok(activityDto);




        }

        //Delete Activity
        //DELETE: https://localhost:portnumber/api/activities/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        //kont ketbe [FromRoute] Guid id me7abich ya3mil l delete ki radithe Guid id
        public async Task<IActionResult> Delete(Guid id)
        {
            var activityDomainModel= await dbContext.Activities.FirstOrDefaultAsync(x => x.Id == id);
            if (activityDomainModel == null)
            {
                return NotFound();
            }

            //Delete region
            dbContext.Activities.Remove(activityDomainModel);
            await dbContext.SaveChangesAsync();

            //return deleted Activity back
            //map Domain Model to DTO
            var activityDto = new ActivityDTO
            {
                Id = activityDomainModel.Id,
                Title = activityDomainModel.Title,
                StartDate = activityDomainModel.StartDate,
                EndDate = activityDomainModel.EndDate,
                Animator = activityDomainModel.Animator,
                Pictures = activityDomainModel.Pictures,
                TeamBuilding = activityDomainModel.TeamBuilding,
                Status = activityDomainModel.Status,
                Description=activityDomainModel.Description
            };
            return Ok(activityDto);
        }


    }



}
