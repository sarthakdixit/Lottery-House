//SPDX-License-Identifier: GPL-3.0

pragma solidity >=0.5.0 <0.9.0;

contract Lottery{
    address public manager;

    constructor(){
        // address of person who deploy the contract
        manager = msg.sender;
    }

    modifier onlyOwner(){
        require(manager == msg.sender);
        _;
    }

    // this function runs automatically when contract receive eth
    receive() external payable{
    }

    // returns contract balance in wei
    function getBalance() public view onlyOwner returns(uint){
        return address(this).balance;
    }

    function pickWinner(address add, uint amount) public onlyOwner{
        // transfering balance to winner
        address payable winner;
        winner = payable(add);
        winner.transfer(amount);
    }
}
