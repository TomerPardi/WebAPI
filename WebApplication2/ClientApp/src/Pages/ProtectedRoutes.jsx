import React, { useState } from "react";
import { Navigate, Outlet } from "react-router-dom";
import context from "react-bootstrap/esm/AccordionContext";
import { useEffect } from "react";
import AppContext from "../AppContext";
import axios from "axios";


export const ProtectedRoutes = (props) => {
  //const [isAuth, setAuth] = useState(false);
  //const [isLoading, setLoading] = useState(true);


  if (props.isLoading) {
    return <div>loading</div>
  }
  else {
    return props.isAuth ? <Outlet /> : <Navigate to='/' />
  }
};
