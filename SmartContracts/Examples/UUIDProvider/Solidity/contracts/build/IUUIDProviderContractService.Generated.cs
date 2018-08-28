using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

// Created with 'Solidity to C# generator' by stef.heyenrath@mstack.nl (mStack B.V.)
// Based on abi-code-gen (https://github.com/Nethereum/abi-code-gen)
namespace ConsoleApp
{
    public interface IUUIDProviderContractService
    {
        Task<TransactionReceipt> ExecuteTransactionAsync(Func<IUUIDProviderContractService, Task<string>> func, int timeoutInSeconds = 120);

        Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash);

        Function GetFunctionGenerateUUID4();

        Event GetUUIDEvent();

        Task<byte[]> GenerateUUID4CallAsync();
        Task<byte[]> GenerateUUID4CallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<string> GenerateUUID4Async(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);


    }
}
