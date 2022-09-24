import React, { useContext } from "react";
import { Navigate } from "react-router-dom";
import AuthContext from "../context/AuthContext";

const ProtectedRoute = ({ children }) => {
  const { tokens } = useContext(AuthContext);

  return tokens ? <Navigate to="/" /> : children;
};

export default ProtectedRoute;
