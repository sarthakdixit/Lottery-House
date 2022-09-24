import React, { useContext } from "react";
import { Navigate } from "react-router-dom";
import AuthContext from "../context/AuthContext";

const PrivateRoute = ({ children }) => {
  const { tokens } = useContext(AuthContext);

  return tokens ? children : <Navigate to="/auth" />;
};

export default PrivateRoute;
