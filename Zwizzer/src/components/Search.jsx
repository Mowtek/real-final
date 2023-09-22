import React, { useRef, useState } from "react";
import "./Search.css";
import { useNavigate } from "react-router-dom";
import Posts from "./Posts.jsx";

function Search() {
  const navigate = useNavigate();
  let searchInfo = useRef();
  let [posts, setPosts] = useState([]);
  let [error, setError] = useState("");
  const handleSearch = () => {
    fetch(`https://localhost/api/zweets/search/${searchInfo}`)
      .then((res) => {
        if (res.status === 200) {
          setError("");
          return res.json();
        } else if (res.status === 404) setError("No zweets found.");
      })
      .then((json) => setPosts(json))
      .catch((err) => console.err(err));
  };

  return (
    <div id="SearchPage">
      <header className="searchHeader">
        <button
          className="backButton"
          onClick={() => {
            navigate(-1);
          }}
        >
          <i className="fa-solid fa-arrow-left backIcon"></i>
        </button>
        <input
          type="text"
          name="searchBar"
          id="searchBar"
          placeholder="Search"
          onChange={(i) => (searchInfo = i.target.value)}
          onKeyUp={(e) => {
            if (e.key === "Enter") handleSearch();
          }}
        />
        <button className="searchButton" onClick={(i) => handleSearch()}>
          <i className="fa-solid fa-magnifying-glass"></i>
        </button>
      </header>
      {error && <div className="displayErrorSearch">{error}</div>}
      {posts && <Posts posts={posts} />}
    </div>
  );
}

export default Search;
