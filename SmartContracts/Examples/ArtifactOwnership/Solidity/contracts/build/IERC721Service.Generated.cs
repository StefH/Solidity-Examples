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
    public interface IERC721Service
    {
        Task<TransactionReceipt> ExecuteTransactionAsync(Func<IERC721Service, Task<string>> func, int timeoutInSeconds = 120);

        Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash);

        Function GetFunctionApprove();
        Function GetFunctionImplementsERC721();
        Function GetFunctionTotalSupply();
        Function GetFunctionTakeOwnership();
        Function GetFunctionOwnerOf();
        Function GetFunctionBalanceOf();
        Function GetFunctionTransfer();

        Event GetApproval();

        Task<bool> ImplementsERC721CallAsync();
        Task<bool> ImplementsERC721CallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> TotalSupplyCallAsync();
        Task<BigInteger> TotalSupplyCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> OwnerOfCallAsync(BigInteger _tokenId);
        Task<string> OwnerOfCallAsync(string addressFrom, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> BalanceOfCallAsync(string _owner);
        Task<BigInteger> BalanceOfCallAsync(string addressFrom, string _owner, HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<string> ApproveAsync(string addressFrom, string _to, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> TakeOwnershipAsync(string addressFrom, string _from, string _to, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> TransferAsync(string addressFrom, string _to, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null);


    }
}
