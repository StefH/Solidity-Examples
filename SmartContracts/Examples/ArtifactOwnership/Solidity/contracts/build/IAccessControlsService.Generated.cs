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
    public interface IAccessControlsService
    {
        Task<TransactionReceipt> ExecuteTransactionAsync(Func<IAccessControlsService, Task<string>> func, int timeoutInSeconds = 120);

        Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash);

        Function GetFunctionCfoAddress();
        Function GetFunctionCeoAddress();
        Function GetFunctionSetCEO();
        Function GetFunctionUnassignAdmin();
        Function GetFunctionGameserverAddress();
        Function GetFunctionAssignAdmin();
        Function GetFunctionUnpause();
        Function GetFunctionSetCFO();
        Function GetFunctionAdminAddresses();
        Function GetFunctionPaused();
        Function GetFunctionWithdrawBalance();
        Function GetFunctionPause();
        Function GetFunctionSetGameserver();


        Task<string> CfoAddressCallAsync();
        Task<string> CfoAddressCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> CeoAddressCallAsync();
        Task<string> CeoAddressCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> GameserverAddressCallAsync();
        Task<string> GameserverAddressCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<bool> AdminAddressesCallAsync(string a);
        Task<bool> AdminAddressesCallAsync(string addressFrom, string a, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<bool> PausedCallAsync();
        Task<bool> PausedCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<string> SetCEOAsync(string addressFrom, string _newCEO, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> UnassignAdminAsync(string addressFrom, string _adminAddress, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> AssignAdminAsync(string addressFrom, string _adminAddress, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> UnpauseAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> SetCFOAsync(string addressFrom, string _newCFO, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> WithdrawBalanceAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> PauseAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> SetGameserverAsync(string addressFrom, string _newGameserver, HexBigInteger gas = null, HexBigInteger valueAmount = null);


    }
}
