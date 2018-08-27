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
    public class MultiplyContractService : IMultiplyContractService
    {
        public static string ABI = @"[{""constant"":true,""inputs"":[{""name"":""index"",""type"":""uint256""}],""name"":""getOrderIdAtIndex"",""outputs"":[{""name"":""orderId"",""type"":""bytes16""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""countOrders"",""outputs"":[{""name"":""count"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""val"",""type"":""int256""}],""name"":""multiply"",""outputs"":[{""name"":""result"",""type"":""int256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""orderId"",""type"":""bytes16""}],""name"":""getOrderById"",""outputs"":[{""name"":""id"",""type"":""bytes16""},{""name"":""name"",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""name"",""type"":""string""}],""name"":""addOrder"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""close"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""id"",""type"":""bytes16""}],""name"":""isExistingOrder"",""outputs"":[{""name"":""existing"",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""val1"",""type"":""int256""},{""name"":""val2"",""type"":""int256""}],""name"":""add"",""outputs"":[{""name"":""result"",""type"":""int256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""val"",""type"":""int256""}],""name"":""storeValue"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""name"":""_multiplier"",""type"":""int256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""from"",""type"":""address""},{""indexed"":true,""name"":""value1"",""type"":""int256""},{""indexed"":true,""name"":""value2"",""type"":""int256""}],""name"":""AddEvent"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""from"",""type"":""address""},{""indexed"":true,""name"":""value"",""type"":""int256""}],""name"":""StoreValueEvent"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""from"",""type"":""address""},{""indexed"":true,""name"":""id"",""type"":""bytes16""},{""indexed"":false,""name"":""name"",""type"":""string""}],""name"":""AddOrderEvent"",""type"":""event""}]";

        public static string ByteCode = "0x6060604052341561000f57600080fd5b6040516020806108cf8339810160405280805160008054600160a060020a033316600160a060020a0319909116179055600155505061087c806100536000396000f3006060604052600436106100985763ffffffff7c0100000000000000000000000000000000000000000000000000000000600035041663024303b1811461009d5780630d73c6de146100d95780631df4f144146100fe5780631fc5a75a146101145780632cca04df146101ce57806343d726d6146102215780634b506d5c14610234578063a5f3c23b14610271578063f2a4bb531461028a575b600080fd5b34156100a857600080fd5b6100b36004356102a0565b6040516fffffffffffffffffffffffffffffffff19909116815260200160405180910390f35b34156100e457600080fd5b6100ec6102eb565b60405190815260200160405180910390f35b341561010957600080fd5b6100ec6004356102f2565b341561011f57600080fd5b61013d6fffffffffffffffffffffffffffffffff19600435166102f9565b6040516fffffffffffffffffffffffffffffffff198316815260406020820181815290820183818151815260200191508051906020019080838360005b8381101561019257808201518382015260200161017a565b50505050905090810190601f1680156101bf5780820380516001836020036101000a031916815260200191505b50935050505060405180910390f35b34156101d957600080fd5b61021f60046024813581810190830135806020601f8201819004810201604051908101604052818152929190602084018383808284375094965061041095505050505050565b005b341561022c57600080fd5b61021f610608565b341561023f57600080fd5b61025d6fffffffffffffffffffffffffffffffff196004351661064b565b604051901515815260200160405180910390f35b341561027c57600080fd5b6100ec6004356024356106d5565b341561029557600080fd5b61021f600435610721565b6000808210156102af57600080fd5b60048054839081106102bd57fe5b90600052602060002090600291828204019190066010029054906101000a9004608060020a0290505b919050565b6004545b90565b6001540290565b600061030361076d565b600061030e8461064b565b151561031657fe5b60036000856fffffffffffffffffffffffffffffffff19166fffffffffffffffffffffffffffffffff1916815260200190815260200160002090508060000160009054906101000a9004608060020a0281600101808054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103ff5780601f106103d4576101008083540402835291602001916103ff565b820191906000526020600020905b8154815290600101906020018083116103e257829003601f168201915b505050505090509250925050915091565b60008060008084511161042257600080fd5b60045484426040518084815260200183805190602001908083835b6020831061045c5780518252601f19909201916020918201910161043d565b6001836020036101000a0380198251168184511617909252505050919091019283525050602001915060409050519081900390206fffffffffffffffffffffffffffffffff1980821660009081526003602052604090208054909116608060020a83041781559093508392509050600181018480516104df92916020019061077f565b506004805460028301819055600181016104f983826107fd565b916000526020600020906002918282040191900660100284909190916101000a8154816fffffffffffffffffffffffffffffffff0219169083608060020a9004021790555050816fffffffffffffffffffffffffffffffff19163373ffffffffffffffffffffffffffffffffffffffff167fca538c4b8d21e6ce75d513ef46eba5db55e1185324f96231315f207e77dddcf28660405160208082528190810183818151815260200191508051906020019080838360005b838110156105c85780820151838201526020016105b0565b50505050905090810190601f1680156105f55780820380516001836020036101000a031916815260200191505b509250505060405180910390a350505050565b6000543373ffffffffffffffffffffffffffffffffffffffff90811691161461063057600080fd5b60005473ffffffffffffffffffffffffffffffffffffffff16ff5b600454600090151561065f575060006102e6565b6fffffffffffffffffffffffffffffffff19821660008181526003602052604090206002015460048054909190811061069457fe5b90600052602060002090600291828204019190066010029054906101000a9004608060020a026fffffffffffffffffffffffffffffffff1916149050919050565b600081833373ffffffffffffffffffffffffffffffffffffffff167fc23966d0a2948f73a1c4a577849824fa93acb0b93e06c6b43b5c0187ac04c30060405160405180910390a4500190565b60028190558073ffffffffffffffffffffffffffffffffffffffff33167f2e5ae076202f48276f8650b870f0b037887556558ad3f43d169715af82ef6eb260405160405180910390a350565b60206040519081016040526000815290565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106107c057805160ff19168380011785556107ed565b828001600101855582156107ed579182015b828111156107ed5782518255916020019190600101906107d2565b506107f9929150610836565b5090565b8154818355818115116108315760010160029004816001016002900483600052602060002091820191016108319190610836565b505050565b6102ef91905b808211156107f9576000815560010161083c5600a165627a7a723058202e031d80aa879c6e82780c7dce8defe8a6b7c5a6cc953b0ebb3b5928f3889c590029";

        public static async Task<string> DeployContractAsync(Web3 web3, string addressFrom, BigInteger _multiplier, CancellationTokenSource token = null, HexBigInteger gas = null)
        {
            if (gas == null)
            {
                BigInteger estimatedGas = await web3.Eth.DeployContract.EstimateGasAsync(ABI, ByteCode, addressFrom).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            var transactionReceipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(ABI, ByteCode, addressFrom, gas, token, _multiplier).ConfigureAwait(false);
            return transactionReceipt.ContractAddress;
        }

        private readonly Web3 _web3;
        private readonly Contract _contract;

        public MultiplyContractService(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public async Task<TransactionReceipt> ExecuteTransactionAsync(Func<IMultiplyContractService, Task<string>> func, int timeoutInSeconds)
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

        public Function GetFunctionGetOrderIdAtIndex()
        {
            return _contract.GetFunction("getOrderIdAtIndex");
        }

        public Function GetFunctionCountOrders()
        {
            return _contract.GetFunction("countOrders");
        }

        public Function GetFunctionMultiply()
        {
            return _contract.GetFunction("multiply");
        }

        public Function GetFunctionGetOrderById()
        {
            return _contract.GetFunction("getOrderById");
        }

        public Function GetFunctionAddOrder()
        {
            return _contract.GetFunction("addOrder");
        }

        public Function GetFunctionClose()
        {
            return _contract.GetFunction("close");
        }

        public Function GetFunctionIsExistingOrder()
        {
            return _contract.GetFunction("isExistingOrder");
        }

        public Function GetFunctionAdd()
        {
            return _contract.GetFunction("add");
        }

        public Function GetFunctionStoreValue()
        {
            return _contract.GetFunction("storeValue");
        }


        public Event GetAddEvent()
        {
            return _contract.GetEvent("AddEvent");
        }

        public Event GetStoreValueEvent()
        {
            return _contract.GetEvent("StoreValueEvent");
        }

        public Event GetAddOrderEvent()
        {
            return _contract.GetEvent("AddOrderEvent");
        }


        public Task<byte[]> GetOrderIdAtIndexCallAsync(BigInteger index)
        {
            var function = GetFunctionGetOrderIdAtIndex();
            return function.CallAsync<byte[]>(index);
        }

        public async Task<byte[]> GetOrderIdAtIndexCallAsync(string addressFrom, BigInteger index, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetOrderIdAtIndex();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, index).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<byte[]>(addressFrom, gas, valueAmount, index).ConfigureAwait(false);
        }

        public Task<BigInteger> CountOrdersCallAsync()
        {
            var function = GetFunctionCountOrders();
            return function.CallAsync<BigInteger>();
        }

        public async Task<BigInteger> CountOrdersCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionCountOrders();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public Task<BigInteger> MultiplyCallAsync(BigInteger val)
        {
            var function = GetFunctionMultiply();
            return function.CallAsync<BigInteger>(val);
        }

        public async Task<BigInteger> MultiplyCallAsync(string addressFrom, BigInteger val, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionMultiply();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, val).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount, val).ConfigureAwait(false);
        }

        public Task<bool> IsExistingOrderCallAsync(byte[] id)
        {
            var function = GetFunctionIsExistingOrder();
            return function.CallAsync<bool>(id);
        }

        public async Task<bool> IsExistingOrderCallAsync(string addressFrom, byte[] id, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionIsExistingOrder();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, id).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<bool>(addressFrom, gas, valueAmount, id).ConfigureAwait(false);
        }

        public Task<BigInteger> AddCallAsync(BigInteger val1, BigInteger val2)
        {
            var function = GetFunctionAdd();
            return function.CallAsync<BigInteger>(val1, val2);
        }

        public async Task<BigInteger> AddCallAsync(string addressFrom, BigInteger val1, BigInteger val2, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAdd();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, val1, val2).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount, val1, val2).ConfigureAwait(false);
        }


        public async Task<string> AddOrderAsync(string addressFrom, string name, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAddOrder();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, name).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, name).ConfigureAwait(false);
        }

        public async Task<string> CloseAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionClose();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public async Task<string> AddAsync(string addressFrom, BigInteger val1, BigInteger val2, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAdd();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, val1, val2).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, val1, val2).ConfigureAwait(false);
        }

        public async Task<string> StoreValueAsync(string addressFrom, BigInteger val, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionStoreValue();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, val).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, val).ConfigureAwait(false);
        }


        public Task<GetOrderById> GetOrderByIdCallAsync(byte[] orderId)
        {
            var function = GetFunctionGetOrderById();
            return function.CallDeserializingToObjectAsync<GetOrderById>(orderId);
        }

        public async Task<GetOrderById> GetOrderByIdCallAsync(string addressFrom, byte[] orderId,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetOrderById();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, orderId).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallDeserializingToObjectAsync<GetOrderById>(addressFrom, gas, valueAmount, orderId).ConfigureAwait(false);
        }



    }

    [FunctionOutput]
    public class GetOrderById
    {
        [Parameter("bytes16", "id", 1)]
        public byte[] Id { get; set; }

        [Parameter("string", "name", 2)]
        public string Name { get; set; }

    }


    public class AddEvent
    {
        [Parameter("address", "from", 1, true)]
        public string From { get; set; }

        [Parameter("int256", "value1", 2, true)]
        public BigInteger Value1 { get; set; }

        [Parameter("int256", "value2", 3, true)]
        public BigInteger Value2 { get; set; }

    }

    public class StoreValueEvent
    {
        [Parameter("address", "from", 1, true)]
        public string From { get; set; }

        [Parameter("int256", "value", 2, true)]
        public BigInteger Value { get; set; }

    }

    public class AddOrderEvent
    {
        [Parameter("address", "from", 1, true)]
        public string From { get; set; }

        [Parameter("bytes16", "id", 2, true)]
        public byte[] Id { get; set; }

        [Parameter("string", "name", 3, false)]
        public string Name { get; set; }

    }


}
