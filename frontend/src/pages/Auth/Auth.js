import React from "react";
import AuthForm from "../../components/AuthForm/AuthForm";
import LoginBackground from "../../assets/images/login-background.jpg";
import "./index.scss";

const Login = () => {
  return (
    <>
      <img className="image-container" src={LoginBackground}></img>
      <AuthForm />
    </>
  );
};

export default Login;
