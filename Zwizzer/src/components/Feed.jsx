import React from "react";
import "./Feed.css";
import Home from "./Home.jsx";
import Profile from "./Profile.jsx";
import Search from "./Search.jsx";
import Login from "./Login.jsx";
import Register from "./Register.jsx";
import Post from "./Post.jsx";
import { Route, Routes, NavLink } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";
import ManageUsers from "./ManageUsers.jsx";
import Error from "./Error.jsx";

function Feed({ user, setUser, logOut, setJwt }) {
  return (
    <div id="FeedArea">
      <Routes>
        <Route path="/">
          <Route index element={<Home logOut={logOut} />} />
          <Route path="search" element={<Search />} />
          <Route path="login" element={<Login setJwt={setJwt} />} />
          <Route path="register" element={<Register setJwt={setJwt} />} />
          <Route
            path="profile/:id"
            element={<Profile user={user} logOut={logOut} />}
          />
          <Route path="error/404" element={<Error error={404} />} />
          <Route path="error/401" element={<Error error={401} />} />
          <Route
            path="post/:userId/:postId"
            element={<Post logOut={logOut} />}
          />
          <Route
            path="manage-users"
            element={
              <ProtectedRoute>
                <ManageUsers logOut={logOut} />
              </ProtectedRoute>
            }
          />
        </Route>
      </Routes>
      <div className="FeedNavigation">
        <NavLink to="/" className={"FeedNavigationLink"}>
          <i className="fa-solid fa-house"></i>
        </NavLink>
        <NavLink to="/search" className={"FeedNavigationLink"}>
          <i className="fa-solid fa-magnifying-glass"></i>
        </NavLink>
        {Object.keys(user).length > 0 && user.userRole === 2 && (
          <NavLink className={"FeedNavigationLink"} to="/manage-users">
            <i className="fa-regular fa-user"></i>
          </NavLink>
        )}
        {Object.keys(user).length > 0 && (
          <>
            <NavLink
              to={`/profile/${user.userId}`}
              className={"FeedNavigationLink"}
            >
              <i className="fa-solid fa-user"></i>
            </NavLink>
            <button className="FeedNavigationButton" onClick={() => logOut()}>
              <i className="fa-solid fa-right-from-bracket"></i>
            </button>
          </>
        )}
        {Object.keys(user).length === 0 && (
          <NavLink to="/login" className={"FeedNavigationLink"}>
            <i className="fa-solid fa-right-to-bracket"></i>
          </NavLink>
        )}
      </div>
    </div>
  );
}

export default Feed;
