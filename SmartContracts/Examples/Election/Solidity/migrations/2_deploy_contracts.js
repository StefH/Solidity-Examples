var ElectionContract = artifacts.require("ElectionContract");

module.exports = (deployer) => {
    deployer.deploy(ElectionContract);
};
