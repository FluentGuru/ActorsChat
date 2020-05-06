using ActorsChat.Contracts.Chats;
using ActorsChat.Domain.Services;
using ActorsChat.Domain.Types;
using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Grains.Chats
{
    public class ChatState : Chat
    {
    }

    public class ChatGrain : Grain<ChatState>, IChat
    {
        private readonly IDateTime dateTime;
        public ChatGrain(IDateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public async Task<Chat> Start(string creator, string name)
        {
            State = new ChatState() { Id = this.GetPrimaryKey(), Creator = creator, Name = name, CreatedDate = dateTime.Now };
            await WriteStateAsync();
            return State;
        }

        public async Task Join(string nickname)
        {
            var participations = await GetParticipations();
            await participations.SetParticipant(nickname, UserStatuses.Online);
            await SendMessage(nickname, null, ChatMessageTypes.UserJoined);
        }

        public async Task Leave(string nickname)
        {
            var participations = await GetParticipations();
            await participations.RemoveParticipant(nickname);
            await SendMessage(nickname, null, ChatMessageTypes.UserLeft);
        }

        public Task SendMessage(string nickname, string message)
        {
            return SendMessage(nickname, message, ChatMessageTypes.Message);
        }

        private async Task SendMessage(string nickname, string message, ChatMessageTypes type)
        {
            var messages = await GetMessages();
            await messages.SendMessage(new ChatMessage() {
                ChatId = this.GetPrimaryKey(),
                Sender = nickname,
                Message = message,
                Type = type,
                SentDate = dateTime.Now });
        }

        public Task<IChatParticipations> GetParticipations()
        {
            return Task.FromResult(GrainFactory.GetGrain<IChatParticipations>(this.GetPrimaryKey()));
        }

        public Task<IChatMessages> GetMessages()
        {
            return Task.FromResult(GrainFactory.GetGrain<IChatMessages>(this.GetPrimaryKey()));
        }
    }
}
