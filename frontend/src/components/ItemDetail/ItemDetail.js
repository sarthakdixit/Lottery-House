import React from "react";
import "./index.scss";

const ItemDetail = ({ itemdetail }) => {
  return (
    <div className="itemdetail">
      <div className="form-group">
        <label>Name</label>
        <input
          type="text"
          className="form-control"
          value={itemdetail.item.name}
          disabled
        />
      </div>
      <div className="form-group">
        <label>Description</label>
        <textarea
          className="form-control"
          value={itemdetail.item.description}
          disabled
        ></textarea>
      </div>
      <div className="form-group">
        <label>Amount</label>
        <input
          type="text"
          className="form-control"
          value={`${itemdetail.item.amount} ETH`}
          disabled
        />
      </div>
      <div className="form-group">
        <label>Total Users Participated</label>
        <input
          type="text"
          className="form-control"
          value={itemdetail.paymentCount}
          disabled
        />
      </div>
      <div className="form-group">
        <label>Total Amount Collected</label>
        <input
          type="text"
          className="form-control"
          value={`${itemdetail.paymentCount * itemdetail.item.amount} ETH`}
          disabled
        />
      </div>
    </div>
  );
};

export default ItemDetail;
