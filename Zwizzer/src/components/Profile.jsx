import React, { useEffect, useState } from "react";
import "./Profile.css";
import { useNavigate, useParams } from "react-router-dom";
import Posts from "./Posts.jsx";
//import { UserContext } from "../App";

function Profile({ logOut }) {
  //const user = useContext(UserContext);
  let { id } = useParams();
  const navigate = useNavigate();
  const [posts, setPosts] = useState([]);
  const [profile, setProfile] = useState({});
  const [selectedButton, setselectedButton] = useState(0);
  const [error, setError] = useState("");
  const [notFound, setNotFound] = useState(false);

  useEffect(() => {
    fetch(`https://localhost/api/Users/profile/${id}`)
      .then((res) => {
        if (res.status === 200) return res.json();
        else if (res.status === 404) {
          setError("No user found.");
          setNotFound(true);
        } else setError("Internal server Error. Please try again.");
      })
      .then((json) => {
        setProfile(json);
      })
      .catch((err) => console.error(err));
  }, [id]);

  return (
    <div id="profile">
      {notFound && error && navigate("/error/404")}
      {profile && (
        <>
          <header className="profileHeader">
            <button
              className="backButton"
              onClick={() => {
                navigate(-1);
              }}
            >
              <i className="fa-solid fa-arrow-left backIcon"></i>
            </button>
            <div className="profileName">
              <h2>{profile.userName}</h2>
              {profile.zweets && (
                <p className="postNumber">{profile.zweets.length} Zweets</p>
              )}
            </div>
          </header>
          <div className="imagesAndName">
            <img
              className="backgroundProfile"
              alt="Background Photograph"
              src={`https://localhost/${profile.backgroundImagePath}`}
            />
            <img
              className="photoProfile"
              alt="Profile Photograph"
              src={`https://localhost/${profile.profileImagePath}`}
            />
            <div className="nameBio">
              <h2>{profile.userName}</h2>
              <p>{profile.bio}</p>
            </div>
          </div>
          <div className="profileButtons">
            <button
              className={
                selectedButton === 1
                  ? "chooseProfilePosts chooseProfilePostsFocus"
                  : "chooseProfilePosts"
              }
              onClick={() => {
                setPosts(profile.zweets);
                setselectedButton(1);
              }}
            >
              Zweets
            </button>
            <button
              className={
                selectedButton === 2
                  ? "chooseProfilePosts chooseProfilePostsFocus"
                  : "chooseProfilePosts"
              }
              onClick={() => {
                setPosts(profile.rezweets);
                setselectedButton(2);
              }}
            >
              Rezweets
            </button>
            <button
              className={
                selectedButton === 3
                  ? "chooseProfilePosts chooseProfilePostsFocus"
                  : "chooseProfilePosts"
              }
              onClick={() => {
                setPosts(profile.comments);
                setselectedButton(3);
              }}
            >
              Comments
            </button>
            <button
              className={
                selectedButton === 4
                  ? "chooseProfilePosts chooseProfilePostsFocus"
                  : "chooseProfilePosts"
              }
              onClick={() => {
                setPosts(profile.likes);
                setselectedButton(4);
              }}
            >
              Likes
            </button>
          </div>
          <Posts posts={posts} logOut={logOut} />
        </>
      )}
    </div>
  );
}

export default Profile;
