using AutoMapper;
using Chat.Data.Dtos;
using Chat.Data.Repositories;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Chat.Hubs
{    
    public class ChatHub : Hub
    {
        public readonly IChatRepository chatRepo;
        private readonly IMapper mapper;

        public ChatHub(IChatRepository _chatRepo,IMapper _mapper)
        {
            chatRepo = _chatRepo;
            mapper=_mapper;
        }
        public override async Task OnConnectedAsync()
        {            
            Console.WriteLine($"--> We Received Request! Connectction ID : {Context.ConnectionId}.");
            var context = Context.GetHttpContext();
            int userId = int.Parse(context.Request.Headers["UserId"]);
            Console.WriteLine(" --> UserId: " + userId);
            foreach(RoomsUser item in await chatRepo.GetUserRoomsAsync(userId))
                await Groups.AddToGroupAsync(Context.ConnectionId,item.RoomId.ToString());
        }
        public async void SendMessage(MessageCreateDto message)
        {            
            Console.WriteLine("--> We Receive Message!");
            var result = await chatRepo.AddMessageAsync(mapper.Map<Message>(message));            
            await Clients.Group(message.RoomId.ToString()).SendAsync("ReceiveMessage", result);
            Console.WriteLine("--> We Sended Messege To Listeners!");
        }
        public async Task CreateRoom(string roomJson)
        {
            var roomObject = JsonSerializer.Deserialize<Room>(roomJson);
            await chatRepo.AddRoomAsync(roomObject);
            await Groups.AddToGroupAsync(roomObject.UserId.ToString(),roomObject.Id.ToString());
        }
        public async Task AddToRoom(string userId, string roomId)
        {
            if (await chatRepo.CheckUserJoinedToGroup(int.Parse(userId),int.Parse(roomId)))
            {
                return;
            }
            await chatRepo.AddToRoomAsync(int.Parse(userId), int.Parse(roomId));
            await Groups.AddToGroupAsync(userId, roomId);
        }
    }
}