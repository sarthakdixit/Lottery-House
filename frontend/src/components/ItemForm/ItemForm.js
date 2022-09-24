import React, { useState } from "react";
import useAxios from "../../utilities/useAxios";
import { toast } from "react-toastify";

const ItemForm = () => {
  const [form, setForm] = useState({
    name: "",
    description: "",
    timeFrame: "1D",
    amount: 0,
  });
  const api = useAxios();
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    let name = e.target.name;
    let value = e.target.value;
    setForm({
      ...form,
      [name]: value,
    });
  };

  const submitForm = async (e) => {
    e.preventDefault();

    setLoading(true);

    try {
      let response = await api.post("/api/Item/InsertAnItem", form);
      if (response.status == 200 || response.status == 204) {
        toast.success("Item submitted");
      }
    } catch (e) {
      //console.log(e);
      toast.error(e.response.data.Message);
    }
    cleanForm();
    setLoading(false);
  };

  const cleanForm = () => {
    setForm({
      name: "",
      description: "",
      timeFrame: "1D",
      amount: 0,
    });
  };

  return (
    <>
      <h1>Submit New Item</h1>
      <form onSubmit={submitForm}>
        <div className="form-group">
          <label>Name</label>
          <input
            type="text"
            name="name"
            value={form.name}
            className="form-control"
            placeholder="Name"
            onChange={handleChange}
            disabled={loading}
            required
          />
        </div>
        <div className="form-group">
          <label>Description</label>
          <textarea
            className="form-control"
            id="exampleFormControlTextarea1"
            rows="3"
            value={form.description}
            name="description"
            placeholder="Description"
            onChange={handleChange}
            required
            disabled={loading}
          ></textarea>
        </div>
        <div className="form-group">
          <label>Time Frame</label>
          <select
            className="form-control"
            name="timeFrame"
            onChange={handleChange}
            required
            value={form.timeFrame}
            disabled={loading}
          >
            <option value="1D">1 Day</option>
            <option value="1W">1 Week</option>
            <option value="1M">1 Month</option>
            <option value="1Y">1 Year</option>
          </select>
        </div>
        <div className="form-group">
          <label>Amount</label>
          <input
            type="number"
            name="amount"
            value={form.amount}
            className="form-control-file"
            onChange={handleChange}
            required
            disabled={loading}
          />
        </div>
        <button type="submit" className="btn btn-primary" disabled={loading}>
          Submit
        </button>
        <button
          type="reset"
          onClick={cleanForm}
          className="btn btn-danger"
          disabled={loading}
        >
          Reset
        </button>
      </form>
    </>
  );
};

export default ItemForm;
