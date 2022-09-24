import React, { useState, useContext } from "react";
import AuthContext from "../../context/AuthContext";
import { toast } from "react-toastify";
import axios from "axios";
import { Config } from "../../utilities/Config";

const AuthForm = () => {
  const { setTokens } = useContext(AuthContext);
  const [form, setForm] = useState({
    username: "",
    email: "",
    password: "",
  });
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    let name = e.target.name;
    let value = e.target.value;
    setForm({
      ...form,
      [name]: value,
    });
  };

  const submitForm = async (type) => {
    setLoading(true);
    if (checkInputs()) {
      let url = type == 0 ? "/api/Users/Login" : "/api/Users/Regsiter";
      try {
        let response = await axios.post(`${Config.baseUrl}${url}`, form);
        if (response.status == 200) {
          setTokens(response.data);
        }
      } catch (e) {
        toast.error(e.response.data.Message);
      }
    } else {
      toast.error("Please input values");
    }
    setLoading(false);
  };

  const checkInputs = () => {
    if (
      form.email &&
      form.username &&
      form.password &&
      validateEmail(form.email) &&
      form.password.length >= 6
    )
      return true;
    return false;
  };

  const validateEmail = (email) => {
    return email.match(
      /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
  };

  return (
    <>
      <div className="h-100 container">
        <div className="row d-flex align-items-center justify-content-center">
          <div className="col-md-6">
            <div
              className="shadow p-3 mb-5 bg-white rounded card px-5 py-5 mt-5"
              id="form1"
            >
              <div className="form-data">
                <div className="forms-inputs mb-4">
                  <h1>Lottery House</h1>
                </div>
                <div className="forms-inputs mb-4">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Username"
                    name="username"
                    value={form.username}
                    onChange={handleChange}
                    disabled={loading}
                    required
                  />
                </div>
                <div className="forms-inputs mb-4">
                  <input
                    type="email"
                    className="form-control"
                    placeholder="Email"
                    name="email"
                    value={form.email}
                    onChange={handleChange}
                    disabled={loading}
                    required
                  />
                </div>
                <div className="forms-inputs mb-4">
                  <input
                    type="password"
                    className="form-control"
                    placeholder="Password (Min. 6 characters)"
                    min={6}
                    name="password"
                    value={form.password}
                    onChange={handleChange}
                    disabled={loading}
                    required
                  />
                </div>
                <div className="mb-3">
                  <button
                    type="button"
                    className="btn btn-success w-100"
                    disabled={loading}
                    onClick={() => submitForm(0)}
                  >
                    Login
                  </button>
                </div>
                <div className="mb-3">
                  <button
                    type="button"
                    className="btn btn-primary w-100"
                    disabled={loading}
                    onClick={() => submitForm(1)}
                  >
                    Register
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default AuthForm;
