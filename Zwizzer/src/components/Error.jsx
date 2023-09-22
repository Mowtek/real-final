import React from "react";
import "./Error.css";
import { NavLink } from "react-router-dom";

function Error({ error }) {
  return (
    <div className="ErrorDiv">
      <h2>Error {error}</h2>
      {error === 404 && <p>Seems like the resource does not exist.</p>}
      {error === 401 && <p>Seems like you're unauthorized.</p>}
      <NavLink to="/" className={"errorHomeLink"}>
        Click here to go back Home
      </NavLink>
    </div>
  );
}

export default Error;
