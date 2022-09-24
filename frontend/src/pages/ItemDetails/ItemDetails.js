import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Spinner from "../../components/Spinner/Spinner";
import "./index.scss";
import useAxios from "../../utilities/useAxios";
import { toast } from "react-toastify";
import ItemDetail from "../../components/ItemDetail/ItemDetail";
import EtherForm from "../../components/EtherForm/EtherForm";
import { Config } from "../../utilities/Config";

const ItemDetails = () => {
  const { id } = useParams();
  const [loading, setLoading] = useState(true);
  const [itemDetail, setItemDetail] = useState({});
  const api = useAxios();

  useEffect(() => {
    getItemDetail();
  }, []);

  const getItemDetail = async () => {
    try {
      let response = await api.get(`/api/Item/GetItemDetail?itemid=${id}`);
      if (response.status == 200) {
        setItemDetail(response.data);
      }
    } catch (e) {
      toast.error(e.response.data.Message);
    }
    setLoading(false);
  };

  return (
    <div className="item-detail-page">
      {loading ? (
        <div className="spinner-container">
          <Spinner />
        </div>
      ) : (
        <>
          {Object.keys(itemDetail).length == 0 ? (
            <h1>Invalid Item</h1>
          ) : (
            <>
              <ItemDetail itemdetail={itemDetail} />
              <hr></hr>
              <EtherForm
                itemId={itemDetail.item.id}
                userPaid={itemDetail.userPaid}
                recipient={Config.smartContractId}
                amount={itemDetail.item.amount * Config.EthToWei}
              />
            </>
          )}
        </>
      )}
    </div>
  );
};

export default ItemDetails;
