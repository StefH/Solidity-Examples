using System;
using System.Threading.Tasks;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Geth;

namespace ConsoleApp
{
    class Program
    {
        private const string Endpoint = "http://localhost:7545";

        // Note that the password does not matter when connecting to local Ganache
        private static readonly ManagedAccount Account = new ManagedAccount("0xe2e6a2994D00BDEcd1A4280EE9859793d950e16A", "test");

        public static void Main(string[] args)
        {
            Console.WriteLine("Blockchain - Ganache - ConsoleApp");

            Console.WriteLine(new string('_', 80));

            TestService().Wait(30000);

            Console.WriteLine(new string('_', 80));
        }

        private static async Task TestService()
        {
            var web3 = new Web3Geth(Account, Endpoint);

            Console.WriteLine("DeployContractAsync");
            string contractAddress = await ArtifactOwnershipService.DeployContractAsync(web3, Account.Address);

            IArtifactOwnershipService service = new ArtifactOwnershipService(web3, contractAddress);

            Console.WriteLine("Call");
            var result = await service.TotalSupplyCallAsync();
            Console.WriteLine("result = `" + result + "`");
        }
    }
}