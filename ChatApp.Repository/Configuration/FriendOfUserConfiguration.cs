using ChatApp.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Repository.Configuration
{
    public class FriendOfUserConfiguration : IEntityTypeConfiguration<FriendsOfUser>
    {
        public void Configure(EntityTypeBuilder<FriendsOfUser> builder)
        {
            builder
           .HasOne(f => f.FriendOrRequestUser)
           .WithMany()
           .HasForeignKey(f => f.FriendOrRequestId) // FriendOrRequestUser ı User tablosuna UserId üzerinden bağla
           .OnDelete(DeleteBehavior.Restrict); //Silme davranışını ayarla

            builder.HasOne(f => f.User).WithMany()
                .HasForeignKey(f => f.UserId) // User ı User tablosuna UserId üzerinden bağla
                .OnDelete(DeleteBehavior.Restrict); //Silme davranışını ayarla
        }
    }
}
