using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SoapService.Interfaces
{
    [ServiceContract]
    public interface ISoapService
    {
        [OperationContract]
        Task<string> GetData(int value);

        [OperationContract]
        Task<string> GetMoreData(string value);
    }

}
