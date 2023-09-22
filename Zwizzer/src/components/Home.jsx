import React, { useEffect, useState, useRef, useContext } from "react";
import "./Home.css";
import Posts from "./Posts.jsx";
import { UserContext } from "../App";
import { NavLink, useNavigate } from "react-router-dom";

function Home({ logOut }) {
  const user = useContext(UserContext);

  const navigate = useNavigate();

  const [posts, setPosts] = useState([]);
  const [error, setError] = useState("");
  const [postError, setPostError] = useState("");
  const [newPostContent, setNewPostContent] = useState("");

  const textArea = useRef();

  useEffect(() => {
    fetchPosts();
  }, []);

  const handleSubmit = () => {
    fetch("https://localhost/api/zweets", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
      },
      body: JSON.stringify({ userId: user.userId, content: newPostContent }),
    })
      .then((res) => {
        if (res.status === 201) {
          fetchPosts();
          setPostError("");
        } else if (res.status === 401) {
          logOut();
          navigate("/login");
        } else return res.json();
      })
      // .then((json) => {
      //   if (json.errors !== null && json.error.length > 1)
      //     setPostError(json.errors["Content"]);
      // })
      .catch((err) => {
        console.error(err);
        logOut();
      });
    textArea.current.value = "";
  };

  const fetchPosts = () => {
    fetch("https://localhost/api/zweets")
      .then((res) => {
        if (res.status === 200) return res.json();
        else setError("Internal server error. Please try again.");
      })
      .then((json) => {
        setPosts(json);
      })
      .catch((err) => console.error(err));
  };

  return (
    <div className="zwizzerHome">
      {Object.keys(user).length > 0 && (
        <div id="homeNewPost">
          <NavLink
            className={"homeNewPostPhoto"}
            to={`/profile/${user.userId}`}
          >
            <img
              src={`https://localhost${user.profileImagePath}`}
              alt="Your photograph"
              className="homeNewPostPhoto"
            />
          </NavLink>
          <div className="homeNewPostTextAreaAndError">
            <textarea
              id="NewPost"
              ref={textArea}
              type="text"
              className="homeNewPostInput"
              placeholder="What is happening?!?"
              autoFocus
              minLength={1}
              maxLength={280}
              onChange={(e) => {
                setNewPostContent(e.target.value);
              }}
              onKeyUp={(e) => {
                if (e.key === "Enter") {
                  handleSubmit();
                }
              }}
            />
            {postError && <p className="postError">{postError}</p>}
          </div>
          <button className="homeNewPostSubmit" onClick={() => handleSubmit()}>
            Zweet
          </button>
        </div>
      )}
      {error && <div>{error}</div>}
      {posts && <Posts posts={posts} />}
    </div>
  );
}

export default Home;
