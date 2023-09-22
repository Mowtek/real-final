import React, { useState } from "react";
import "./Login.css";
import { useNavigate, NavLink } from "react-router-dom";

function Login({ setJwt }) {
  const [error, setError] = useState("");
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [showRedirect, setShowRedirect] = useState(false);

  const navigate = useNavigate();

  const handleSuccess = () => {
    setShowRedirect(true);
    setTimeout(() => {
      setShowRedirect(false);
    }, 1950);
    setTimeout(() => {
      navigate("/");
    }, 2000);
  };

  const handleSubmit = () => {
    fetch("https://localhost/api/users/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        UserName: userName,
        Password: password,
      }),
    })
      .then((res) => {
        if (res.status === 200) {
          return res.json();
        } else {
          setError("One or more fields are Invalid.");
        }
      })
      .then((json) => {
        localStorage.setItem("accessToken", json.token);
        setJwt(json.token);
        handleSuccess();
      })
      .catch((err) => console.error(err));
  };

  return (
    <div className="LoginPage">
      <header className="loginPageHeader">
        <button
          className="backButton"
          onClick={() => {
            navigate(-1);
          }}
        >
          <i className="fa-solid fa-arrow-left backIcon"></i>
        </button>
        <h2>Login</h2>
      </header>
      <div className="registerPage">
        {error && (
          <p style={{ textAlign: "center", color: "red", fontWeight: "bold" }}>
            {error}
          </p>
        )}
        {showRedirect && (
          <p
            style={{
              textAlign: "center",
              color: "rgb(87, 233, 87)",
              fontWeight: "bold",
            }}
          >
            Success! Redirecting to Home page...
          </p>
        )}
        <form className="registerForm">
          <div className="registerDiv">
            <label for="UserName">Username:</label>
            <input
              type="text"
              minLength="1"
              maxLength="32"
              name="UserName"
              id="UserName"
              required
              className="registerInput"
              onChange={(e) => {
                setUserName(e.target.value);
              }}
            />
          </div>
          <div className="registerDiv">
            <label for="Password">Password:</label>
            <input
              type="password"
              minLength="8"
              name="Password"
              id="Password"
              required
              className="registerInput"
              onChange={(e) => {
                setPassword(e.target.value);
              }}
            />
          </div>
          <button
            className="registerSubmitButton"
            onClick={(e) => {
              e.preventDefault();
              handleSubmit();
            }}
          >
            Submit
          </button>
        </form>
        <div className="registerPage">
          <p>Don't have a User yet?</p>
          <NavLink
            to="/register"
            style={{ color: "white", fontWeight: "bold" }}
          >
            Register here
          </NavLink>
        </div>
      </div>
    </div>
  );
}

export default Login;
