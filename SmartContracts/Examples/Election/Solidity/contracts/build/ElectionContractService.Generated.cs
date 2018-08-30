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
    public class ElectionContractService : IElectionContractService
    {
        public static string ABI = @"[{""constant"":true,""inputs"":[{""name"":""name"",""type"":""string""}],""name"":""getVote"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""name"",""type"":""string""}],""name"":""vote"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""}]";

        public static string ByteCode = "0x608060405234801561001057600080fd5b50604080517f416c696365000000000000000000000000000000000000000000000000000000815260016005820181905282519182900360250182206000908190557f426f620000000000000000000000000000000000000000000000000000000000835260038301919091529151908190036023019020556103ff806100986000396000f30060806040526004361061004b5763ffffffff7c0100000000000000000000000000000000000000000000000000000000600035041663baa40e5c8114610050578063fc36e15b146100bb575b600080fd5b34801561005c57600080fd5b506040805160206004803580820135601f81018490048402850184019095528484526100a99436949293602493928401919081908401838280828437509497506101169650505050505050565b60408051918252519081900360200190f35b3480156100c757600080fd5b506040805160206004803580820135601f81018490048402850184019095528484526101149436949293602493928401919081908401838280828437509497506102149650505050505050565b005b60008082511115156101ae57604080517f08c379a0000000000000000000000000000000000000000000000000000000008152602060048201526024808201527f42616420726571756573743a20276e616d6527206973206e756c6c206f72206560448201527f6d70747900000000000000000000000000000000000000000000000000000000606482015290519081900360840190fd5b6001826040518082805190602001908083835b602083106101e05780518252601f1990920191602091820191016101c1565b51815160209384036101000a6000190180199092169116179052920194855250604051938490030190922054949350505050565b80516000106102a957604080517f08c379a0000000000000000000000000000000000000000000000000000000008152602060048201526024808201527f42616420726571756573743a20276e616d6527206973206e756c6c206f72206560448201527f6d70747900000000000000000000000000000000000000000000000000000000606482015290519081900360840190fd5b3360009081526020819052604090205460ff161561034e57604080517f08c379a000000000000000000000000000000000000000000000000000000000815260206004820152602b60248201527f42616420726571756573743a207468697320616464726573732068617320616c60448201527f726561647920766f746564000000000000000000000000000000000000000000606482015290519081900360840190fd5b3360009081526020818152604091829020805460ff191660019081179091559151835184928291908401908083835b6020831061039c5780518252601f19909201916020918201910161037d565b51815160209384036101000a60001901801990921691161790529201948552506040519384900301909220805460010190555050505600a165627a7a723058206a34f0b0495a74a9d7d7fb9dc640ed1fa353be77cd07f9e0f4db913b27f1481b0029";

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

        public ElectionContractService(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public async Task<TransactionReceipt> ExecuteTransactionAsync(Func<IElectionContractService, Task<string>> func, int timeoutInSeconds)
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

        public Function GetFunctionGetVote()
        {
            return _contract.GetFunction("getVote");
        }

        public Function GetFunctionVote()
        {
            return _contract.GetFunction("vote");
        }



        public Task<BigInteger> GetVoteCallAsync(string name)
        {
            var function = GetFunctionGetVote();
            return function.CallAsync<BigInteger>(name);
        }

        public async Task<BigInteger> GetVoteCallAsync(string addressFrom, string name, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetVote();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, name).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount, name).ConfigureAwait(false);
        }


        public async Task<string> VoteAsync(string addressFrom, string name, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionVote();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, name).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, name).ConfigureAwait(false);
        }




    }



}
