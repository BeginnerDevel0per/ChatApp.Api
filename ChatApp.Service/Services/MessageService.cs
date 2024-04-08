using AutoMapper;
using ChatApp.Core.Repositories;
using ChatApp.Core.Security;
using ChatApp.Core.Services;
using ChatApp.Core.UnifOfWorks;
using ChatApp.Entities.Entities;
using ChatApp.Hubs;
using ChatApp.Service.Exceptions;
using ChatApp.Service.Helpers;
using ChatApp.Shared.DTOs;
using ChatApp.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;


namespace ChatApp.Service.Services
{
    public class MessageService : GenericService<Message>, IMessageService
    {
        private readonly IGetInformationFromToken _getInformationFromToken;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMessageRepository _messageRepository;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MessageService(IGenericRepository<Message> genericRepository, IHubContext<ChatHub> hubContext, IMessageRepository messageRepository, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IMapper mapper, IUserRepository userRepository, IGetInformationFromToken getInformationFromToken) : base(genericRepository, unitOfWork)
        {
            _getInformationFromToken = getInformationFromToken;
            _hubContext = hubContext;
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task SendMessage(Guid RecevierId, string message)
        {
            try
            {
                var SenderUser = _getInformationFromToken.GetUserIdAndUserName();

                var RecevierUser = await _userRepository.IsFriendsOfUsersAsync(SenderUser.Id, RecevierId);




                if (RecevierUser == null || RecevierUser?.IsRequestAccepted == false)
                    throw new ClientSideException("Bu kullanıcıyla arkadaş değilsin.");


                // kullanıcının kimliklerini alıp listeye atıyoruz daha sonra onları sıralıyoruz.
                var userGuids = new List<Guid> { SenderUser.Id, RecevierId };
                //gelen guid idleri tekrar sıralıyor 
                userGuids.Sort();
                // Kullanıcı kimliklerini birleştirerek bir konuşma kimliği oluşturuyoruz.
                var conversationId = string.Join("", userGuids);
                var Messages = await _messageRepository.AddAsync(new Message
                {
                    Content = message,
                    SenderId = SenderUser.Id,
                    ReceiverId = RecevierId,
                    ConversationId = conversationId
                });
                await _unitOfWork.CommitAsync();
                var messageDto = _mapper.Map<MessageDto>(Messages);

                LastMessageDto lastMessageDtoReceiver = new()
                {
                    Message = messageDto,
                    User = new UserIdAndNameDto
                    { Id = SenderUser.Id, UserName = SenderUser.UserName }
                };

                var RecevierUserName = RecevierUser.FriendOrRequestUser.Id == RecevierId ? RecevierUser.FriendOrRequestUser.UserName : RecevierUser.User.UserName;
                LastMessageDto lastMessageDtoSender = new()
                {
                    Message = messageDto,
                    User = new UserIdAndNameDto
                    { Id = RecevierId, UserName = RecevierUserName }
                };

                await _hubContext.Clients.User(SenderUser.Id.ToString()).SendAsync("AddLastMessage", lastMessageDtoSender);//burada son mesajı göndericiye ekliyorum
                await _hubContext.Clients.User(RecevierId.ToString()).SendAsync("ReceiveMessage", messageDto);
                await _hubContext.Clients.User(RecevierId.ToString()).SendAsync("NotificationMessages", lastMessageDtoReceiver);//burada son mesajı alıcıya ekliyorum


                //mesajla birlikte kullanıcının kendi connection idsinide döndürmeliyim.
            }
            catch (Exception ex)
            {

                throw new ClientSideException(ex.Message);
            }


        }
        public List<LastMessageDto> GetLastMessages()
        {
            var UserId = _getInformationFromToken.GetUserIdAndUserName();
            var LastMessages = _messageRepository.GetLastMessages(UserId.Id);





            List<LastMessageDto> lastMessagesDto = new();
            foreach (var item in LastMessages)
            {
                LastMessageDto lastMessageDto = new();
                lastMessageDto.Message = _mapper.Map<MessageDto>(item);

                //son mesajlarda diğer kullanıcının bilgisini ve son mesajı döndürmem gerektiği için diğer kullanıcıyı ayırıp onu nesneme ekliyorum
                if (item.SenderId == UserId.Id)
                    lastMessageDto.User = _mapper.Map<UserIdAndNameDto>(item.Receiver);
                else
                    lastMessageDto.User = _mapper.Map<UserIdAndNameDto>(item.Sender);



                lastMessagesDto.Add(lastMessageDto);
            }



            return lastMessagesDto;
        }

        public IQueryable<Message> GetMessagesBetweenToUsers(Guid OtherUserId)
        {
            var SenderUser = _getInformationFromToken.GetUserIdAndUserName();
            var Messages = _messageRepository.GetMessagesBetweenToUsers(SenderUser.Id, OtherUserId);
            return Messages;
        }



    }
}
