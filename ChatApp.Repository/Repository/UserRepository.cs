using ChatApp.Core.Repositories;
using ChatApp.Entities.Entities;
using ChatApp.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Repository.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public IQueryable<User> SearchUsers(string UserName)
        {

            return _dbContext.Users.Where(u => u.UserName.Contains(UserName)).AsNoTracking().AsQueryable();
        }
        public async Task<User?> GetUser(Guid FriendOrRequestId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == FriendOrRequestId);
        }




        public IQueryable<User> FriendRequests(Guid UserId)
        {//burada login olan kullanıcıya istek gelmişmi onu kontrol ettiriyorum ve kim gönderdiyse isteği o kullanıcıyı döndürüyorum.


            var friendRequests = _dbContext.FriendsOfUsers.Where(x => x.UserId == UserId && x.IsRequestAccepted == false).Select(x => x.FriendOrRequestUser).AsNoTracking().AsQueryable();
            #region deneme sorgu
            //var friendRequests = _dbContext.FriendsOfUsers.Where(x => (x.UserId == UserId) && x.IsRequestAccepted == false).AsNoTracking().AsQueryable()
            //    .Join(_dbContext.Users,
            //    Friendrequest => Friendrequest.FriendOrRequestId,
            //    user => user.Id, (requesUser, user) => user)
            //    .AsEnumerable();
            #endregion
            return friendRequests;
        }

        public IQueryable<FriendsOfUser?> FriendsOfUsers(Guid UserId)
        {//burada login olan kullanıcının birbirleriyle ekli olanları dönüyorum.
            var friendsOfUsers = _dbContext.FriendsOfUsers.Where(x => (x.UserId == UserId || x.FriendOrRequestId == UserId) && x.IsRequestAccepted == true)
.AsNoTracking().AsQueryable();
            #region deneme sorgu
            //var friendsOfUsers = _dbContext.FriendsOfUsers.Where(x => (x.UserId == UserId || x.FriendOrRequestId == UserId) && x.IsRequestAccepted == true).AsNoTracking().AsQueryable()
            //    .Join(_dbContext.Users,
            //    Friend => (Friend.FriendOrRequestId == UserId ? Friend.UserId : Friend.FriendOrRequestId),
            //    user => user.Id, (requesUser, user) => user)
            //    .AsEnumerable();
            #endregion
            return friendsOfUsers;
        }


        public async Task<FriendsOfUser?> IsFriendsOfUsersAsync(Guid SenderId, Guid ReceiverId)
        {//birbirlerine istek atma, gönderme ve arkadaş durumu varmı eğer varsa kullanıcılarla birlikte getiriyor.
            return await _dbContext.FriendsOfUsers.Include(x=>x.FriendOrRequestUser).Include(x=>x.User).Where(x => (x.UserId == ReceiverId && x.FriendOrRequestId == SenderId) || (x.UserId == SenderId && x.FriendOrRequestId == ReceiverId)).FirstOrDefaultAsync();
        }

   
        public async Task<FriendsOfUser?> GetFriendRequest(Guid FriendRequestId, Guid UserId)
        {
            return await _dbContext.FriendsOfUsers.FirstOrDefaultAsync(x => (x.UserId == UserId && x.FriendOrRequestId == FriendRequestId && x.IsRequestAccepted == false));
        }


        public async Task SendFriendRequest(FriendsOfUser friendRequest)
        {
            await _dbContext.FriendsOfUsers.AddAsync(friendRequest);
        }

        public void AcceptFriendRequest(FriendsOfUser acceptFriendRequest)
        {
            _dbContext.FriendsOfUsers.Update(acceptFriendRequest);
        }

        public void RemoveFriendRequestOrFriend(FriendsOfUser friendRequest)
        {
            _dbContext.FriendsOfUsers.Remove(friendRequest);
        }

    }
}
