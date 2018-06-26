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
    public class ArtifactsService : IArtifactsService
    {
        public static string ABI = @"[{""constant"":true,""inputs"":[],""name"":""cfoAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""ceoAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""creationFee"",""outputs"":[{""name"":"""",""type"":""uint8""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_newCEO"",""type"":""address""}],""name"":""setCEO"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_adminAddress"",""type"":""address""}],""name"":""unassignAdmin"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""payForArtifact"",""outputs"":[],""payable"":true,""stateMutability"":""payable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""gameserverAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_id"",""type"":""uint256""}],""name"":""getArtifactTypeId"",""outputs"":[{""name"":""_typeId"",""type"":""uint32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_adminAddress"",""type"":""address""}],""name"":""assignAdmin"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""unpause"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_newCFO"",""type"":""address""}],""name"":""setCFO"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""adminAddresses"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""paused"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""withdrawBalance"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""pause"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_typeId"",""type"":""uint32""},{""name"":""_owner"",""type"":""address""}],""name"":""createArtifact"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""newPrice"",""type"":""uint64""}],""name"":""changeCreationPrice"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""getArtifactsAmount"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_newGameserver"",""type"":""address""}],""name"":""setGameserver"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""ownershipTokenCount"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""uint256""}],""name"":""artifactIndexToApproved"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""uint256""}],""name"":""artifactIndexToOwner"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""from"",""type"":""address""},{""indexed"":true,""name"":""to"",""type"":""address""},{""indexed"":true,""name"":""tokenId"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""payer"",""type"":""address""}],""name"":""PayedCreation"",""type"":""event""}]";

        public static string ByteCode = "0x60806040526004805460ff19169055600a805467ffffffffffffffff1916660470de4df8200017905560008054600160a060020a031916331790556109fe806100496000396000f3006080604052600436106101275763ffffffff7c01000000000000000000000000000000000000000000000000000000006000350416630519ce79811461012c5780630a0f81681461015d57806311190f0c1461017257806327d7874c146101a95780632bc6e747146101cc57806331c680dd146101ed5780633645ebfa146101f55780633698a8b01461020a5780633da7d2ed1461023b5780633f4ba83a1461025c5780634e0a3379146102715780634ed38faf146102925780635c975abb146102c75780635fd8c710146102dc5780638456cb59146102f1578063ae5f5feb14610306578063b0ac8bf114610342578063b95558d914610364578063b981d00f14610379578063cec21acb1461039a578063f19c9a15146103bb578063fd245c71146103d3575b600080fd5b34801561013857600080fd5b506101416103eb565b60408051600160a060020a039092168252519081900360200190f35b34801561016957600080fd5b506101416103fa565b34801561017e57600080fd5b50610193600160a060020a0360043516610409565b6040805160ff9092168252519081900360200190f35b3480156101b557600080fd5b506101ca600160a060020a036004351661041e565b005b3480156101d857600080fd5b506101ca600160a060020a0360043516610479565b6101ca6104b1565b34801561020157600080fd5b50610141610542565b34801561021657600080fd5b50610222600435610551565b6040805163ffffffff9092168252519081900360200190f35b34801561024757600080fd5b506101ca600160a060020a036004351661058f565b34801561026857600080fd5b506101ca6105ca565b34801561027d57600080fd5b506101ca600160a060020a03600435166105fe565b34801561029e57600080fd5b506102b3600160a060020a0360043516610659565b604080519115158252519081900360200190f35b3480156102d357600080fd5b506102b361066e565b3480156102e857600080fd5b506101ca610677565b3480156102fd57600080fd5b506101ca6106cb565b34801561031257600080fd5b5061033063ffffffff60043516600160a060020a0360243516610701565b60408051918252519081900360200190f35b34801561034e57600080fd5b506101ca67ffffffffffffffff600435166107f7565b34801561037057600080fd5b50610330610832565b34801561038557600080fd5b506101ca600160a060020a0360043516610838565b3480156103a657600080fd5b50610330600160a060020a0360043516610893565b3480156103c757600080fd5b506101416004356108a5565b3480156103df57600080fd5b506101416004356108c0565b600154600160a060020a031681565b600054600160a060020a031681565b60096020526000908152604090205460ff1681565b600054600160a060020a0316331461043557600080fd5b600160a060020a038116151561044a57600080fd5b6000805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b600054600160a060020a0316331461049057600080fd5b600160a060020a03166000908152600360205260409020805460ff19169055565b600a5467ffffffffffffffff1634146104c957600080fd5b3360009081526009602052604090205460ff908116106104e857600080fd5b33600081815260096020908152604091829020805460ff8082166001011660ff19909116179055815192835290517f54e469b8aac5bc31d1c7a70c1a3d5781734ebd611d05d8d804952a7a97ad28499281900390910190a1565b600254600160a060020a031681565b600060058281548110151561056257fe5b90600052602060002090600891828204019190066004029054906101000a900463ffffffff169050919050565b600054600160a060020a031633146105a657600080fd5b600160a060020a03166000908152600360205260409020805460ff19166001179055565b600054600160a060020a031633146105e157600080fd5b60045460ff1615156105f257600080fd5b6004805460ff19169055565b600054600160a060020a0316331461061557600080fd5b600160a060020a038116151561062a57600080fd5b6001805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b60036020526000908152604090205460ff1681565b60045460ff1681565b600154600160a060020a0316331461068e57600080fd5b600154604051600160a060020a0390911690303180156108fc02916000818181858888f193505050501580156106c8573d6000803e3d6000fd5b50565b600054600160a060020a031633146106e257600080fd5b60045460ff16156106f257600080fd5b6004805460ff19166001179055565b6002546000908190600160a060020a0316331461071d57600080fd5b600160a060020a03831660009081526009602052604081205460ff161161074357600080fd5b50600580546001810182556000919091527f036b6384b5eca791c62761152d0c79bb0604c104a5fb6f4eb0703f3154bb3db060088204018054600783166004026101000a63ffffffff8181021990921687831691909102179091558111156107aa57600080fd5b6002546107c190600160a060020a031684836108db565b600160a060020a03929092166000908152600960205260409020805460ff19811660ff9182166000190190911617905550919050565b600054600160a060020a0316331461080e57600080fd5b600a805467ffffffffffffffff191667ffffffffffffffff92909216919091179055565b60055490565b600054600160a060020a0316331461084f57600080fd5b600160a060020a038116151561086457600080fd5b6002805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b60076020526000908152604090205481565b600860205260009081526040902054600160a060020a031681565b600660205260009081526040902054600160a060020a031681565b60045460ff16156108eb57600080fd5b600160a060020a0380831660008181526007602090815260408083208054600101905585835260069091529020805473ffffffffffffffffffffffffffffffffffffffff1916909117905560025484821691161461098c57600160a060020a0383166000908152600760209081526040808320805460001901905583835260089091529020805473ffffffffffffffffffffffffffffffffffffffff191690555b8082600160a060020a031684600160a060020a03167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef60405160405180910390a45050505600a165627a7a7230582080618bb45fb201042ebfaf3a81e5ad22e94829a7295c60c2d5349c010ec6d51d0029";

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

        public ArtifactsService(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public async Task<TransactionReceipt> ExecuteTransactionAsync(Func<IArtifactsService, Task<string>> func, int timeoutInSeconds)
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

        public Function GetFunctionCreationFee()
        {
            return _contract.GetFunction("creationFee");
        }

        public Function GetFunctionSetCEO()
        {
            return _contract.GetFunction("setCEO");
        }

        public Function GetFunctionUnassignAdmin()
        {
            return _contract.GetFunction("unassignAdmin");
        }

        public Function GetFunctionPayForArtifact()
        {
            return _contract.GetFunction("payForArtifact");
        }

        public Function GetFunctionGameserverAddress()
        {
            return _contract.GetFunction("gameserverAddress");
        }

        public Function GetFunctionGetArtifactTypeId()
        {
            return _contract.GetFunction("getArtifactTypeId");
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

        public Function GetFunctionCreateArtifact()
        {
            return _contract.GetFunction("createArtifact");
        }

        public Function GetFunctionChangeCreationPrice()
        {
            return _contract.GetFunction("changeCreationPrice");
        }

        public Function GetFunctionGetArtifactsAmount()
        {
            return _contract.GetFunction("getArtifactsAmount");
        }

        public Function GetFunctionSetGameserver()
        {
            return _contract.GetFunction("setGameserver");
        }

        public Function GetFunctionOwnershipTokenCount()
        {
            return _contract.GetFunction("ownershipTokenCount");
        }

        public Function GetFunctionArtifactIndexToApproved()
        {
            return _contract.GetFunction("artifactIndexToApproved");
        }

        public Function GetFunctionArtifactIndexToOwner()
        {
            return _contract.GetFunction("artifactIndexToOwner");
        }


        public Event GetTransfer()
        {
            return _contract.GetEvent("Transfer");
        }

        public Event GetPayedCreation()
        {
            return _contract.GetEvent("PayedCreation");
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

        public Task<byte> CreationFeeCallAsync(string a)
        {
            var function = GetFunctionCreationFee();
            return function.CallAsync<byte>(a);
        }

        public async Task<byte> CreationFeeCallAsync(string addressFrom, string a, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionCreationFee();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, a).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<byte>(addressFrom, gas, valueAmount, a).ConfigureAwait(false);
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

        public Task<int> GetArtifactTypeIdCallAsync(BigInteger _id)
        {
            var function = GetFunctionGetArtifactTypeId();
            return function.CallAsync<int>(_id);
        }

        public async Task<int> GetArtifactTypeIdCallAsync(string addressFrom, BigInteger _id, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetArtifactTypeId();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _id).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<int>(addressFrom, gas, valueAmount, _id).ConfigureAwait(false);
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

        public Task<BigInteger> CreateArtifactCallAsync(int _typeId, string _owner)
        {
            var function = GetFunctionCreateArtifact();
            return function.CallAsync<BigInteger>(_typeId, _owner);
        }

        public async Task<BigInteger> CreateArtifactCallAsync(string addressFrom, int _typeId, string _owner, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionCreateArtifact();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _typeId, _owner).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount, _typeId, _owner).ConfigureAwait(false);
        }

        public Task<BigInteger> GetArtifactsAmountCallAsync()
        {
            var function = GetFunctionGetArtifactsAmount();
            return function.CallAsync<BigInteger>();
        }

        public async Task<BigInteger> GetArtifactsAmountCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetArtifactsAmount();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public Task<BigInteger> OwnershipTokenCountCallAsync(string a)
        {
            var function = GetFunctionOwnershipTokenCount();
            return function.CallAsync<BigInteger>(a);
        }

        public async Task<BigInteger> OwnershipTokenCountCallAsync(string addressFrom, string a, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionOwnershipTokenCount();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, a).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount, a).ConfigureAwait(false);
        }

        public Task<string> ArtifactIndexToApprovedCallAsync(BigInteger a)
        {
            var function = GetFunctionArtifactIndexToApproved();
            return function.CallAsync<string>(a);
        }

        public async Task<string> ArtifactIndexToApprovedCallAsync(string addressFrom, BigInteger a, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionArtifactIndexToApproved();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, a).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<string>(addressFrom, gas, valueAmount, a).ConfigureAwait(false);
        }

        public Task<string> ArtifactIndexToOwnerCallAsync(BigInteger a)
        {
            var function = GetFunctionArtifactIndexToOwner();
            return function.CallAsync<string>(a);
        }

        public async Task<string> ArtifactIndexToOwnerCallAsync(string addressFrom, BigInteger a, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionArtifactIndexToOwner();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, a).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<string>(addressFrom, gas, valueAmount, a).ConfigureAwait(false);
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

        public async Task<string> PayForArtifactAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionPayForArtifact();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
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

        public async Task<string> CreateArtifactAsync(string addressFrom, int _typeId, string _owner, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionCreateArtifact();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, _typeId, _owner).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, _typeId, _owner).ConfigureAwait(false);
        }

        public async Task<string> ChangeCreationPriceAsync(string addressFrom, ulong newPrice, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionChangeCreationPrice();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, newPrice).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, newPrice).ConfigureAwait(false);
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


    public class Transfer
    {
        [Parameter("address", "from", 1, true)]
        public string From { get; set; }

        [Parameter("address", "to", 2, true)]
        public string To { get; set; }

        [Parameter("uint256", "tokenId", 3, true)]
        public BigInteger TokenId { get; set; }

    }


}
