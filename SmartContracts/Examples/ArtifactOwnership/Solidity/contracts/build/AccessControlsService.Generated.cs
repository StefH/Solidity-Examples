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
    public class AccessControlsService : IAccessControlsService
    {
        public static string ABI = @"[{""constant"":true,""inputs"":[],""name"":""cfoAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""ceoAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_newCEO"",""type"":""address""}],""name"":""setCEO"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_adminAddress"",""type"":""address""}],""name"":""unassignAdmin"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""gameserverAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_adminAddress"",""type"":""address""}],""name"":""assignAdmin"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""unpause"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_newCFO"",""type"":""address""}],""name"":""setCFO"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""adminAddresses"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""paused"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""withdrawBalance"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""pause"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_newGameserver"",""type"":""address""}],""name"":""setGameserver"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""}]";

        public static string ByteCode = "0x60806040526004805460ff1916905534801561001a57600080fd5b5060008054600160a060020a0319163317905561050d8061003c6000396000f3006080604052600436106100c45763ffffffff7c01000000000000000000000000000000000000000000000000000000006000350416630519ce7981146100c95780630a0f8168146100fa57806327d7874c1461010f5780632bc6e747146101325780633645ebfa146101535780633da7d2ed146101685780633f4ba83a146101895780634e0a33791461019e5780634ed38faf146101bf5780635c975abb146101f45780635fd8c710146102095780638456cb591461021e578063b981d00f14610233575b600080fd5b3480156100d557600080fd5b506100de610254565b60408051600160a060020a039092168252519081900360200190f35b34801561010657600080fd5b506100de610263565b34801561011b57600080fd5b50610130600160a060020a0360043516610272565b005b34801561013e57600080fd5b50610130600160a060020a03600435166102cd565b34801561015f57600080fd5b506100de610305565b34801561017457600080fd5b50610130600160a060020a0360043516610314565b34801561019557600080fd5b5061013061034f565b3480156101aa57600080fd5b50610130600160a060020a0360043516610383565b3480156101cb57600080fd5b506101e0600160a060020a03600435166103de565b604080519115158252519081900360200190f35b34801561020057600080fd5b506101e06103f3565b34801561021557600080fd5b506101306103fc565b34801561022a57600080fd5b50610130610450565b34801561023f57600080fd5b50610130600160a060020a0360043516610486565b600154600160a060020a031681565b600054600160a060020a031681565b600054600160a060020a0316331461028957600080fd5b600160a060020a038116151561029e57600080fd5b6000805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b600054600160a060020a031633146102e457600080fd5b600160a060020a03166000908152600360205260409020805460ff19169055565b600254600160a060020a031681565b600054600160a060020a0316331461032b57600080fd5b600160a060020a03166000908152600360205260409020805460ff19166001179055565b600054600160a060020a0316331461036657600080fd5b60045460ff16151561037757600080fd5b6004805460ff19169055565b600054600160a060020a0316331461039a57600080fd5b600160a060020a03811615156103af57600080fd5b6001805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b60036020526000908152604090205460ff1681565b60045460ff1681565b600154600160a060020a0316331461041357600080fd5b600154604051600160a060020a0390911690303180156108fc02916000818181858888f1935050505015801561044d573d6000803e3d6000fd5b50565b600054600160a060020a0316331461046757600080fd5b60045460ff161561047757600080fd5b6004805460ff19166001179055565b600054600160a060020a0316331461049d57600080fd5b600160a060020a03811615156104b257600080fd5b6002805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a03929092169190911790555600a165627a7a7230582047697bdd4f1a6d8fc87aa382a28f716be45e99b9739416ec0b10dd3a50276bfc0029";

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

        public AccessControlsService(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public async Task<TransactionReceipt> ExecuteTransactionAsync(Func<IAccessControlsService, Task<string>> func, int timeoutInSeconds)
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

        public Function GetFunctionCfoAddress()
        {
            return _contract.GetFunction("cfoAddress");
        }

        public Function GetFunctionCeoAddress()
        {
            return _contract.GetFunction("ceoAddress");
        }

        public Function GetFunctionSetCEO()
        {
            return _contract.GetFunction("setCEO");
        }

        public Function GetFunctionUnassignAdmin()
        {
            return _contract.GetFunction("unassignAdmin");
        }

        public Function GetFunctionGameserverAddress()
        {
            return _contract.GetFunction("gameserverAddress");
        }

        public Function GetFunctionAssignAdmin()
        {
            return _contract.GetFunction("assignAdmin");
        }

        public Function GetFunctionUnpause()
        {
            return _contract.GetFunction("unpause");
        }

        public Function GetFunctionSetCFO()
        {
            return _contract.GetFunction("setCFO");
        }

        public Function GetFunctionAdminAddresses()
        {
            return _contract.GetFunction("adminAddresses");
        }

        public Function GetFunctionPaused()
        {
            return _contract.GetFunction("paused");
        }

        public Function GetFunctionWithdrawBalance()
        {
            return _contract.GetFunction("withdrawBalance");
        }

        public Function GetFunctionPause()
        {
            return _contract.GetFunction("pause");
        }

        public Function GetFunctionSetGameserver()
        {
            return _contract.GetFunction("setGameserver");
        }



        public Task<string> CfoAddressCallAsync()
        {
            var function = GetFunctionCfoAddress();
            return function.CallAsync<string>();
        }

        public async Task<string> CfoAddressCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionCfoAddress();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<string>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public Task<string> CeoAddressCallAsync()
        {
            var function = GetFunctionCeoAddress();
            return function.CallAsync<string>();
        }

        public async Task<string> CeoAddressCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionCeoAddress();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<string>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public Task<string> GameserverAddressCallAsync()
        {
            var function = GetFunctionGameserverAddress();
            return function.CallAsync<string>();
        }

        public async Task<string> GameserverAddressCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGameserverAddress();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<string>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public Task<bool> AdminAddressesCallAsync(string a)
        {
            var function = GetFunctionAdminAddresses();
            return function.CallAsync<bool>(a);
        }

        public async Task<bool> AdminAddressesCallAsync(string addressFrom, string a, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAdminAddresses();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, a).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<bool>(addressFrom, gas, valueAmount, a).ConfigureAwait(false);
        }

        public Task<bool> PausedCallAsync()
        {
            var function = GetFunctionPaused();
            return function.CallAsync<bool>();
        }

        public async Task<bool> PausedCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionPaused();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<bool>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }


        public async Task<string> SetCEOAsync(string addressFrom, string _newCEO, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSetCEO();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _newCEO).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, _newCEO).ConfigureAwait(false);
        }

        public async Task<string> UnassignAdminAsync(string addressFrom, string _adminAddress, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionUnassignAdmin();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _adminAddress).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, _adminAddress).ConfigureAwait(false);
        }

        public async Task<string> AssignAdminAsync(string addressFrom, string _adminAddress, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionAssignAdmin();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _adminAddress).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, _adminAddress).ConfigureAwait(false);
        }

        public async Task<string> UnpauseAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionUnpause();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public async Task<string> SetCFOAsync(string addressFrom, string _newCFO, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSetCFO();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _newCFO).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, _newCFO).ConfigureAwait(false);
        }

        public async Task<string> WithdrawBalanceAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionWithdrawBalance();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public async Task<string> PauseAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionPause();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public async Task<string> SetGameserverAsync(string addressFrom, string _newGameserver, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSetGameserver();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _newGameserver).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, _newGameserver).ConfigureAwait(false);
        }




    }



}
