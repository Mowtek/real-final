import React, { useState } from "react";
import "./Register.css";
import { useNavigate, NavLink } from "react-router-dom";

function Register({ setJwt }) {
  const [error, setError] = useState("");
  const [userName, setUserName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [bio, setBio] = useState("");
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
    fetch("https://localhost/api/users/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        UserName: userName,
        Email: email,
        Password: password,
        Bio: bio,
      }),
    })
      .then((res) => {
        if (res.status === 201) {
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
        <h2>Register</h2>
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
            <label for="Email">Email:</label>
            <input
              type="email"
              minLength="1"
              maxLength="320"
              pattern="[a-z0-9._%+\-]+@[a-z0-9.\-]+\.[a-z]{2,}$"
              name="Email"
              id="Email"
              required
              className="registerInput"
              onChange={(e) => {
                setEmail(e.target.value);
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
          <div className="registerDiv">
            <label>Bio:</label>
            <textarea
              type="textarea"
              maxLength="160"
              name="Bio"
              id="Bio"
              className="registerInput registerBio"
              onChange={(e) => {
                setBio(e.target.value);
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
          <p>Already a User?</p>
          <NavLink to="/login" style={{ color: "white", fontWeight: "bold" }}>
            Login here
          </NavLink>
        </div>
      </div>
    </div>
  );
}

export default Register;
