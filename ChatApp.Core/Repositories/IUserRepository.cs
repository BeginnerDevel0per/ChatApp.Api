using ChatApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        IQueryable<User> SearchUsers(string UserName);



        Task<User?> GetUser(Guid UserIdToFetch);

        IQueryable<User> FriendRequests(Guid UserId);

        Task<FriendsOfUser?> IsFriendsOfUsersAsync(Guid FriendOrRequestId, Guid UserId);

        IQueryable<FriendsOfUser?> FriendsOfUsers(Guid UserId);

        Task<FriendsOfUser?> GetFriendRequest(Guid FriendRequestId, Guid UserId);






        Task SendFriendRequest(FriendsOfUser friendRequest);

        void AcceptFriendRequest(FriendsOfUser acceptFriendRequest);
        void RemoveFriendRequestOrFriend(FriendsOfUser friendRequest);
    }
}
