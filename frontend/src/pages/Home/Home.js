import React, { useEffect, useState } from "react";
import "./index.scss";
import useAxios from "../../utilities/useAxios";
import { toast } from "react-toastify";
import Spinner from "../../components/Spinner/Spinner";
import ItemCard from "../../components/ItemCard/ItemCard";

const Home = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const api = useAxios();

  useEffect(() => {
    getActiveItems();
  }, []);

  const getActiveItems = async () => {
    try {
      let response = await api.get("/api/Item/GetActiveItems");
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
            <h1>No active lotteries found</h1>
          ) : (
            <>
              {data.map((item, key) => {
                let dateStr = new Date(item.expiredAt);
                item.expiredAt = dateStr.toLocaleDateString();
                return <ItemCard key={key} item={item} />;
              })}
            </>
          )}
        </>
      )}
    </div>
  );
};

export default Home;
