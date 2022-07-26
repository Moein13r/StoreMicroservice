using AutoMapper;
using Chat.Data.Dtos;
using Chat.Models;

namespace Chat.Profiles
{
    public class MessageProfile:Profile
    {
        public MessageProfile()
        {
            //source -> destination
            CreateMap<MessageCreateDto,Message>();
            CreateMap<Message,MessageResponseDto>();
        }
    }
}
