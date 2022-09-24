import React, { useState, useEffect, useContext } from "react";
import { items } from "../../utilities/SidebarItemsList";
import { Link } from "react-router-dom";
import useAxios from "../../utilities/useAxios";
import { toast } from "react-toastify";
import EtherContractAmount from "../EtherContractAmount/EtherContractAmount";

const SideNavbarItems = () => {
  const [user, setUser] = useState({});
  const [loading, setLoading] = useState(false);
  const api = useAxios();

  useEffect(() => {
    if (Object.keys(user).length == 0) {
      getUser();
    }
  }, []);

  const getUser = async () => {
    setLoading(true);
    try {
      let response = await api.post("/api/Users/GetUserWithId");
      if (response.status == 200) {
        setUser(response.data);
      }
    } catch (e) {
      toast.error(e.response.data.Message);
    }
    setLoading(false);
  };

  return (
    <>
      {Object.keys(user).length == 0 ? null : (
        <>
          {user.isAdmin ? <EtherContractAmount /> : null}
          {items.map((item, index) => {
            if (user.isAdmin && item.admin == 1) {
              return (
                <Link key={index} to={item.path}>
                  {item.name}
                </Link>
              );
            }
            if (user.isAdmin && item.admin == -1) {
              return null;
            }
            if (!user.isAdmin && item.admin == 1) {
              return null;
            }
            return (
              <Link key={index} to={item.path}>
                {item.name}
              </Link>
            );
          })}
        </>
      )}
    </>
  );
};

export default SideNavbarItems;
