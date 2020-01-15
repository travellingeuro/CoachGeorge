using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGXC.Services
{
    public interface IPdfServices
    {
        Task CreatePDF(int eventId);
    }
}
