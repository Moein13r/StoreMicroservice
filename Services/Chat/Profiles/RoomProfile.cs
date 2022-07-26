using AutoMapper;
using Chat.Data.Dtos;
using Chat.Models;

namespace Chat.Profiles
{
    public class RoomProfile:Profile
    {
        public RoomProfile()
        {
            //source -> destination
            CreateMap<RoomCreateDto,Room>();
            CreateMap<Room,RoomResponseDto>();
        }
    }
}