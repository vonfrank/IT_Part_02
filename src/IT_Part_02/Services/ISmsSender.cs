using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT_Part_02.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
