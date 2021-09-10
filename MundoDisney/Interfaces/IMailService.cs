using MundoDisney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Interfaces
{
    public interface IMailService
    {
        Task SendMail(Usuario user);
    }
}
