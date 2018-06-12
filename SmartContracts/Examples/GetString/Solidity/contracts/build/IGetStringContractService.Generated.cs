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
    public interface IGetStringContractService
    {
        Task<TransactionReceipt> ExecuteTransactionAsync(Func<IGetStringContractService, Task<string>> func, int timeoutInSeconds = 120);

        Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash);

        Function GetFunctionGetMyString();


        Task<byte[]> GetMyStringCallAsync();
        Task<byte[]> GetMyStringCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);



    }
}
