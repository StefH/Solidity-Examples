pragma solidity ^0.4.24; // solhint-disable-line


contract ElectionContract {
    mapping(address => bool) private voters;
    mapping(string => uint) private candidateVote;
    
    constructor() public {
        candidateVote["Alice"] = 0;
        candidateVote["Bob"] = 0;
    }
    
    function getVote(string name) public view returns(uint) {
        require(bytes(name).length > 0, "Bad request: 'name' is null or empty");

        return candidateVote[name];
    }
    
    function vote(string name) public {
        require(bytes(name).length > 0, "Bad request: 'name' is null or empty");
        require(!voters[msg.sender], "Bad request: this address has already voted");

        voters[msg.sender] = true;

        candidateVote[name]++;
    }

    function recursion(int i) public {
        recursion(i);
    }
}