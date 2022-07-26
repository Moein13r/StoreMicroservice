using AutoMapper;
using Chat.Data.Dtos;
using Chat.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Chat.Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMapper mapper;
        private readonly ChatDbContext dbContext;

        public ChatRepository(ChatDbContext _dbContext,IMapper _mapper)        
        {
            mapper =_mapper;
            dbContext = _dbContext;
        }
        private async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();

        /// <summary>
        /// Add a Message To Database
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>        
        public async ValueTask<Message> AddMessageAsync(Message message)
        {
            await dbContext.Messages.AddAsync(message);
            dbContext.SaveChanges();
            return message;
        }    
        /// <summary>
        /// Get All Rooms of Specific User By UserId
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public async ValueTask<RoomResponseDto> GetRoomByIdAsync(int roomId,int userId)
        {
            var room=await dbContext.Rooms.Where(r=>r.Id==roomId).FirstAsync();
            int UnseenedMessage=dbContext.Messages.Where(m=>m.RoomId==roomId).Count()-dbContext.SeenedMessages.Where(sm=>sm.Message.RoomId == roomId && sm.UserId == userId).Count();
            var roomResponseDto = mapper.Map<RoomResponseDto>(room);            
            roomResponseDto.UnSeenedMessages=UnseenedMessage;
            return roomResponseDto;
        }
        public async ValueTask<IQueryable<RoomResponseDto>> GetRoomsByUserIdAsync(int userId)
        {
            string query="GetUserRoomsByUserId @userId";            
            return dbContext.GetUserRoomsByUserId.FromSqlRaw(query,new SqlParameter[]{new SqlParameter("@userId",userId)});
        }

        public async ValueTask<Room> AddRoomAsync(Room room)
        {
            await dbContext.Rooms.AddAsync(room);
            dbContext.SaveChanges();
            await this.AddToRoomAsync(room.UserId, room.Id);
            return room;
        }
        public async Task AddToRoomAsync(int userId, int groupId)
        {
            await dbContext.RoomsUsers.AddAsync(new RoomsUser { UserId = userId, RoomId = groupId });
            dbContext.SaveChanges();
        }

        public async ValueTask<IEnumerable<RoomsUser>> GetUserRoomsAsync(int userId)
        {
            return from ru in dbContext.RoomsUsers.AsQueryable() where ru.UserId == userId select ru ;
        }        
        public async Task<bool> CheckRoomExsistAsync(int roomId)
        {
            return dbContext.Rooms.AsQueryable().Where(room => room.Id == roomId).Count() > 0;
        }
        
        public async Task<bool> CheckUserJoinedToGroup(int userId, int roomId)
        {
            return dbContext.RoomsUsers.AsQueryable().Where(RUser => RUser.UserId == userId && RUser.RoomId == roomId).Count() > 0;
        }

        public async ValueTask<IAsyncEnumerable<Message>> GetRoomMessagesAsync(int roomId)
        {
            return dbContext.Messages.Where(m=>m.RoomId==roomId).AsAsyncEnumerable();
        }

        public async Task AddMessageSeenAsync(int messageId,int userId)
        {
            await dbContext.SeenedMessages.AddAsync(new SeenedMessage{MessageId=messageId,UserId=userId});
            dbContext.SaveChanges();
        }
    }
}