using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace ChatService
{
    public class NotificationService : Chat.ChatBase
    {
        public override async Task JoinChat(IAsyncStreamReader<ChatMessage> requestStream, IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var message = requestStream.Current;
                Console.WriteLine($"Received message from {message.SenderId}: {message.Message}");

                // Wys³anie wiadomoœci do wszystkich uczestników czatu
                await responseStream.WriteAsync(new ChatMessage
                {
                    SenderId = message.SenderId,
                    Message = $"Echo: {message.Message}"
                });
            }
        }
    }
}
    