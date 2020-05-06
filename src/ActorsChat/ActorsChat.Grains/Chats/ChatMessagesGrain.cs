using ActorsChat.Contracts.Chats;
using ActorsChat.Domain.Types;
using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Grains.Chats
{
    public class ChatMessagesGrain : Grain<List<ChatMessage>>, IChatMessages
    {
        private IAsyncStream<ChatMessage> messagesStream;

        public override Task OnActivateAsync()
        {
            messagesStream = GetStreamProvider("Default").GetStream<ChatMessage>(this.GetPrimaryKey(), "Chats");
            return Task.CompletedTask;
        }

        public Task<IEnumerable<ChatMessage>> GetMessages(int index, int count = 50)
        {
            return Task.FromResult(State.OrderByDescending(m => m.SentDate).Skip(index).Take(count));
        }

        public async Task SendMessage(ChatMessage message)
        {
            State.Add(message);
            await WriteStateAsync();
            await messagesStream.OnNextAsync(message);
        }
    }
}
