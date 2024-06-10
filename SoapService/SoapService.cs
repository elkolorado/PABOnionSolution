namespace SoapService;

using global::SoapService.Interfaces;
using System.Threading.Tasks;

public class SoapService : ISoapService
{
    public Task<string> GetData(int value)
    {
        return Task.FromResult($"You entered: {value}");
    }

    public Task<string> GetMoreData(string value)
    {
        return Task.FromResult($"You entered: {value}");
    }
}

