pragma solidity ^0.4.18; // solhint-disable-line


// Based on https://github.com/pipermerriam/ethereum-uuid
contract UUIDProviderContract {
    // Actual cost is about 500,000 gas so we will only try to increase the entropy when we've been provided enough gas.
    uint constant private ADD_ENTROPY_GAS = 750000;

    event UUIDEvent (
        bytes16 uuid
    );

    mapping (bytes32 => bool) private used;
    mapping (bytes16 => bool) private seen;

    bytes16 private prev;
    bytes16 private next;
    bytes16[] private collisions;
    bytes private entropy;
    uint private minEntropy;

    // Constructor
    function UUIDProviderContract() public {
        seen[0x0] = true;
        minEntropy = 32;
        addEntropy();
    }

    // Public generateUUID4 function
    function generateUUID4() public returns (bytes16 uuid) {
        while (seen[uuid]) {
            if (uuid != 0x0) {
                collisions.push(uuid);
            }

            if (next != 0x0) {
                uuid = next;
                next = 0x0;
            } else {
                var b = getByte();
                bytes32 buf = keccak256(prev, b);
                uuid = setUUID4Bytes(bytes16(buf));
                next = setUUID4Bytes(bytes16(uint(buf) / 2 ** 128));
            }
        }

        seen[uuid] = true;
        UUIDEvent(uuid);
        prev = uuid;
        return uuid;
    }

    function getByte() private returns (byte b) {
        /*
         *  Entropy sources
         *  - now
         *  - block.hash(..) * 256 (ish)
         *  - tx.origin
         *  - msg.sender
         *  - block.gaslimit
         *  - msg.gas
         *  - tx.gasprice
         *  - address(this)
         */
        // both return a single byte as well as refilling entropy as needed.
        if (entropy.length < 2 || (entropy.length < minEntropy && msg.gas > ADD_ENTROPY_GAS)) {
            addEntropy();
            minEntropy += 1;
        }

        if (entropy.length > 0) {
            b = entropy[entropy.length - 1];
            entropy.length -= 1;
            return b;
        }

        assert(true);
    }

    function addEntropy() private returns (bool) {
        bytes32 key = getEntropy();

        if (key == 0x0) {
            return false;
        }

        for (uint i = 0; i < 32; i++) {
            entropy.push(byte(key));
            key = bytes32(uint(key) / 2**8);
        }
        return true;
    }

    function getEntropy() private constant returns (bytes32 key) {
        for (uint i = 0; i < 256; i++) {
            key = block.blockhash(block.number - i);
            if (!used[key]) {
                return key;
            }
        }
        key = keccak256(now);
        if (!used[key]) {
            return key;
        }

        key = keccak256(msg.gas);
        if (!used[key]) {
            return key;
        }

        key = keccak256(block.difficulty);
        if (!used[key]) {
            return key;
        }

        // Code below is disabled because of solhint error : 'Avoid to use tx.origin'
        // key = keccak256(msg.sender);
        // if (!used[key]) {
        //     return key;
        // }

        key = keccak256(msg.sender);
        if (!used[key]) {
            return key;
        }

        key = keccak256(block.coinbase);
        if (!used[key]) {
            return key;
        }

        key = keccak256(block.gaslimit);
        if (!used[key]) {
            return key;
        }

        key = keccak256(tx.gasprice);
        if (!used[key]) {
            return key;
        }

        key = keccak256(address(this));
        if (!used[key]) {
            return key;
        }

        return 0x0;
    }

    function collisionCount() private constant returns (uint) {
        return collisions.length;
    }
    
    function setUUID4Bytes(bytes16 v) private pure returns (bytes16) {
        byte byte5 = byte(uint(v) * 2 ** (8 * 5));
        byte byte7 = byte(uint(v) * 2 ** (8 * 7));

        if (byte7 < 0x40 || byte7 >= 0x50) {
            byte7 = byte(uint8(byte7) % 16 + 64);
            v &= 0xffffffffffffffff00ffffffffffffff;
            v |= bytes16(uint(byte7) * 2 ** (8 * 7));
        }

        if (byte5 < 0x80 || byte5 > 0xb0) {
            byte5 = byte(uint8(byte5) % 64 + 128);
            v &= 0xffffffffffffffffffff00ffffffffff;
            v |= bytes16(uint(byte5) * 2 ** (8 * 5));
        }

        return v;
    }
}