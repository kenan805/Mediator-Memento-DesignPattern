using System;
using System.Collections.Generic;

namespace Mediator_MementoDesignPattern
{
    public interface IChatMediator
    {
        void AddUser(User user);
        void SendMessageToAllUsers(string message, User currentUsr);
    }

    public class ChatMediator : IChatMediator
    {
        private List<User> users;

        public ChatMediator()
        {
            users = new List<User>();
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void SendMessageToAllUsers(string message, User currentUsr)
        {
            users.ForEach(w =>
            {
                if (w != currentUsr)    // current userden basqa butun userlere mesaj gedir
                {
                    w.ReceiveMessage(message);
                }
            });
        }
    }

    public abstract class User
    {
        private IChatMediator chatMediator;
        private string name;

        public User(IChatMediator chatMediator, string name)
        {
            this.chatMediator = chatMediator;
            this.name = name;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public IChatMediator ChatMediator
        {
            get
            {
                return chatMediator;
            }
        }

        public abstract void SendMessage(string message);
        public abstract void ReceiveMessage(string message);
    }

    public class BasicUser : User
    {
        public BasicUser(IChatMediator chatMediator, string name)
            : base(chatMediator, name)
        {
        }

        public override void SendMessage(string message)
        {
            this.ChatMediator.SendMessageToAllUsers(message, this);
        }

        public override void ReceiveMessage(string message)
        {
            Console.WriteLine("Basic User: " + this.Name + " receive message: " + message);
        }
    }

    public class PremiumUser : User
    {
        public PremiumUser(IChatMediator chatMediator, string name)
            : base(chatMediator, name)
        {
        }

        public override void SendMessage(string message)
        {
            this.ChatMediator.SendMessageToAllUsers(message, this);
        }

        public override void ReceiveMessage(string message)
        {
            Console.WriteLine("Premium User: " + this.Name + " receive message: " + message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IChatMediator chatMediator = new ChatMediator();
            //3 online var chatda
            User user1 = new BasicUser(chatMediator, "User 1");
            User user2 = new PremiumUser(chatMediator, "User 2");
            User user3 = new PremiumUser(chatMediator, "User 3");
            chatMediator.AddUser(user1);
            chatMediator.AddUser(user2);
            chatMediator.AddUser(user3);

            //yenisi add olundu
            User newUser = new BasicUser(chatMediator, "New User");
            chatMediator.AddUser(newUser);

            //bu haqda melumat digerlerine gedir
            newUser.SendMessage("New user is online.");
        }
    }
}
