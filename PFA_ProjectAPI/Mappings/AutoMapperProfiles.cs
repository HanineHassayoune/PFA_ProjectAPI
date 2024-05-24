using AutoMapper;
using PFA_ProjectAPI.Models.Domain;
using PFA_ProjectAPI.Models.DTO;
using PFA_ProjectAPI.Models.DtoEvent;
using PFA_ProjectAPI.Models.Enums;

namespace PFA_ProjectAPI.Mappings
{
    public class AutoMapperProfiles:Profile
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
            CreateMap<Event ,EventDto>().ReverseMap();
            CreateMap<UpdateEventRequestDto, Event>().ReverseMap();
            

        }
    }
}
