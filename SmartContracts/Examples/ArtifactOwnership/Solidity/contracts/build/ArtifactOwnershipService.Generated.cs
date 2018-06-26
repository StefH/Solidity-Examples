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
    public class ArtifactOwnershipService : IArtifactOwnershipService
    {
        public static string ABI = @"[{""constant"":true,""inputs"":[],""name"":""cfoAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""name"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_tokenId"",""type"":""uint256""}],""name"":""approve"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""ceoAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""implementsERC721"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""pure"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""creationFee"",""outputs"":[{""name"":"""",""type"":""uint8""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_newCEO"",""type"":""address""}],""name"":""setCEO"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_adminAddress"",""type"":""address""}],""name"":""unassignAdmin"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""payForArtifact"",""outputs"":[],""payable"":true,""stateMutability"":""payable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""gameserverAddress"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_id"",""type"":""uint256""}],""name"":""getArtifactTypeId"",""outputs"":[{""name"":""_typeId"",""type"":""uint32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_adminAddress"",""type"":""address""}],""name"":""assignAdmin"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""unpause"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_newCFO"",""type"":""address""}],""name"":""setCFO"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""adminAddresses"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_from"",""type"":""address""},{""name"":""_to"",""type"":""address""},{""name"":""_tokenId"",""type"":""uint256""}],""name"":""takeOwnership"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""paused"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""withdrawBalance"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_tokenId"",""type"":""uint256""}],""name"":""ownerOf"",""outputs"":[{""name"":""owner"",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":""_owner"",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""name"":""count"",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""pause"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""symbol"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_tokenId"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_typeId"",""type"":""uint32""},{""name"":""_owner"",""type"":""address""}],""name"":""createArtifact"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""newPrice"",""type"":""uint64""}],""name"":""changeCreationPrice"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""getArtifactsAmount"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_newGameserver"",""type"":""address""}],""name"":""setGameserver"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""ownershipTokenCount"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""uint256""}],""name"":""artifactIndexToApproved"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""uint256""}],""name"":""artifactIndexToOwner"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""owner"",""type"":""address""},{""indexed"":true,""name"":""approved"",""type"":""address""},{""indexed"":true,""name"":""tokenId"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""from"",""type"":""address""},{""indexed"":true,""name"":""to"",""type"":""address""},{""indexed"":true,""name"":""tokenId"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""payer"",""type"":""address""}],""name"":""PayedCreation"",""type"":""event""}]";

        public static string ByteCode = "0x6004805460ff19169055600a805467ffffffffffffffff1916660470de4df8200017905560c0604052601060808190527f45746865726e616c41727469666163740000000000000000000000000000000060a090815261006291600b91906100bf565b506040805180820190915260028082527f454100000000000000000000000000000000000000000000000000000000000060209092019182526100a791600c916100bf565b5060008054600160a060020a0319163317905561015a565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061010057805160ff191683800117855561012d565b8280016001018555821561012d579182015b8281111561012d578251825591602001919060010190610112565b5061013992915061013d565b5090565b61015791905b808211156101395760008155600101610143565b90565b610e6b806101696000396000f30060806040526004361061018a5763ffffffff7c01000000000000000000000000000000000000000000000000000000006000350416630519ce79811461018f57806306fdde03146101c0578063095ea7b31461024a5780630a0f8168146102705780631051db341461028557806311190f0c146102ae57806318160ddd146102e557806327d7874c1461030c5780632bc6e7471461032d57806331c680dd1461034e5780633645ebfa146103565780633698a8b01461036b5780633da7d2ed1461039c5780633f4ba83a146103bd5780634e0a3379146103d25780634ed38faf146103f357806355e54232146104145780635c975abb1461043e5780635fd8c710146104535780636352211e1461046857806370a08231146104805780638456cb59146104a157806395d89b41146104b6578063a9059cbb146104cb578063ae5f5feb146104ef578063b0ac8bf114610519578063b95558d9146102e5578063b981d00f1461053b578063cec21acb1461055c578063f19c9a151461057d578063fd245c7114610595575b600080fd5b34801561019b57600080fd5b506101a46105ad565b60408051600160a060020a039092168252519081900360200190f35b3480156101cc57600080fd5b506101d56105bc565b6040805160208082528351818301528351919283929083019185019080838360005b8381101561020f5781810151838201526020016101f7565b50505050905090810190601f16801561023c5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34801561025657600080fd5b5061026e600160a060020a036004351660243561064a565b005b34801561027c57600080fd5b506101a46106b5565b34801561029157600080fd5b5061029a6106c4565b604080519115158252519081900360200190f35b3480156102ba57600080fd5b506102cf600160a060020a03600435166106c9565b6040805160ff9092168252519081900360200190f35b3480156102f157600080fd5b506102fa6106de565b60408051918252519081900360200190f35b34801561031857600080fd5b5061026e600160a060020a03600435166106e4565b34801561033957600080fd5b5061026e600160a060020a036004351661073f565b61026e610777565b34801561036257600080fd5b506101a4610808565b34801561037757600080fd5b50610383600435610817565b6040805163ffffffff9092168252519081900360200190f35b3480156103a857600080fd5b5061026e600160a060020a0360043516610855565b3480156103c957600080fd5b5061026e610890565b3480156103de57600080fd5b5061026e600160a060020a03600435166108c4565b3480156103ff57600080fd5b5061029a600160a060020a036004351661091f565b34801561042057600080fd5b5061026e600160a060020a0360043581169060243516604435610934565b34801561044a57600080fd5b5061029a61097e565b34801561045f57600080fd5b5061026e610987565b34801561047457600080fd5b506101a46004356109db565b34801561048c57600080fd5b506102fa600160a060020a0360043516610a04565b3480156104ad57600080fd5b5061026e610a1f565b3480156104c257600080fd5b506101d5610a55565b3480156104d757600080fd5b5061026e600160a060020a0360043516602435610ab0565b3480156104fb57600080fd5b506102fa63ffffffff60043516600160a060020a0360243516610af9565b34801561052557600080fd5b5061026e67ffffffffffffffff60043516610bef565b34801561054757600080fd5b5061026e600160a060020a0360043516610c2a565b34801561056857600080fd5b506102fa600160a060020a0360043516610c85565b34801561058957600080fd5b506101a4600435610c97565b3480156105a157600080fd5b506101a4600435610cb2565b600154600160a060020a031681565b600b805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156106425780601f1061061757610100808354040283529160200191610642565b820191906000526020600020905b81548152906001019060200180831161062557829003601f168201915b505050505081565b60045460ff161561065a57600080fd5b6106643382610ccd565b151561066f57600080fd5b6106798183610ced565b6040518190600160a060020a0384169033907f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b92590600090a45050565b600054600160a060020a031681565b600190565b60096020526000908152604090205460ff1681565b60055490565b600054600160a060020a031633146106fb57600080fd5b600160a060020a038116151561071057600080fd5b6000805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b600054600160a060020a0316331461075657600080fd5b600160a060020a03166000908152600360205260409020805460ff19169055565b600a5467ffffffffffffffff16341461078f57600080fd5b3360009081526009602052604090205460ff908116106107ae57600080fd5b33600081815260096020908152604091829020805460ff8082166001011660ff19909116179055815192835290517f54e469b8aac5bc31d1c7a70c1a3d5781734ebd611d05d8d804952a7a97ad28499281900390910190a1565b600254600160a060020a031681565b600060058281548110151561082857fe5b90600052602060002090600891828204019190066004029054906101000a900463ffffffff169050919050565b600054600160a060020a0316331461086c57600080fd5b600160a060020a03166000908152600360205260409020805460ff19166001179055565b600054600160a060020a031633146108a757600080fd5b60045460ff1615156108b857600080fd5b6004805460ff19169055565b600054600160a060020a031633146108db57600080fd5b600160a060020a03811615156108f057600080fd5b6001805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b60036020526000908152604090205460ff1681565b60045460ff161561094457600080fd5b61094e3382610d28565b151561095957600080fd5b6109638382610ccd565b151561096e57600080fd5b610979838383610d48565b505050565b60045460ff1681565b600154600160a060020a0316331461099e57600080fd5b600154604051600160a060020a0390911690303180156108fc02916000818181858888f193505050501580156109d8573d6000803e3d6000fd5b50565b600081815260066020526040902054600160a060020a03168015156109ff57600080fd5b919050565b600160a060020a031660009081526007602052604090205490565b600054600160a060020a03163314610a3657600080fd5b60045460ff1615610a4657600080fd5b6004805460ff19166001179055565b600c805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156106425780601f1061061757610100808354040283529160200191610642565b60045460ff1615610ac057600080fd5b600160a060020a0382161515610ad557600080fd5b610adf3382610ccd565b1515610aea57600080fd5b610af5338383610d48565b5050565b6002546000908190600160a060020a03163314610b1557600080fd5b600160a060020a03831660009081526009602052604081205460ff1611610b3b57600080fd5b50600580546001810182556000919091527f036b6384b5eca791c62761152d0c79bb0604c104a5fb6f4eb0703f3154bb3db060088204018054600783166004026101000a63ffffffff818102199092168783169190910217909155811115610ba257600080fd5b600254610bb990600160a060020a03168483610d48565b600160a060020a03929092166000908152600960205260409020805460ff19811660ff9182166000190190911617905550919050565b600054600160a060020a03163314610c0657600080fd5b600a805467ffffffffffffffff191667ffffffffffffffff92909216919091179055565b600054600160a060020a03163314610c4157600080fd5b600160a060020a0381161515610c5657600080fd5b6002805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b60076020526000908152604090205481565b600860205260009081526040902054600160a060020a031681565b600660205260009081526040902054600160a060020a031681565b600090815260066020526040902054600160a060020a0391821691161490565b600091825260086020526040909120805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a03909216919091179055565b600090815260086020526040902054600160a060020a0391821691161490565b60045460ff1615610d5857600080fd5b600160a060020a0380831660008181526007602090815260408083208054600101905585835260069091529020805473ffffffffffffffffffffffffffffffffffffffff19169091179055600254848216911614610df957600160a060020a0383166000908152600760209081526040808320805460001901905583835260089091529020805473ffffffffffffffffffffffffffffffffffffffff191690555b8082600160a060020a031684600160a060020a03167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef60405160405180910390a45050505600a165627a7a72305820760818416bf88b0f544f0df87363f900e345a777ed2c37a9b2d0f2d646d161300029";

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

        public ArtifactOwnershipService(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public async Task<TransactionReceipt> ExecuteTransactionAsync(Func<IArtifactOwnershipService, Task<string>> func, int timeoutInSeconds)
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

        public Function GetFunctionName()
        {
            return _contract.GetFunction("name");
        }

        public Function GetFunctionApprove()
        {
            return _contract.GetFunction("approve");
        }

        public Function GetFunctionCeoAddress()
        {
            return _contract.GetFunction("ceoAddress");
        }

        public Function GetFunctionImplementsERC721()
        {
            return _contract.GetFunction("implementsERC721");
        }

        public Function GetFunctionCreationFee()
        {
            return _contract.GetFunction("creationFee");
        }

        public Function GetFunctionTotalSupply()
        {
            return _contract.GetFunction("totalSupply");
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

        public Function GetFunctionTakeOwnership()
        {
            return _contract.GetFunction("takeOwnership");
        }

        public Function GetFunctionPaused()
        {
            return _contract.GetFunction("paused");
        }

        public Function GetFunctionWithdrawBalance()
        {
            return _contract.GetFunction("withdrawBalance");
        }

        public Function GetFunctionOwnerOf()
        {
            return _contract.GetFunction("ownerOf");
        }

        public Function GetFunctionBalanceOf()
        {
            return _contract.GetFunction("balanceOf");
        }

        public Function GetFunctionPause()
        {
            return _contract.GetFunction("pause");
        }

        public Function GetFunctionSymbol()
        {
            return _contract.GetFunction("symbol");
        }

        public Function GetFunctionTransfer()
        {
            return _contract.GetFunction("transfer");
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


        public Event GetApproval()
        {
            return _contract.GetEvent("Approval");
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

        public Task<string> NameCallAsync()
        {
            var function = GetFunctionName();
            return function.CallAsync<string>();
        }

        public async Task<string> NameCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionName();

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

        public Task<string> SymbolCallAsync()
        {
            var function = GetFunctionSymbol();
            return function.CallAsync<string>();
        }

        public async Task<string> SymbolCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSymbol();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<string>(addressFrom, gas, valueAmount).ConfigureAwait(false);
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


    public class Approval
    {
        [Parameter("address", "owner", 1, true)]
        public string Owner { get; set; }

        [Parameter("address", "approved", 2, true)]
        public string Approved { get; set; }

        [Parameter("uint256", "tokenId", 3, true)]
        public BigInteger TokenId { get; set; }

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
