import React, { useState, useEffect, useContext } from "react";
import "./Post.css";
import { useParams, useNavigate, NavLink } from "react-router-dom";
import { UserContext } from "../App";

function Post({ logOut }) {
  const navigate = useNavigate();
  const user = useContext(UserContext);

  const { postId } = useParams();
  const [post, setPost] = useState({});
  const [content, setContent] = useState("");
  const [error, setError] = useState("");

  const fetchPost = () => {
    fetch(`https://localhost/api/zweets/${postId}`)
      .then((res) => {
        if (res.status === 200) return res.json();
        else navigate("/error/404");
      })
      .then((json) => setPost(json));
  };

  const handleSubmit = () => {
    fetch("https://localhost/api/zweets/comment", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
      },
      body: JSON.stringify({
        UserId: user.userId,
        ZweetId: post.zweetId,
        Content: content,
      }),
    }).then((res) => {
      if (res.status === 201) fetchPost();
      else if (res.status === 401) logOut();
      else setError("An error has occured. Please try again.");
    });
  };

  const handleLikePost = () => {
    fetch(`https://localhost/api/zweets/like/post`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
      },
      body: JSON.stringify({ UserId: user.userId, ZweetId: postId }),
    }).then((res) => {
      if (res.status === 201 || res.status === 204) {
        fetchPost();
      } else setError("Error has occured liking the post.");
    });
  };
  const handleLikeComment = (comment) => {
    fetch(`https://localhost/api/zweets/like/comment`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
      },
      body: JSON.stringify({
        UserId: user.userId,
        CommentId: comment.commentId,
      }),
    })
      .then((res) => {
        if (res.status === 201 || res.status === 204) fetchPost();
        else setError("Error has occured liking the post.");
      })
      .catch((err) => {
        console.error(err);
        logOut();
      });
  };

  const handleRezweet = () => {
    fetch("https://localhost/api/zweets/rezweet", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
      },
      body: JSON.stringify({ UserId: user.userId, ZweetId: post.zweetId }),
    })
      .then((res) => {
        if (res.status === 201 || res.status === 204) {
          fetchPost();
        } else setError("Error has occured rezweeting this post.");
      })
      .catch((err) => {
        console.error(err);
        logOut();
      });
  };

  useEffect(() => {
    fetch(`https://localhost/api/zweets/${postId}`)
      .then((res) => {
        if (res.status === 200) return res.json();
        else navigate("/error/404");
      })
      .then((json) => {
        setPost(json);
      });
  }, [postId, navigate]);

  return (
    <div className="postPage">
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
          <h2>Zweet by {post.userName}</h2>
          {post.comments && (
            <p className="postNumber">{post.comments.length} Comments</p>
          )}
        </div>
      </header>
      <div className="postClass">
        <NavLink className="postImage">
          <img
            className="postImgActual"
            alt="Authors Photograph"
            src={`https://localhost/${post.userPhotoPath}`}
          />
        </NavLink>
        <div className="postContent">
          <NavLink to={`/profile/${post.userId}`} className={"nameAndDate"}>
            <h3 className="postsUserName">{post.userName}</h3>
            <p className="postsCreatedTime">{post.creationTime}</p>
          </NavLink>
          {user.userRole === 2 && (
            <button
              className="postDeleteButton"
              onClick={() => {
                if (
                  window.confirm("Are you sure you want to delete this post?")
                ) {
                  fetch(`https://localhost/api/zweets/${post.zweetId}`, {
                    method: "DELETE",
                    headers: {
                      "Content-Type": "application/json",
                      Authorization: `Bearer ${localStorage.getItem(
                        "accessToken"
                      )}`,
                    },
                  }).then((res) => {
                    if (res.status === 204) navigate("/");
                    else logOut();
                  });
                }
              }}
            >
              X
            </button>
          )}
          <p className="postParagraph">{post.content}</p>
          {Object.keys(user).length > 0 && (
            <div className="postInteraction">
              <div
                className="interaction repost"
                onClick={() => handleRezweet()}
                style={
                  Object.keys(post).length > 0 &&
                  post.rezweetUsers !== null &&
                  post.rezweetUsers.some((e) => e.userId === user.userId)
                    ? { color: "rgb(87, 233, 87)" }
                    : {}
                }
              >
                <button className="interactionRepost">
                  <i className="fa-solid fa-retweet"></i>
                </button>
                <p>{post.rezweetCount}</p>
              </div>
              <div className="interaction comment">
                <button className="interactionComment">
                  <i className="fa-regular fa-comment"></i>
                </button>
                <p>{post.commentCount}</p>
              </div>
              <div
                className="interaction like"
                onClick={() => handleLikePost()}
                style={
                  Object.keys(post).length > 0 &&
                  post.likeUsers !== null &&
                  post.likeUsers.some((e) => e.userId === user.userId)
                    ? { color: "rgb(244, 101, 101)" }
                    : {}
                }
              >
                <button className="interactionLike">
                  {Object.keys(post).length > 0 &&
                  post.likeUsers !== null &&
                  post.likeUsers.some((e) => e.userId === user.userId) ? (
                    <i className="fa-solid fa-heart"></i>
                  ) : (
                    <i className="fa-regular fa-heart"></i>
                  )}
                </button>
                <p>{post.likeCount}</p>
              </div>
            </div>
          )}
        </div>
      </div>
      <div className="postCommentsSection">
        {Object.keys(user).length > 0 && (
          <div className="postNewComment">
            {error && (
              <p style={{ textAlign: "center", color: "red" }}>{error}</p>
            )}
            <NavLink
              className="homeNewPostPhoto"
              to={`/profile/${user.userId}`}
            >
              <img
                src={`https://localhost${user.profileImagePath}`}
                alt="Your Photograph"
                className="homeNewPostPhoto"
              />
            </NavLink>
            <textarea
              className="newCommentInput"
              maxLength="280"
              minLength="1"
              placeholder={`Write a comment for ${post.userName}`}
              onChange={(e) => {
                setContent(e.target.value);
              }}
              onKeyUp={(e) => {
                if (e.key === "Enter") {
                  handleSubmit();
                  e.target.value = "";
                }
              }}
            />
            <button
              className="homeNewPostSubmit"
              style={{ width: "fit-content" }}
              onClick={() => handleSubmit()}
            >
              Comment
            </button>
          </div>
        )}
        <div className="actualComments">
          {post.comments &&
            post.comments.map((comment, index) => {
              return (
                <div className="postComment" key={index}>
                  <div className="postClass">
                    <NavLink className="postImage">
                      <img
                        className="postImgActual"
                        alt="Authors Photograph"
                        src={`https://localhost/${comment.userPhotoPath}`}
                      />
                    </NavLink>
                    <div className="postContent">
                      <NavLink
                        to={`/profile/${comment.userId}`}
                        className={"nameAndDate"}
                      >
                        <h3 className="postsUserName">{comment.userName}</h3>
                        <p className="postsCreatedTime">
                          {comment.creationTime} |{" "}
                          <span style={{ color: "rgb(81, 174, 250)" }}>
                            Comment
                          </span>
                        </p>
                      </NavLink>
                      <p className="postParagraph">{comment.content}</p>
                      {Object.keys(user).length > 0 && (
                        <div className="postInteraction">
                          <div
                            className={"interaction like"}
                            style={
                              Object.keys(comment).length > 0 &&
                              comment.likeUsers !== null &&
                              comment.likeUsers.some(
                                (e) => e.userId === user.userId
                              )
                                ? { color: "rgb(244, 101, 101)" }
                                : {}
                            }
                            onClick={() => handleLikeComment(comment)}
                          >
                            <button className="interactionLike">
                              {Object.keys(comment).length > 0 &&
                              comment.likeUsers !== null &&
                              comment.likeUsers.some(
                                (e) => e.userId === user.userId
                              ) ? (
                                <i className="fa-solid fa-heart"></i>
                              ) : (
                                <i className="fa-regular fa-heart"></i>
                              )}
                            </button>
                            <p>{comment.likeCount}</p>
                          </div>
                        </div>
                      )}
                    </div>
                  </div>
                </div>
              );
            })}
        </div>
      </div>
    </div>
  );
}

export default Post;
