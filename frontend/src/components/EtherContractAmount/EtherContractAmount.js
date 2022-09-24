import React, { useEffect, useState } from "react";
import { ethers } from "ethers";
import smartContract from "../../smartContract.json";
import { Config } from "../../utilities/Config";
import { toast } from "react-toastify";

const EtherContractAmount = () => {
  const [message, setMessage] = useState("Please login to meta mask");

  useEffect(() => {
    getTotalAmount();
  }, []);

  const getTotalAmount = async () => {
    try {
      let provider = new ethers.providers.Web3Provider(window.ethereum);
      await provider.send("eth_requestAccounts", []);
      let signer = await provider.getSigner();
      let instance = new ethers.Contract(
        Config.smartContractId,
        smartContract,
        signer
      );
      let totalAmount = await instance.getBalance();
      let wei = Number(totalAmount.toString());
      let eth = wei / Config.EthToWei;
      setMessage(`Amount: ${eth} ETH`);
    } catch (e) {
      toast.error(e);
      setMessage(e.message);
    }
  };

  return <h6>{message}</h6>;
};

export default EtherContractAmount;
