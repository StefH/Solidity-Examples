using System;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Geth;
using Newtonsoft.Json;

namespace ConsoleApp
{
    class Program
    {
        private static readonly ManagedAccount ContractOwner = new ManagedAccount("0x12890d2cce102216644c59daE5baed380d84830c", "password");
        private static readonly ManagedAccount Supplier1 = new ManagedAccount("0xaf7c2c210d31245cf332fc1d820bdfd77d1e3555", "password");
        private static readonly ManagedAccount Supplier2 = new ManagedAccount("0x72bC90BcEE4F850E5f746dCcf8AfC48faFf037b2", "password");
        private static readonly ManagedAccount Manufacturer1 = new ManagedAccount("0x13f022d72158410433cbd66f5dd8bf6d2d129924", "password");
        private static readonly ManagedAccount Manufacturer2 = new ManagedAccount("0x6bA8D4304EfaE5Edc7942762aF0029FF514F8CEb", "password");

        private static string _contractAddress;

        /// <summary>
        /// Simple ConsoleApp to connect to your local Ethereum to deploy a contract and run functions and transactions. This project depends on the Solidity project.
        /// So make sure to run `npm run build`.
        /// 
        /// Also make sure to start `\SupplyChainLog\Blockchain\testchain.bat`.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Blockchain - Ethereum - SupplyChainLog - ConsoleApp");

            Console.WriteLine(new string('-', 80));

            TestService().Wait(60000);

            Console.WriteLine(new string('_', 80));
        }

        private static uint ToEpoch(DateTime date)
        {
            return (uint)(date - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private static async Task TestService()
        {
            var web3 = new Web3Geth(ContractOwner);

            var gasForDeployContract = new HexBigInteger(15000000);
            Console.WriteLine("Deploying Contract...");
            _contractAddress = await SupplyChainLogContractService.DeployContractAsync(web3, ContractOwner.Address, null, gasForDeployContract);
            Console.WriteLine("Deploying Contract done");


            ISupplyChainLogContractService service = new SupplyChainLogContractService(web3, _contractAddress);


            var closeResult = await service.CloseAsync(ContractOwner.Address, new HexBigInteger(900000));
            Console.WriteLine($"closeResult = {closeResult}");
        }
    }
}