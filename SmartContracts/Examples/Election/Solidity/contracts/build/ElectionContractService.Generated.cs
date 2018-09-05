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
        public static string ABI = @"[{""constant"":false,""inputs"":[{""name"":""i"",""type"":""int256""}],""name"":""recursion"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""name"",""type"":""string""}],""name"":""getVote"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""name"",""type"":""string""}],""name"":""vote"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""}]";

        public static string ByteCode = "0x608060405234801561001057600080fd5b50604080517f416c696365000000000000000000000000000000000000000000000000000000815260016005820181905282519182900360250182206000908190557f426f6200000000000000000000000000000000000000000000000000000000008352600383019190915291519081900360230190205561042e806100986000396000f3006080604052600436106100565763ffffffff7c01000000000000000000000000000000000000000000000000000000006000350416632fdc74cc811461005b578063baa40e5c14610075578063fc36e15b146100e0575b600080fd5b34801561006757600080fd5b50610073600435610139565b005b34801561008157600080fd5b506040805160206004803580820135601f81018490048402850184019095528484526100ce9436949293602493928401919081908401838280828437509497506101459650505050505050565b60408051918252519081900360200190f35b3480156100ec57600080fd5b506040805160206004803580820135601f81018490048402850184019095528484526100739436949293602493928401919081908401838280828437509497506102439650505050505050565b61014281610139565b50565b60008082511115156101dd57604080517f08c379a0000000000000000000000000000000000000000000000000000000008152602060048201526024808201527f42616420726571756573743a20276e616d6527206973206e756c6c206f72206560448201527f6d70747900000000000000000000000000000000000000000000000000000000606482015290519081900360840190fd5b6001826040518082805190602001908083835b6020831061020f5780518252601f1990920191602091820191016101f0565b51815160209384036101000a6000190180199092169116179052920194855250604051938490030190922054949350505050565b80516000106102d857604080517f08c379a0000000000000000000000000000000000000000000000000000000008152602060048201526024808201527f42616420726571756573743a20276e616d6527206973206e756c6c206f72206560448201527f6d70747900000000000000000000000000000000000000000000000000000000606482015290519081900360840190fd5b3360009081526020819052604090205460ff161561037d57604080517f08c379a000000000000000000000000000000000000000000000000000000000815260206004820152602b60248201527f42616420726571756573743a207468697320616464726573732068617320616c60448201527f726561647920766f746564000000000000000000000000000000000000000000606482015290519081900360840190fd5b3360009081526020818152604091829020805460ff191660019081179091559151835184928291908401908083835b602083106103cb5780518252601f1990920191602091820191016103ac565b51815160209384036101000a60001901801990921691161790529201948552506040519384900301909220805460010190555050505600a165627a7a7230582065536a98fc3e5c42d5e85b47bac0a6f4c632ea42c5b22e9c35df7e98c678bc340029";

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

        public Function GetFunctionRecursion()
        {
            return _contract.GetFunction("recursion");
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


        public async Task<string> RecursionAsync(string addressFrom, BigInteger i, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionRecursion();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, i).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, i).ConfigureAwait(false);
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
