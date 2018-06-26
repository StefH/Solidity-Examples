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
    public interface IArtifactOwnershipService
    {
        Task<TransactionReceipt> ExecuteTransactionAsync(Func<IArtifactOwnershipService, Task<string>> func, int timeoutInSeconds = 120);

        Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash);

        Function GetFunctionCfoAddress();
        Function GetFunctionName();
        Function GetFunctionApprove();
        Function GetFunctionCeoAddress();
        Function GetFunctionImplementsERC721();
        Function GetFunctionCreationFee();
        Function GetFunctionTotalSupply();
        Function GetFunctionSetCEO();
        Function GetFunctionUnassignAdmin();
        Function GetFunctionPayForArtifact();
        Function GetFunctionGameserverAddress();
        Function GetFunctionGetArtifactTypeId();
        Function GetFunctionAssignAdmin();
        Function GetFunctionUnpause();
        Function GetFunctionSetCFO();
        Function GetFunctionAdminAddresses();
        Function GetFunctionTakeOwnership();
        Function GetFunctionPaused();
        Function GetFunctionWithdrawBalance();
        Function GetFunctionOwnerOf();
        Function GetFunctionBalanceOf();
        Function GetFunctionPause();
        Function GetFunctionSymbol();
        Function GetFunctionTransfer();
        Function GetFunctionCreateArtifact();
        Function GetFunctionChangeCreationPrice();
        Function GetFunctionGetArtifactsAmount();
        Function GetFunctionSetGameserver();
        Function GetFunctionOwnershipTokenCount();
        Function GetFunctionArtifactIndexToApproved();
        Function GetFunctionArtifactIndexToOwner();

        Event GetApproval();
        Event GetTransfer();
        Event GetPayedCreation();

        Task<string> CfoAddressCallAsync();
        Task<string> CfoAddressCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> NameCallAsync();
        Task<string> NameCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> CeoAddressCallAsync();
        Task<string> CeoAddressCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<bool> ImplementsERC721CallAsync();
        Task<bool> ImplementsERC721CallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<byte> CreationFeeCallAsync(string a);
        Task<byte> CreationFeeCallAsync(string addressFrom, string a, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> TotalSupplyCallAsync();
        Task<BigInteger> TotalSupplyCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> GameserverAddressCallAsync();
        Task<string> GameserverAddressCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<int> GetArtifactTypeIdCallAsync(BigInteger _id);
        Task<int> GetArtifactTypeIdCallAsync(string addressFrom, BigInteger _id, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<bool> AdminAddressesCallAsync(string a);
        Task<bool> AdminAddressesCallAsync(string addressFrom, string a, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<bool> PausedCallAsync();
        Task<bool> PausedCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> OwnerOfCallAsync(BigInteger _tokenId);
        Task<string> OwnerOfCallAsync(string addressFrom, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> BalanceOfCallAsync(string _owner);
        Task<BigInteger> BalanceOfCallAsync(string addressFrom, string _owner, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> SymbolCallAsync();
        Task<string> SymbolCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> CreateArtifactCallAsync(int _typeId, string _owner);
        Task<BigInteger> CreateArtifactCallAsync(string addressFrom, int _typeId, string _owner, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> GetArtifactsAmountCallAsync();
        Task<BigInteger> GetArtifactsAmountCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> OwnershipTokenCountCallAsync(string a);
        Task<BigInteger> OwnershipTokenCountCallAsync(string addressFrom, string a, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> ArtifactIndexToApprovedCallAsync(BigInteger a);
        Task<string> ArtifactIndexToApprovedCallAsync(string addressFrom, BigInteger a, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> ArtifactIndexToOwnerCallAsync(BigInteger a);
        Task<string> ArtifactIndexToOwnerCallAsync(string addressFrom, BigInteger a, HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<string> ApproveAsync(string addressFrom, string _to, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> SetCEOAsync(string addressFrom, string _newCEO, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> UnassignAdminAsync(string addressFrom, string _adminAddress, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> PayForArtifactAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> AssignAdminAsync(string addressFrom, string _adminAddress, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> UnpauseAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> SetCFOAsync(string addressFrom, string _newCFO, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> TakeOwnershipAsync(string addressFrom, string _from, string _to, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> WithdrawBalanceAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> PauseAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> TransferAsync(string addressFrom, string _to, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> CreateArtifactAsync(string addressFrom, int _typeId, string _owner, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> ChangeCreationPriceAsync(string addressFrom, ulong newPrice, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> SetGameserverAsync(string addressFrom, string _newGameserver, HexBigInteger gas = null, HexBigInteger valueAmount = null);


    }
}
