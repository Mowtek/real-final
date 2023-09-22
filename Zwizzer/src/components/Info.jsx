import React, { useContext, useState } from "react";
import "./Info.css";
import { NavLink } from "react-router-dom";
import { UserContext } from "../App";

function Info({ logOut }) {
  const user = useContext(UserContext);
  const [show, setShow] = useState(false);

  const toggleShow = () => {
    show ? setShow(false) : setShow(true);
  };

  return (
    <div id="info">
      <NavLink className={"logo"} to="/">
        <img className="bird" src="/bird.png" alt="" />
      </NavLink>
      <div id="NavBar">
        <NavLink
          to="/"
          className={({ isActive, isPending }) =>
            isPending ? "pending" : isActive ? "active" : ""
          }
        >
          <i className="fa-solid fa-house icon"></i>
          <p>Home</p>
        </NavLink>
        <NavLink
          to="/search"
          className={({ isActive, isPending }) =>
            isPending ? "pending" : isActive ? "active" : ""
          }
        >
          <i className="fa-solid fa-magnifying-glass"></i>
          <p>Search</p>
        </NavLink>
        {Object.keys(user).length > 0 && (
          <NavLink
            to={`/profile/${user.userId}`}
            className={({ isActive, isPending }) =>
              isPending ? "pending" : isActive ? "active" : ""
            }
          >
            <i className="fa-regular fa-user"></i>
            <p>Profile</p>
          </NavLink>
        )}
        {Object.keys(user).length > 0 && user.userRole === 2 && (
          <NavLink
            to={`/manage-users`}
            className={({ isActive, isPending }) =>
              isPending ? "pending" : isActive ? "active" : ""
            }
          >
            <i className="fa-regular fa-user"></i>
            <p>Manage Users</p>
          </NavLink>
        )}
      </div>
      <div className="PostButton">
        {Object.keys(user).length === 0 && user.constructor === Object && (
          <NavLink to="/login" className="Post">
            <p>Zweet</p>
          </NavLink>
        )}
        {Object.keys(user).length > 0 && (
          <NavLink to="/#NewPost" className="Post">
            <p>Zweet</p>
          </NavLink>
        )}
      </div>
      {Object.keys(user).length === 0 && user.constructor === Object && (
        <div className="ProfileTab">
          <NavLink className="loginAndRegister" to="/login">
            Login
          </NavLink>
          <NavLink className="loginAndRegister" to="/register">
            Register
          </NavLink>
        </div>
      )}
      {Object.keys(user).length > 0 && (
        <div className="ProfileTab">
          {show && (
            <div className="infoLogOutPanel">
              <button
                className="infoLogOutButton"
                onClick={() => {
                  logOut();
                  setShow(false);
                }}
              >
                <i className="fa-solid fa-right-from-bracket"></i>
                Log Out
              </button>
            </div>
          )}
          <button onClick={() => toggleShow()}>
            <img
              src={`https://localhost${user.profileImagePath}`}
              alt="Your Photograph"
              className="sideProfileImage"
            />
            <h3>{user.userName}</h3>
            <i className="fa-solid fa-gear"></i>
          </button>
        </div>
      )}
    </div>
  );
}

export default Info;
