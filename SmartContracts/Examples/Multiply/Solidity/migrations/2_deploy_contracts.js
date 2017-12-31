// var UUIDProviderContract = artifacts.require("../../UUIDProvider/Solidity/contracts/UUIDProviderContract.sol");
var MultiplyContract = artifacts.require("MultiplyContract");

module.exports = (deployer) => {
    // deployer.deploy(UUIDProviderContract);

    var multiplier = 4;
    deployer.deploy(MultiplyContract, multiplier);
};
