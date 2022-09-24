import axios from "axios";
import { Config } from "./Config";

const baseUrl = Config.baseUrl;

let loginUser = localStorage.getItem("loginUser")
  ? JSON.parse(localStorage.getItem("loginUser"))
  : null;

const axiosInstance = axios.create({
  baseUrl,
  headers: { Authorization: `Bearer ${loginUser.tokens.accessToken}` },
});

axiosInstance.interceptors.request.use(async (req) => {
  loginUser = localStorage.getItem("loginUser")
    ? JSON.parse(localStorage.getItem("loginUser"))
    : null;

  if (loginUser) {
    req.headers.Authorization = `Bearer ${loginUser.tokens.accessToken}`;

    let tokenExpiredResponse = await axios.post(
      `${baseUrl}/api/Token/ValidateToken`
    );

    if (tokenExpiredResponse.status != 200) {
      let tokenRefreshResponse = await axios.post(
        `${baseUrl}/api/Token/RefreshToken`,
        {
          accessToken: loginUser.tokens.accessToken,
          recoveryToken: loginUser.tokens.recoveryToken,
        }
      );
      if (tokenRefreshResponse.status == 200) {
        let tokenApi = tokenRefreshResponse.data;
        loginUser.tokens.accessToken = tokenApi.accessToken;
        loginUser.tokens.recoveryToken = tokenApi.recoveryToken;
        localStorage.setItem("loginUser", JSON.stringify(loginUser));
        req.headers.Authorization = `Bearer ${tokenApi.accessToken}`;
      } else {
        localStorage.setItem("loginUser", null);
      }
    }
  }
  return req;
});

export default axiosInstance;
