using AutoMapper;
using PFA_ProjectAPI.Domain.Models.Domain;
using PFA_ProjectAPI.Domain.Models.DtoActivity;
using PFA_ProjectAPI.Domain.Models.DtoEvent;
using PFA_ProjectAPI.Domain.Models.DtoFeedback;
using PFA_ProjectAPI.Domain.Models.DtoUser;


namespace PFA_ProjectAPI.Domain.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Create map from Activity Domain Model to ActivityDTO (on l'utilise getAll,getById)
            CreateMap<Activity, ActivityDTO>().ReverseMap();
            //Create map bteween AddActivityRequestDto and Activity Domain model (on l'utilise create) 
            CreateMap<AddActivityRequestDto, Activity>().ReverseMap();
            CreateMap<UpdateActivityRequestDto, Activity>().ReverseMap();

            // Create map between AddEventRequestDto and Event
            CreateMap<AddEventRequestDto, Event>();
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<UpdateEventRequestDto, Event>().ReverseMap();


            CreateMap<AddFeedbackRequestDto, Feedback>();
            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<UpdateFeedbackRequestDto, Feedback>().ReverseMap();


            CreateMap<User, UserDto>(); 
            CreateMap<UserDto, User>();



        }
    }
}
