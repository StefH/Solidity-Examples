using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.Contracts;

namespace ConsoleApp
{
   public interface IMultiplyContractService
   {
        Function GetFunctionMultiply();
        Function GetFunctionClose();
        Function GetFunctionAdd();


        Task<BigInteger> MultiplyAsync(BigInteger val);
        Task<BigInteger> AddAsync(BigInteger val1, BigInteger val2);

        Task<string> CloseAsync(string addressFrom, HexBigInteger gas = null, HexBigInteger valueAmount = null);


    }
}
