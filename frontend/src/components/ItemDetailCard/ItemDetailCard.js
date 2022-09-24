import React, { useState } from "react";
import "./index.scss";
import useAxios from "../../utilities/useAxios";
import { ethers } from "ethers";
import smartContract from "../../smartContract.json";
import { Config } from "../../utilities/Config";
import { toast } from "react-toastify";

const ItemDetailCard = ({ itemDetail, button }) => {
  const [investment, setInvestment] = useState({});
  const [loading, setLoading] = useState(false);
  const api = useAxios();

  const chooseWinner = async () => {
    setLoading(true);
    try {
      let response = await api.post(
        `/api/Winner/ChooseWinner?itemId=${itemDetail.item.id}`
      );
      if (response.status == 200) {
        setInvestment(response.data);
      }
    } catch (e) {
      toast.error(e.response.data.Message);
    }
    setLoading(false);
  };

  const insertWinner = async () => {
    setLoading(true);
    try {
      let response = await api.post("/api/Winner/InsertAWinner", investment);
      if (response.status == 200) {
        toast.success("Winner got selected");
      }
    } catch (e) {
      toast.error(e.response.data.Message);
    }
    setLoading(false);
  };

  const sendAmount = async () => {
    setLoading(true);
    try {
      let provider = new ethers.providers.Web3Provider(window.ethereum);
      await provider.send("eth_requestAccounts", []);
      let signer = await provider.getSigner();
      let instance = new ethers.Contract(
        Config.smartContractId,
        smartContract,
        signer
      );
      let amount =
        itemDetail.paymentCount * itemDetail.item.amount * Config.EthToWei;
      await instance.sendAmount(investment.cryptId, amount.toString());
      await insertWinner();
      window.location.reload();
    } catch (e) {
      toast.error(e.message);
    }
    setLoading(false);
  };

  return (
    <div className="card" style={{ width: "18rem", margin: "10px" }}>
      <div className="card-body">
        <h5 className="card-title">{itemDetail.item.name}</h5>
        <h6 className="card-subtitle mb-2 text-muted">
          Total Amount : {itemDetail.paymentCount * itemDetail.item.amount} ETH
        </h6>
        <p className="card-text">Expired At : {itemDetail.item.expiredAt}</p>
        {button == 1 ? (
          <>
            {Object.keys(investment).length == 0 ? (
              <button
                disabled={loading}
                className="btn btn-primary"
                onClick={chooseWinner}
              >
                Choose Winner
              </button>
            ) : (
              <button
                disabled={loading}
                className="btn btn-success"
                onClick={sendAmount}
              >
                Pay to Winner
              </button>
            )}
          </>
        ) : null}
      </div>
    </div>
  );
};

export default ItemDetailCard;
