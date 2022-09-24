import React, { useState } from "react";
import "./index.scss";
import { ethers } from "ethers";
import smartContract from "../../smartContract.json";
import { Config } from "../../utilities/Config";
import { toast } from "react-toastify";
import useAxios from "../../utilities/useAxios";

const EtherForm = ({ itemId, userPaid, recipient, amount }) => {
  const [loading, setLoading] = useState(false);
  const api = useAxios();

  const sendAmount = async () => {
    try {
      let provider = new ethers.providers.Web3Provider(window.ethereum);
      await provider.send("eth_requestAccounts", []);
      let signer = await provider.getSigner();
      let signerAddress = await signer.getAddress();
      let instance = new ethers.Contract(
        Config.smartContractId,
        smartContract,
        signer
      );
      await instance.sendAmount(recipient, amount.toString());
      await invest(signerAddress.toString());
    } catch (e) {
      toast.error(e.message);
    }
  };

  const invest = async (signerAddress) => {
    let form = {
      itemId: itemId,
      cryptId: signerAddress,
    };
    try {
      let response = await api.post("/api/Investment/InsertAnInvestment", form);
      if (response.status == 200 || response.status == 204) {
        toast.success("Congrats, you have participated in lottery");
        window.location.reload();
      }
    } catch (e) {
      toast.error(e.response.data.Message);
    }
  };

  const handleSubmit = async () => {
    setLoading(true);
    await sendAmount();
    setLoading(false);
  };

  return (
    <div className="ether-form">
      {userPaid ? (
        <button className="btn btn-success" disabled>
          Participated
        </button>
      ) : (
        <button
          className="btn btn-primary"
          onClick={handleSubmit}
          disabled={loading}
        >
          Participate
        </button>
      )}
    </div>
  );
};

export default EtherForm;
