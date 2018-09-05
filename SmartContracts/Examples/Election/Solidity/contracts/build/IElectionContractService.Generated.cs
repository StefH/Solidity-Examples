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
    public interface IElectionContractService
    {
        Task<TransactionReceipt> ExecuteTransactionAsync(Func<IElectionContractService, Task<string>> func, int timeoutInSeconds = 120);

        Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash);

        Function GetFunctionRecursion();
        Function GetFunctionGetVote();
        Function GetFunctionVote();


        Task<BigInteger> GetVoteCallAsync(string name);
        Task<BigInteger> GetVoteCallAsync(string addressFrom, string name, HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<string> RecursionAsync(string addressFrom, BigInteger i, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> VoteAsync(string addressFrom, string name, HexBigInteger gas = null, HexBigInteger valueAmount = null);


    }
}
