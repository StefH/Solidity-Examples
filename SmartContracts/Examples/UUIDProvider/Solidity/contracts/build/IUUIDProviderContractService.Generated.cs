using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.Contracts;

namespace ConsoleApp
{
   public interface IUUIDProviderContractService
   {
        Function GetFunctionGenerateUUID4();

        Event GetUUIDEvent();

        Task<byte[]> GenerateUUID4CallAsync();

        Task<string> GenerateUUID4Async(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);


    }
}
