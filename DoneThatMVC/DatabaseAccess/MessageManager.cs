using DoneThatMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoneThatMVC.DatabaseAccess
{
    public static class MessageManager
    {
        public static Message AddMessage(Message message)
        {
            Message msgdb;

            using (AppContext db = new AppContext())
            {
                db.Messages.Add(message);
                msgdb = db.Messages.FirstOrDefault(m => m.Id == message.Id);
                db.SaveChanges();
            }

            return message;
        }


        public static void EditMessage(int msgId, string msg)
        {
            Message message;
            using (var db = new AppContext())
            {
                message = db.Messages.Include("Receiver").Include("Sender").First(i => i.Id == msgId);
                message.Text = msg;
                message.TimeStamp = DateTime.Now;
                db.SaveChanges();
            }
        }

        public static void DeleteMessage(int msgId)
        {
            using (var db = new AppContext())
            {
                Message message = db.Messages.First(i => i.Id == msgId);
                db.Messages.Remove(message);
                db.SaveChanges();
            }
        }

        public static List<Message> ConversationBetween(int currentUserId, int userConvWithId)
        {
            List<Message> messages = new List<Message>();
            using (var db = new AppContext())
            {

                User currentUser = db.Users.Find(currentUserId);
                User convWithUser = db.Users.Find(userConvWithId);

                var q1 = db.Messages.Include("Receiver").Include("Sender").Where(i => i.Receiver.Id == currentUser.Id && i.Sender.Id == convWithUser.Id);
                var q2 = db.Messages.Include("Receiver").Include("Sender").Where(i => i.Sender.Id == currentUser.Id && i.Receiver.Id == convWithUser.Id);

                messages = q1.Union(q2).ToList();


            }
            return messages;
        }


    }
}


