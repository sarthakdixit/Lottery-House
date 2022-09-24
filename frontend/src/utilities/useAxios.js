import axios from "axios";
import { Config } from "./Config";
import { useContext } from "react";
import AuthContext from "../context/AuthContext";

const baseURL = Config.baseUrl;

const useAxios = () => {
  const { tokens, setTokens } = useContext(AuthContext);

  const axiosInstance = axios.create({
    baseURL,
    headers: { Authorization: `Bearer ${tokens?.accessToken}` },
  });

  axiosInstance.interceptors.request.use(async (req) => {
    if (tokens != null) {
      req.headers.Authorization = `Bearer ${tokens.accessToken}`;

      try {
        let tokenExpiredResponse = await axios.post(
          `${baseURL}/api/Token/ValidateToken`,
          tokens
        );
        if (tokenExpiredResponse.status == 200) {
          let tokenApi = tokenExpiredResponse.data;
          if (tokenApi.accessToken && tokenApi.recoveryToken) {
            setTokens({
              accessToken: tokenApi.accessToken,
              recoveryToken: tokenApi.recoveryToken,
            });
            req.headers.Authorization = `Bearer ${tokenApi.accessToken}`;
          }
        }
      } catch (e) {
        setTokens(null);
      }
    }
    return req;
  });
  return axiosInstance;
};

export default useAxios;
