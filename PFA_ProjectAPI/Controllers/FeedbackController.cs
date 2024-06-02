using API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PFA_ProjectAPI.Models.Domain;
using PFA_ProjectAPI.Models.DtoFeadback;
using PFA_ProjectAPI.Repositories;
using System.Security.Claims;

namespace PFA_ProjectAPI.Controllers
{
    //Get All feedbacks
    //Get : https://localhost:portnumber/api/feedbacks
    [Route("api/[controller]")]
    [ApiController]
    
    public class FeedbacksController : ControllerBase
    {
        private readonly TBDbContext dbContext;
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IMapper mapper;
        public FeedbacksController(TBDbContext dbContext, IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.feedbackRepository = feedbackRepository;
            this.mapper = mapper;

        }



        //GET all feedbacks
        //GET: https://localhost:portnumber/api/feedbacks
        [HttpGet]
        // [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            //Get Data From Database - Domain models

            var feedbacksDomain = await feedbackRepository.GetAllAsync();
            //Map Domain Models to DTOs
            var feedbacksDto = mapper.Map<List<FeedbackDto>>(feedbacksDomain);

            return Ok(feedbacksDto);
        }


        //Get feedback By ID
        //Get : https://localhost:portnumber/api/feedbacks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        // [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetFeedbackById(Guid id)
        {

            // Get Feedback Domain Model from Database

            var feedbackDomain = await feedbackRepository.GetByIdAsync(id);
            if (feedbackDomain == null)
            {
                return NotFound();
            }
            //return DTO back to client
            return Ok(mapper.Map<FeedbackDto>(feedbackDomain));
        }


        //POST To create New Feeback
        //POST: https://localhost:portnumber/api/feedbacks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddFeedbackRequestDto addFeedbackRequestDto)
        {
            // Validate the request model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Récupérez l'ID de l'utilisateur connecté
           // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Utilisez User.FindFirstValue pour obtenir l'ID de l'utilisateur connecté

            // Map or Convert DTO to Domain Model
            var feedbackDomainModel = mapper.Map<Feedback>(addFeedbackRequestDto);

            // Associez l'ID de l'utilisateur au feedback
            //feedbackDomainModel.UserId = Guid.Parse(userId);

            // Use Domain Model to create feedback
            feedbackDomainModel = await feedbackRepository.CreateAsync(feedbackDomainModel);

            // Map Domain model back to DTO
            var feedbackDto = mapper.Map<FeedbackDto>(feedbackDomainModel);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = feedbackDto.Id }, feedbackDto);
        }



        //Delete Feedback
        //DELETE: https://localhost:portnumber/api/feedbacks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        // [Authorize(Roles = "Writer , Reader")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var feedbackDomainModel = await feedbackRepository.DeleteAsync(id);
            if (feedbackDomainModel == null)
            {
                return NotFound();
            }

            //return deleted feedback back
            //map Domain Model to DTO
            var feedbackDto = mapper.Map<FeedbackDto>(feedbackDomainModel);
            return Ok(feedbackDto);
        }
    }
}
