import React, { useContext, useEffect } from "react";
import AuthContext from "../../context/AuthContext";

const Logout = () => {
  const { setTokens } = useContext(AuthContext);

  useEffect(() => {
    setTokens(null);
  }, []);

  return <div></div>;
};

export default Logout;
