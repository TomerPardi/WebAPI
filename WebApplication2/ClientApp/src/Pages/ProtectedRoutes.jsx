import React from "react";
import { Navigate, Outlet } from "react-router-dom";
import "./ProtectedRoutes.css"


export const ProtectedRoutes = (props) => {

  if (props.isLoading) {
    // loading spinner courtesy of https://loading.io/css/
    return <div><div class="lds-roller">
      <div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div></div>
  }
  else {
    return props.isAuth ? <Outlet /> : <Navigate to='/' />
  }
};
