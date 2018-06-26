using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

// Created with 'Solidity to C# generator' by stef.heyenrath@mstack.nl (mStack B.V.)
// Based on abi-code-gen (https://github.com/Nethereum/abi-code-gen)
namespace ConsoleApp
{
    public class ERC721Service : IERC721Service
    {
        public static string ABI = @"[{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_tokenId"",""type"":""uint256""}],""name"":""approve"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""implementsERC721"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""pure"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""name"":""total"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_from"",""type"":""address""},{""name"":""_to"",""type"":""address""},{""name"":""_tokenId"",""type"":""uint256""}],""name"":""takeOwnership"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_tokenId"",""type"":""uint256""}],""name"":""ownerOf"",""outputs"":[{""name"":""owner"",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""name"":""balance"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_tokenId"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""owner"",""type"":""address""},{""indexed"":true,""name"":""approved"",""type"":""address""},{""indexed"":true,""name"":""tokenId"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""}]";

        public static string ByteCode = "0x";

        public static async Task<string> DeployContractAsync(Web3 web3, string addressFrom,  CancellationTokenSource token = null, HexBigInteger gas = null)
        {
            if (gas == null)
            {
                BigInteger estimatedGas = await web3.Eth.DeployContract.EstimateGasAsync(ABI, ByteCode, addressFrom).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            var transactionReceipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(ABI, ByteCode, addressFrom, gas, token).ConfigureAwait(false);
            return transactionReceipt.ContractAddress;
        }

        private readonly Web3 _web3;
        private readonly Contract _contract;

        public ERC721Service(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public async Task<TransactionReceipt> ExecuteTransactionAsync(Func<IERC721Service, Task<string>> func, int timeoutInSeconds)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutInSeconds));

            string transaction = await func(this);
            var receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction).ConfigureAwait(false);

            while (receipt == null && !cts.IsCancellationRequested)
            {
                await Task.Delay(500);
                receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction).ConfigureAwait(false);
            }

            return receipt;
        }

        public Task<BlockWithTransactions> GetBlockWithTransactionsAsync(string blockhash)
        {
            return _web3.Eth.Blocks.GetBlockWithTransactionsByHash.SendRequestAsync(blockhash);
        }

        public Function GetFunctionApprove()
        {
            return _contract.GetFunction("approve");
        }

        public Function GetFunctionImplementsERC721()
        {
            return _contract.GetFunction("implementsERC721");
        }

        public Function GetFunctionTotalSupply()
        {
            return _contract.GetFunction("totalSupply");
        }

        public Function GetFunctionTakeOwnership()
        {
            return _contract.GetFunction("takeOwnership");
        }

        public Function GetFunctionOwnerOf()
        {
            return _contract.GetFunction("ownerOf");
        }

        public Function GetFunctionBalanceOf()
        {
            return _contract.GetFunction("balanceOf");
        }

        public Function GetFunctionTransfer()
        {
            return _contract.GetFunction("transfer");
        }


        public Event GetApproval()
        {
            return _contract.GetEvent("Approval");
        }


        public Task<bool> ImplementsERC721CallAsync()
        {
            var function = GetFunctionImplementsERC721();
            return function.CallAsync<bool>();
        }

        public async Task<bool> ImplementsERC721CallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionImplementsERC721();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<bool>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public Task<BigInteger> TotalSupplyCallAsync()
        {
            var function = GetFunctionTotalSupply();
            return function.CallAsync<BigInteger>();
        }

        public async Task<BigInteger> TotalSupplyCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionTotalSupply();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public Task<string> OwnerOfCallAsync(BigInteger _tokenId)
        {
            var function = GetFunctionOwnerOf();
            return function.CallAsync<string>(_tokenId);
        }

        public async Task<string> OwnerOfCallAsync(string addressFrom, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionOwnerOf();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _tokenId).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<string>(addressFrom, gas, valueAmount, _tokenId).ConfigureAwait(false);
        }

        public Task<BigInteger> BalanceOfCallAsync(string _owner)
        {
            var function = GetFunctionBalanceOf();
            return function.CallAsync<BigInteger>(_owner);
        }

        public async Task<BigInteger> BalanceOfCallAsync(string addressFrom, string _owner, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionBalanceOf();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _owner).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount, _owner).ConfigureAwait(false);
        }


        public async Task<string> ApproveAsync(string addressFrom, string _to, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionApprove();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _to, _tokenId).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, _to, _tokenId).ConfigureAwait(false);
        }

        public async Task<string> TakeOwnershipAsync(string addressFrom, string _from, string _to, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionTakeOwnership();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _from, _to, _tokenId).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, _from, _to, _tokenId).ConfigureAwait(false);
        }

        public async Task<string> TransferAsync(string addressFrom, string _to, BigInteger _tokenId, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionTransfer();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _to, _tokenId).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, _to, _tokenId).ConfigureAwait(false);
        }




    }


    public class Approval
    {
        [Parameter("address", "owner", 1, true)]
        public string Owner { get; set; }

        [Parameter("address", "approved", 2, true)]
        public string Approved { get; set; }

        [Parameter("uint256", "tokenId", 3, true)]
        public BigInteger TokenId { get; set; }

    }


}
