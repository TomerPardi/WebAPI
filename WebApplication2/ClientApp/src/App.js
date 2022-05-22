import React, { useState, useEffect } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import NotFound from "./Pages/NotFound";
import Homepage from "./Pages/homepage/Homepage";
import { ProtectedRoutes } from "./Pages/ProtectedRoutes";
import { AppContextProvider } from "./AppContext";
import axios from "axios";
import { HubConnectionBuilder } from "@microsoft/signalr";

export const App = () => {
  const [isLoading, setLoading] = useState(true);
  const [isAuth, setAuth] = useState(false);
  const [changed, setChanged] = useState(false);
  const sharedContext = require("./sharedContext.json");

  useEffect(() => {
    const authenticate = async () => {
      const res = await axios
        .get(`https://localhost:7066/Self`, {
          withCredentials: true,
        })
        .catch((e) => console.error(e));
      const serverRes = await axios
        .get(`https://localhost:7066/Server`, {

        })
        .catch((e) => console.error(e));
      setLoading(false);
      if (res !== undefined && res.status === 200) {
        setAuth(true);
        sharedContext.server = serverRes.data;
      }
    };
    authenticate();
  }, [isAuth]);

  const connectionFunc = async (username) => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7066/messagesHub")
        .build();

      sharedContext.connection = connection;
      connection.on("Changed", () => {
        // we need to define changed state in app js
        setChanged(true);
      });
      await connection.start();
      await connection.invoke("Join", username);
    } catch { }
  };

  return (
    // TODO check if we can cache chats locally
    <AppContextProvider value={sharedContext}>
      <BrowserRouter>
        <Routes>
          <Route path='*' element={<NotFound />} />
          <Route
            element={<ProtectedRoutes isLoading={isLoading} isAuth={isAuth} />}
          >
            <Route
              path='home'
              element={<Homepage setChanged={setChanged} changed={changed} />}
            />
          </Route>
          <Route
            path='/'
            element={
              <Login setAuth={setAuth} connectionFunc={connectionFunc} />
            }
          />
          {/* <Route path='/' element={<Main isLoading={isLoading} isAuth={isAuth} />} /> */}
          <Route path='register' element={<Register />} />
          {/* <Route path='login' element={<Login setAuth={setAuth} />} /> */}
        </Routes>
      </BrowserRouter>
    </AppContextProvider>
  );
};
