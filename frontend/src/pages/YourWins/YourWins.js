import React, { useState, useEffect } from "react";
import "./index.scss";
import useAxios from "../../utilities/useAxios";
import { toast } from "react-toastify";
import Spinner from "../../components/Spinner/Spinner";
import ItemDetailCard from "../../components/ItemDetailCard/ItemDetailCard";

const YourWins = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const api = useAxios();

  useEffect(() => {
    getData();
  }, []);

  const getData = async () => {
    try {
      let response = await api.get("/api/Winner/GetWinsUsingUserId");
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
            <h1>Sorry, you havn't won anything</h1>
          ) : (
            <>
              {data.map((itemDetail, id) => {
                let dateStr = new Date(itemDetail.item.expiredAt);
                itemDetail.item.expiredAt = dateStr.toLocaleDateString();
                return (
                  <ItemDetailCard key={id} itemDetail={itemDetail} button={0} />
                );
              })}
            </>
          )}
        </>
      )}
    </div>
  );
};

export default YourWins;
