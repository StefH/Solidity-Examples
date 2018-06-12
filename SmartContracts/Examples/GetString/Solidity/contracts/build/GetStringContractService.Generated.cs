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
    public class GetStringContractService : IGetStringContractService
    {
        public static string ABI = @"[{""constant"":true,""inputs"":[],""name"":""getMyString"",""outputs"":[{""name"":"""",""type"":""bytes""}],""payable"":false,""stateMutability"":""view"",""type"":""function""}]";

        public static string ByteCode = "0x606060405260408051908101604052600a81527f736f6d65537472696e67000000000000000000000000000000000000000000006020820152600090805161004b92916020019061005c565b50341561005757600080fd5b6100f7565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061009d57805160ff19168380011785556100ca565b828001600101855582156100ca579182015b828111156100ca5782518255916020019190600101906100af565b506100d69291506100da565b5090565b6100f491905b808211156100d657600081556001016100e0565b90565b6101bf806101066000396000f3006060604052600436106100405763ffffffff7c01000000000000000000000000000000000000000000000000000000006000350416637e7b8a968114610045575b600080fd5b341561005057600080fd5b6100586100cf565b60405160208082528190810183818151815260200191508051906020019080838360005b8381101561009457808201518382015260200161007c565b50505050905090810190601f1680156100c15780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b6100d7610181565b6100df610181565b60008054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156101755780601f1061014a57610100808354040283529160200191610175565b820191906000526020600020905b81548152906001019060200180831161015857829003601f168201915b50939695505050505050565b602060405190810160405260008152905600a165627a7a723058208fe705168110a4ce59989ae38882e4acf74fa61595fb2417e82f71f53d445a520029";

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

        public GetStringContractService(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public async Task<TransactionReceipt> ExecuteTransactionAsync(Func<IGetStringContractService, Task<string>> func, int timeoutInSeconds)
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

        public Function GetFunctionGetMyString()
        {
            return _contract.GetFunction("getMyString");
        }



        public Task<byte[]> GetMyStringCallAsync()
        {
            var function = GetFunctionGetMyString();
            return function.CallAsync<byte[]>();
        }

        public async Task<byte[]> GetMyStringCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetMyString();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<byte[]>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }





    }



}
