using System;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;

namespace ConsoleApp
{
   public class MultiplyContractService : IMultiplyContractService
   {
        public static string ABI = @"[{""constant"":true,""inputs"":[{""name"":""val"",""type"":""int256""}],""name"":""multiply"",""outputs"":[{""name"":""result"",""type"":""int256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""close"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""val1"",""type"":""int256""},{""name"":""val2"",""type"":""int256""}],""name"":""add"",""outputs"":[{""name"":""result"",""type"":""int256""}],""payable"":false,""stateMutability"":""pure"",""type"":""function""},{""inputs"":[{""name"":""_multiplier"",""type"":""int256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""}]";

        public static string ByteCode = "0x6060604052341561000f57600080fd5b6040516020806101708339810160405280805160008054600160a060020a033316600160a060020a0319909116179055600155505061011d806100536000396000f30060606040526004361060525763ffffffff7c01000000000000000000000000000000000000000000000000000000006000350416631df4f1448114605757806343d726d614607c578063a5f3c23b14608e575b600080fd5b3415606157600080fd5b606a60043560a4565b60405190815260200160405180910390f35b3415608657600080fd5b608c60ab565b005b3415609857600080fd5b606a60043560243560ed565b6001540290565b6000543373ffffffffffffffffffffffffffffffffffffffff90811691161460d257600080fd5b60005473ffffffffffffffffffffffffffffffffffffffff16ff5b01905600a165627a7a723058200996547f3dcfd74e60d2c3696af5a95aabb567f5633c46ad26ca15a12d161a110029";

        public static Task<string> DeployContractAsync(Web3 web3, string addressFrom, BigInteger _multiplier, CancellationTokenSource token = null, HexBigInteger gas = null)
        {
            return web3.Eth.DeployContract
                .SendRequestAndWaitForReceiptAsync(ABI, ByteCode, addressFrom, gas, token, _multiplier)
                .ContinueWith((t) => t.Result.ContractAddress);
        }

        private readonly Web3 _web3;
        private readonly Contract _contract;

        public MultiplyContractService(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionMultiply() {
            return _contract.GetFunction("multiply");
        }
        public Function GetFunctionClose() {
            return _contract.GetFunction("close");
        }
        public Function GetFunctionAdd() {
            return _contract.GetFunction("add");
        }


        public Task<BigInteger> MultiplyAsync(BigInteger val) {
            var function = GetFunctionMultiply();
            return function.CallAsync<BigInteger>(val);
        }
        public Task<BigInteger> AddAsync(BigInteger val1, BigInteger val2) {
            var function = GetFunctionAdd();
            return function.CallAsync<BigInteger>(val1, val2);
        }

        public Task<string> CloseAsync(string addressFrom, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionClose();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount);
        }



    }



}
