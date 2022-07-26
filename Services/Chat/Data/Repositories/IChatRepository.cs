using Chat.Data.Dtos;
using Chat.Models;

namespace Chat.Data.Repositories
{
    public interface IChatRepository
    {
        ValueTask<Message> AddMessageAsync(Message message);
        ValueTask<Room> AddRoomAsync(Room room);
        Task AddMessageSeenAsync(int messageId,int userId);
        Task AddToRoomAsync(int userId,int groupId);
        Task<bool> CheckRoomExsistAsync(int roomId);
        Task<bool> CheckUserJoinedToGroup(int userId, int roomId);
        ValueTask<IAsyncEnumerable<Message>> GetRoomMessagesAsync(int roomId);
        ValueTask<IQueryable<RoomResponseDto>> GetRoomsByUserIdAsync(int userId);        
        ValueTask<IEnumerable<RoomsUser>> GetUserRoomsAsync(int userId);
        ValueTask<RoomResponseDto> GetRoomByIdAsync(int roomId,int userId);
    }
}
