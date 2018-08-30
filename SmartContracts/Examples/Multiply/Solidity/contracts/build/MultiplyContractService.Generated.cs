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

        public static string ByteCode = "0x608060405234801561001057600080fd5b50604051602080610a21833981016040525160008054600160a060020a031916331790556001556109db806100466000396000f3006080604052600436106100985763ffffffff7c0100000000000000000000000000000000000000000000000000000000600035041663024303b1811461009d5780630d73c6de146100db5780631df4f144146101025780631fc5a75a1461011a5780632cca04df146101d757806343d726d6146102325780634b506d5c14610247578063a5f3c23b14610286578063f2a4bb53146102a1575b600080fd5b3480156100a957600080fd5b506100b56004356102b9565b604080516fffffffffffffffffffffffffffffffff199092168252519081900360200190f35b3480156100e757600080fd5b506100f0610399565b60408051918252519081900360200190f35b34801561010e57600080fd5b506100f06004356103a0565b34801561012657600080fd5b506101456fffffffffffffffffffffffffffffffff19600435166103a7565b604080516fffffffffffffffffffffffffffffffff198416815260208082018381528451938301939093528351919291606084019185019080838360005b8381101561019b578181015183820152602001610183565b50505050905090810190601f1680156101c85780820380516001836020036101000a031916815260200191505b50935050505060405180910390f35b3480156101e357600080fd5b506040805160206004803580820135601f81018490048402850184019095528484526102309436949293602493928401919081908401838280828437509497506104999650505050505050565b005b34801561023e57600080fd5b50610230610774565b34801561025357600080fd5b506102726fffffffffffffffffffffffffffffffff1960043516610815565b604080519115158252519081900360200190f35b34801561029257600080fd5b506100f06004356024356108ac565b3480156102ad57600080fd5b506102306004356108e2565b60008082101561035057604080517f08c379a000000000000000000000000000000000000000000000000000000000815260206004820152602360248201527f42616420526571756573743a2027696e646578272073686f756c64206265203e60448201527f3d20300000000000000000000000000000000000000000000000000000000000606482015290519081900360840190fd5b600480548390811061035e57fe5b90600052602060002090600291828204019190066010029054906101000a90047001000000000000000000000000000000000290505b919050565b6004545b90565b6001540290565b6000606060006103b684610815565b15156103be57fe5b506fffffffffffffffffffffffffffffffff1983166000908152600360209081526040918290208054600180830180548651600261010094831615949094026000190190911692909204601f8101869004860283018601909652858252929470010000000000000000000000000000000090920293909183918301828280156104885780601f1061045d57610100808354040283529160200191610488565b820191906000526020600020905b81548152906001019060200180831161046b57829003601f168201915b505050505090509250925050915091565b6000806000806000855111151561051157604080517f08c379a000000000000000000000000000000000000000000000000000000000815260206004820152601c60248201527f42616420526571756573743a20276e616d652720697320656d70747900000000604482015290519081900360640190fd5b42935060048054905085856040516020018084815260200183805190602001908083835b602083106105545780518252601f199092019160209182019101610535565b51815160209384036101000a60001901801990921691161790529201938452506040805180850381529382019081905283519395509350839290850191508083835b602083106105b55780518252601f199092019160209182019101610596565b51815160209384036101000a600019018019909216911617905260408051929094018290039091206fffffffffffffffffffffffffffffffff1980821660009081526003845294909420805490941670010000000000000000000000000000000082041784558a5190985088975092955061063b94506001860193508901919050610917565b506004805460028381018290556001808301845560009384527f8a35acfbc15ff81a39ae7d344fd709f28e8600b4aa8c65c6b64bfe7fe36bd19b9183049190910180546fffffffffffffffffffffffffffffffff929093166010026101000a9182021990921670010000000000000000000000000000000086049190910217905560408051602080825288518183015288516fffffffffffffffffffffffffffffffff1987169433947fca538c4b8d21e6ce75d513ef46eba5db55e1185324f96231315f207e77dddcf2948c949093849392840192918601918190849084905b8381101561073357818101518382015260200161071b565b50505050905090810190601f1680156107605780820380516001836020036101000a031916815260200191505b509250505060405180910390a35050505050565b60005473ffffffffffffffffffffffffffffffffffffffff1633146107fa57604080517f08c379a000000000000000000000000000000000000000000000000000000000815260206004820152600d60248201527f496e76616c6964206f776e657200000000000000000000000000000000000000604482015290519081900360640190fd5b60005473ffffffffffffffffffffffffffffffffffffffff16ff5b600454600090151561082957506000610394565b6fffffffffffffffffffffffffffffffff19821660008181526003602052604090206002015460048054909190811061085e57fe5b90600052602060002090600291828204019190066010029054906101000a9004700100000000000000000000000000000000026fffffffffffffffffffffffffffffffff1916149050919050565b6040516000908290849033907fc23966d0a2948f73a1c4a577849824fa93acb0b93e06c6b43b5c0187ac04c300908590a4500190565b6002819055604051819033907f2e5ae076202f48276f8650b870f0b037887556558ad3f43d169715af82ef6eb290600090a350565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061095857805160ff1916838001178555610985565b82800160010185558215610985579182015b8281111561098557825182559160200191906001019061096a565b50610991929150610995565b5090565b61039d91905b80821115610991576000815560010161099b5600a165627a7a723058208040fa89cb1bb1a0259c078942773aa76fcae7c28978ba10340e05dffded80c00029";

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
