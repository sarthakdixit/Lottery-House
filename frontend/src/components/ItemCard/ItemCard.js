import React from "react";
import "./index.scss";

const ItemCard = ({ item }) => {
  return (
    <div className="card" style={{ width: "18rem", margin: "10px" }}>
      <div className="card-body">
        <h5 className="card-title">{item.name}</h5>
        <h6 className="card-subtitle mb-2 text-muted">
          Amount : {item.amount} ETH
        </h6>
        <p className="card-text">Will Expire At : {item.expiredAt}</p>
        <a href={`/lottery/${item.id}`} className="card-link">
          Visit
        </a>
      </div>
    </div>
  );
};

export default ItemCard;
