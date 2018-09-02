var SalesContract = artifacts.require("SalesContract");

module.exports = (deployer) => {
    deployer.deploy(SalesContract);
};
