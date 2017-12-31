var UUIDProviderContract = artifacts.require("./UUIDProviderContract.sol");

module.exports = (deployer) => {
    deployer.deploy(UUIDProviderContract);
};