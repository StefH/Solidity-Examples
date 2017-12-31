pragma solidity ^0.4.2;

// Next 2 lines show import error, but you can ignore this.
import "truffle/Assert.sol"; // See https://github.com/trufflesuite/truffle/blob/beta/lib/testing/Assert.sol
import "truffle/DeployedAddresses.sol";
import "../contracts/MultiplyContract.sol";

contract TestMultiplyContract {

    function testConstructAndMultiply() public {
        MultiplyContract mc = new MultiplyContract(4);

        Assert.equal(28, mc.multiply(7), "multiply result should be 28");
    }

    function testConstructAndAdd() public {
        MultiplyContract mc = new MultiplyContract(0);

        Assert.equal(10, mc.add(1, 9), "add result should be 10");
    }

}