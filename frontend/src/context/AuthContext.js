import { createContext, useState, useEffect } from "react";

const AuthContext = createContext();

export default AuthContext;

export const AuthProvider = ({ children }) => {
  const [tokens, setTokens] = useState(() =>
    JSON.parse(localStorage.getItem("tokens"))
  );

  useEffect(() => {
    localStorage.setItem("tokens", JSON.stringify(tokens));
  }, [tokens]);

  let contextData = {
    tokens,
    setTokens,
  };

  return (
    <AuthContext.Provider value={contextData}>{children}</AuthContext.Provider>
  );
};
