using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared.DTOs
{
    public class MessageDto
    {

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        public Guid SenderId { get; set; }//GöndereId
        public Guid ReceiverId { get; set; }//ulaşan Id
        public string Content { get; set; }


        //public bool Sender { get; set; }//gönderildimi

        //public Guid UserId { get; set; }//mesajın kullanıcı idsi kim gönderdiyse.
        ////public Guid SenderId { get; set; }//mesajın kullanıcı idsi kim gönderdiyse.

        ////public Guid ReceiverId { get; set; }//mesajın kullanıcı idsi kim gönderdiyse.

        //public string UserName { get; set; }

        //public string Message { get; set; }

        //public DateTime DateSent { get; set; }

        //public bool IsRead { get; set; }//okundumu.

    }
}
