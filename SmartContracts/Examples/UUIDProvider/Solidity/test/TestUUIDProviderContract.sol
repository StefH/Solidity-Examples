pragma solidity ^0.4.2;

// Next 2 lines show import error, but you can ignore this.
import "truffle/Assert.sol"; // See https://github.com/trufflesuite/truffle/blob/beta/lib/testing/Assert.sol
import "truffle/DeployedAddresses.sol";
import "../contracts/UUIDProviderContract.sol";

contract TestUUIDProviderContract {

    function testConstructAndGenerate() public {
        UUIDProviderContract c = new UUIDProviderContract();

        var result = c.generateUUID4();
        Assert.isTrue(result > 0, "uuid should be generated");
    }

}
