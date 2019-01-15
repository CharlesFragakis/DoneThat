using DoneThatMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoneThatMVC.DatabaseAccess
{
    public static class FriendshipManager
    {
        public static void AddFriendship(int MakeRequestId, int AcceptRequestId)
        {

            using (AppContext db = new AppContext())
            {
                Friendship friendship = new Friendship();
                User makeRequestUser = db.Users.FirstOrDefault(i => i.Id == MakeRequestId);
                User acceptRequestUser = db.Users.FirstOrDefault(i => i.Id == AcceptRequestId);
                friendship.MakeRequestUser = makeRequestUser;
                friendship.AcceptRequestUser = acceptRequestUser;
                friendship.IsFriend = false;
                db.Friendships.Add(friendship);
                db.SaveChanges();
            }
        }

        public static void RemoveFriendship(int MakeRequestId, int AcceptRequestId)
        {
            using (AppContext db = new AppContext())
            {
                Friendship friendship = db.Friendships.FirstOrDefault(i => i.AcceptRequestId == AcceptRequestId && i.MakeRequestId == MakeRequestId);
                db.Friendships.Remove(friendship);
                db.SaveChanges();
            }
        }

        public static List<Friendship> GetPendingFriendshipsForCurrentUser(int userId)
        {
            List<Friendship> friendships;
            using (AppContext db = new AppContext())
            {
                friendships = db.Friendships.Where(i => i.AcceptRequestId == userId && !i.IsFriend).ToList();
            }
            return friendships;
        }

        public static void AcceptFriendShip(int MakeRequestId, int AcceptRequestId)
        {
            using (AppContext db = new AppContext())
            {
                Friendship friendship = db.Friendships.FirstOrDefault(i => i.AcceptRequestId == AcceptRequestId && i.MakeRequestId == MakeRequestId);
                friendship.IsFriend = true;
                db.SaveChanges();
            }
        }
    }
}
