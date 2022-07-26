using AutoMapper;
using Chat.Data.Dtos;
using Chat.Data.Repositories;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IChatRepository chatRepo;
        private readonly IMapper mapper;

        public RoomController(IChatRepository _chatRepo, IMapper _mapper)
        {            
            chatRepo = _chatRepo;
            mapper = _mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddMessage(MessageCreateDto message)
        {
            if (message == null)
                return BadRequest("Value Not Valid Paramter name message");

            if (!await chatRepo.CheckRoomExsistAsync(message.RoomId))
                return BadRequest("Room Not Exsist!");

            if (await chatRepo.CheckUserJoinedToGroup(message.UserId, message.RoomId))
                {
                    Console.WriteLine("--> We are Cheking is User Joined are joined To Room ");
                    var messageResponse = await chatRepo.AddMessageAsync(mapper.Map<Message>(message));
                    await chatRepo.AddMessageSeenAsync(messageResponse.Id,messageResponse.UserId);
                }
            else            
                return BadRequest("User Not Joined To Room !");                

            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddRoom(RoomCreateDto room)
        {
            if (room == null)
                return BadRequest();
            await chatRepo.AddRoomAsync(mapper.Map<Room>(room));
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<MessageResponseDto[]>> GetRoomMessages(int roomId)
        {
            if (roomId <= 0)
            {
                return BadRequest();
            }
            var Messages = await chatRepo.GetRoomMessagesAsync(roomId).Result.ToArrayAsync();
            return Messages == null ? NoContent() : Ok(mapper.Map<MessageResponseDto[]>(Messages));
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<RoomResponseDto[]>> GetRoomsByUserId(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("userId Not Valid!");
            }
            var rooms = await chatRepo.GetRoomsByUserIdAsync(userId).ConfigureAwait(false);
            return rooms == null ? NoContent() : Ok(rooms.ToArray<RoomResponseDto>());
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<RoomResponseDto>> GetRoomById(int roomId,int userId)
        {
            if (roomId <= 0)
            {
                return BadRequest("userId Not Valid!");
            }
            var room =await chatRepo.GetRoomByIdAsync(roomId,userId);            
            return room == null ? NoContent() : Ok(room);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddToRoom(int userId, int roomId)
        {
            if (userId <= 0)
            {
                return BadRequest("userId Not Valid!");
            }
            await chatRepo.AddToRoomAsync(userId, roomId);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddMessageSeen(int messageId,int userId)
        {
            if (userId <= 0 || messageId <=0)
            {
                return BadRequest("Values Not Valid!");
            }
            await chatRepo.AddMessageSeenAsync(messageId,userId);
            return Ok();
        }
    }
}
