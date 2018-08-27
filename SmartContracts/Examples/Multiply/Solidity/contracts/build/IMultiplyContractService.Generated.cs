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
    public interface IMultiplyContractService
    {
        Task<TransactionReceipt> ExecuteTransactionAsync(Func<IMultiplyContractService, Task<string>> func, int timeoutInSeconds = 120);

        Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash);

        Function GetFunctionGetOrderIdAtIndex();
        Function GetFunctionCountOrders();
        Function GetFunctionMultiply();
        Function GetFunctionGetOrderById();
        Function GetFunctionAddOrder();
        Function GetFunctionClose();
        Function GetFunctionIsExistingOrder();
        Function GetFunctionAdd();
        Function GetFunctionStoreValue();

        Event GetAddEvent();
        Event GetStoreValueEvent();
        Event GetAddOrderEvent();

        Task<byte[]> GetOrderIdAtIndexCallAsync(BigInteger index);
        Task<byte[]> GetOrderIdAtIndexCallAsync(string addressFrom, BigInteger index, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> CountOrdersCallAsync();
        Task<BigInteger> CountOrdersCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> MultiplyCallAsync(BigInteger val);
        Task<BigInteger> MultiplyCallAsync(string addressFrom, BigInteger val, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<bool> IsExistingOrderCallAsync(byte[] id);
        Task<bool> IsExistingOrderCallAsync(string addressFrom, byte[] id, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> AddCallAsync(BigInteger val1, BigInteger val2);
        Task<BigInteger> AddCallAsync(string addressFrom, BigInteger val1, BigInteger val2, HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<string> AddOrderAsync(string addressFrom, string name, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> CloseAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> AddAsync(string addressFrom, BigInteger val1, BigInteger val2, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> StoreValueAsync(string addressFrom, BigInteger val, HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<GetOrderById> GetOrderByIdCallAsync(byte[] orderId);
        Task<GetOrderById> GetOrderByIdCallAsync(string addressFrom, byte[] orderId, HexBigInteger gas = null, HexBigInteger valueAmount = null);

    }
}
