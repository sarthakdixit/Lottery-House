import React, { useEffect, useState } from "react";
import "./index.scss";
import useAxios from "../../utilities/useAxios";
import { toast } from "react-toastify";
import Spinner from "../../components/Spinner/Spinner";
import ItemDetailCard from "../../components/ItemDetailCard/ItemDetailCard";

const ChooseWinner = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const api = useAxios();

  useEffect(() => {
    getData();
  }, []);

  const getData = async () => {
    try {
      let response = await api.get("/api/Item/GetItemsToChooseWinner");
      if (response.status == 200) {
        setData(response.data);
      }
    } catch (e) {
      toast.error(e.response.data.Message);
    }
    setLoading(false);
  };

  return (
    <div className="home-page">
      {loading ? (
        <div className="spinner-container">
          <Spinner />
        </div>
      ) : (
        <>
          {data.length == 0 ? (
            <h1>All lotteries are active</h1>
          ) : (
            <>
              {data.map((itemDetail, id) => {
                let dateStr = new Date(itemDetail.item.expiredAt);
                itemDetail.item.expiredAt = dateStr.toLocaleDateString();
                return (
                  <ItemDetailCard key={id} itemDetail={itemDetail} button={1} />
                );
              })}
            </>
          )}
        </>
      )}
    </div>
  );
};

export default ChooseWinner;
