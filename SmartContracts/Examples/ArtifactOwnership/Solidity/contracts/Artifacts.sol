pragma solidity ^0.4.23;

import "./AccessControls.sol";

/// @title Package managing artifact actions
contract Artifacts is AccessControls{
    /// @dev All ingame artifacts. Artifact's unqiue ID is
    /// this array's artifact index.
    /// Each uint32 number represents artifact's typeID,
    /// which is used with artifact database.
    uint32[] artifacts;

    /// @dev A mapping from artifact ID to owners' addresses
    mapping (uint256 => address) public artifactIndexToOwner;

    /// @dev A mapping from owner adress to number of tokens it owns
    mapping (address => uint256) public ownershipTokenCount;

    /// @dev A mapping from artifact ID to an approved adress for transer
    /// Zero means there's no approval
    mapping (uint256 => address) public artifactIndexToApproved;

    /// @dev A mapping showing how many artifacts can user materialize
    /// (based on how many times he paid for it)
    mapping(address => uint8) public creationFee;

    /// @dev Transers artifact ownership between accounts
    function _transfer(address _from, address _to, uint256 _tokenId) internal whenNotPaused{
        // Increase token ownership count
        ownershipTokenCount[_to]++;
        // Transfer ownership
        artifactIndexToOwner[_tokenId] = _to;
        // When gameserver creates new artifact instance,
        // but we can't account the address
        if(_from != gameserverAddress){
            // Decrease previous owner's token ownership count
            ownershipTokenCount[_from]--;
            // Clear any previous approved ownership exchange
            delete artifactIndexToApproved[_tokenId];
        }
        // Emit transfer event
        emit Transfer(_from, _to, _tokenId);
    }

    /// @dev A gameserver-only method that creates a new artifact,
    /// stores it and gives to declared player (if he payed for artifact creation)
    /// @param _typeId Created artifact typeId
    /// @param _owner Artifact owner
    function createArtifact (uint32 _typeId, address _owner) public onlyGameserver returns (uint) {
        // Requires that user paid for creation
        require(creationFee[_owner] > 0);
        // Pushing new artifact to the storage
        uint256 _newArtifactId = artifacts.push(_typeId) - 1;
        // Checkig overflow (just in case we go viral c;)
        require(_newArtifactId <= 4294967295);

        // Transfering to the owner
        _transfer(gameserverAddress, _owner, _newArtifactId);
        // Updating creation payment
        creationFee[_owner]--;

        return _newArtifactId;
    }

    /// @dev Returns amount of generated ingame items.
    /// Used for testing & statistics purposes
    function getArtifactsAmount() public view returns (uint){
        return artifacts.length;
    }

    /// @dev Price for storing artifact in blockchain
    uint64 artifactCreationPrice = 0.00125 ether;

    /// @dev User has to pay for creation of artifact
    function payForArtifact() public payable{
        require(msg.value == artifactCreationPrice);
        require(creationFee[msg.sender] < 255);
        creationFee[msg.sender]++;
        emit PayedCreation(msg.sender);
    }

    /// @dev CEO-only function to re-set artifact creation price
    function changeCreationPrice(uint64 newPrice) public onlyCEO {
        artifactCreationPrice = newPrice;
    }

    /// @dev Get artifactt info
    function getArtifactTypeId(uint _id) public view returns(uint32 _typeId) {
        _typeId = artifacts[_id];
    }

    /// @dev Transfer event as defined in current ERC721 template
    event Transfer(address indexed from, address indexed to, uint256 indexed tokenId);

    /// @dev User paying for item creation event
    event PayedCreation(address payer);
}