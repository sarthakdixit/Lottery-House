import React, { useState, useEffect, useContext } from "react";
import { Outlet } from "react-router-dom";
import "./index.scss";
import SideNavbarItems from "../SideNavbarItems/SideNavbarItems";

const SideNavbarLayout = () => {
  return (
    <>
      <div className="sidebar">
        <SideNavbarItems />
      </div>

      <div className="content">
        <Outlet />
      </div>
    </>
  );
};

export default SideNavbarLayout;
