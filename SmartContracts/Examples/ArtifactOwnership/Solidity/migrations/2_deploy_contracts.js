var ArtifactOwnership = artifacts.require("ArtifactOwnership");

module.exports = (deployer) => {
    deployer.deploy(ArtifactOwnership);
};
