using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Telegram.Bot;

namespace KotanTelegramBot.FunctionApp.Commands
{
    public class ChatInformCommandHandler : IRequestHandler<ChatInformCommand>
    {
        private readonly TelegramBotClient _botClient;

        public ChatInformCommandHandler(TelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task<Unit> Handle(ChatInformCommand request, CancellationToken cancellationToken)
        {
            await _botClient.SendTextMessageAsync(request.ChatId, request.Message, cancellationToken: cancellationToken);
            return new Unit();
        }
    }
}