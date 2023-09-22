import React, { createContext, useEffect, useState } from "react";
import "./App.css";
import Feed from "./components/Feed.jsx";
import Info from "./components/Info.jsx";
import AdArea from "./components/AdArea.jsx";
import jwt_decode from "jwt-decode";
import { useNavigate } from "react-router-dom";

export const UserContext = createContext();
function App() {
  const navigate = useNavigate();

  const [user, setUser] = useState({});
  const [jwt, setJwt] = useState("");
  useEffect(() => {
    if (localStorage.getItem("accessToken").length > 0) {
      var decodedToken = jwt_decode(localStorage.getItem("accessToken"));
      var userId =
        decodedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        ];
      fetch("https://localhost/api/users/checkJwt", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        },
      })
        .then((res) => {
          if (res.status === 200) {
            fetch(`https://localhost/api/users/profile/${+userId}`)
              .then((res) => res.json())
              .then((json) => setUser(json))
              .catch((err) => console.error(err));
          } else if (res.status === 401) logOut();
        })
        .catch((err) => {
          console.error(err);
          logOut();
        });
    }
  }, [jwt]);

  const logOut = () => {
    setUser({});
    localStorage.setItem("accessToken", "");
    setJwt("");
    navigate("/login");
  };

  return (
    <UserContext.Provider value={user}>
      <div className="App">
        <Info user={user} setUser={setUser} logOut={logOut} />
        <Feed user={user} setUser={setUser} logOut={logOut} setJwt={setJwt} />
        <AdArea />
      </div>
    </UserContext.Provider>
  );
}

export default App;
