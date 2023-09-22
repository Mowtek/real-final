import React, { useContext } from "react";
import "./Posts.css";
import { NavLink } from "react-router-dom";
import { UserContext } from "../App";

function Posts(props) {
  const user = useContext(UserContext);
  const Posts = props.posts;

  return (
    <div className="PostsArea">
      {!Posts && <div>No data found.</div>}
      {Posts &&
        Posts.map((post, index) => {
          return (
            <div key={index}>
              {post.type !== "Comment" && post.comment == null && (
                <NavLink
                  className="postNavLink"
                  to={"/post/" + post.userId + "/" + post.zweetId}
                  key={index}
                  state={{ post: post }}
                >
                  <div className="postClass" key={index}>
                    <NavLink className="postImage">
                      <img
                        className="postImgActual"
                        alt="Authors Photograph"
                        src={`https://localhost/${post.userPhotoPath}`}
                      />
                    </NavLink>
                    <div className="postContent">
                      <NavLink
                        to={`/profile/${post.userId}`}
                        className={"nameAndDate"}
                      >
                        <h3 className="postsUserName">{post.userName}</h3>
                        <p className="postsCreatedTime">{post.creationTime}</p>
                      </NavLink>
                      <p className="postParagraph">{post.content}</p>
                      {Object.keys(user).length > 0 && (
                        <div className="postInteraction">
                          <div
                            className="interaction repost"
                            style={
                              (post.type === "Zweet" ||
                                post.type === "Rezweet" ||
                                post.type === "Like") &&
                              Object.keys(post).length > 0 &&
                              post.rezweetUsers !== null &&
                              post.rezweetUsers.some(
                                (e) => e.userId === user.userId
                              )
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
                            style={
                              (post.type === "Zweet" ||
                                post.type === "Rezweet" ||
                                post.type === "Like") &&
                              Object.keys(post).length > 0 &&
                              post.likeUsers !== null &&
                              post.likeUsers.some(
                                (e) => e.userId === user.userId
                              )
                                ? { color: "rgb(244, 101, 101)" }
                                : {}
                            }
                          >
                            <button className="interactionLike">
                              {(post.type === "Zweet" ||
                                post.type === "Rezweet" ||
                                post.type === "Like") &&
                              Object.keys(post).length > 0 &&
                              post.likeUsers !== null &&
                              post.likeUsers.some(
                                (e) => e.userId === user.userId
                              ) ? (
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
                </NavLink>
              )}
              {post.type === "Comment" && post.zweet !== null && (
                <div className="postComment" key={index}>
                  <div className="postClass">
                    <NavLink className="postImage">
                      <img
                        className="postImgActual"
                        alt="Authors Photograph"
                        src={`https://localhost/${post.userPhotoPath}`}
                      />
                    </NavLink>
                    <div className="postContent">
                      <NavLink
                        to={`/profile/${post.userId}`}
                        className={"nameAndDate"}
                      >
                        <h3 className="postsUserName">{post.userName}</h3>
                        <p className="postsCreatedTime">
                          {post.creationTime} |{" "}
                          <span style={{ color: "rgb(81, 174, 250)" }}>
                            Comment
                          </span>
                        </p>
                      </NavLink>
                      <p className="postParagraph">{post.content}</p>
                      <NavLink
                        to={`/post/${post.zweet.userId}/${post.zweet.zweetId}`}
                        className={"innerCommentZweetLink"}
                      >
                        <div className="innerCommentZweet">
                          <img
                            src={`https://localhost${post.zweet.userPhotoPath}`}
                            className="commentImage"
                            alt="Poster Photograph"
                          />
                          <div>
                            <div className="nameAndTimeComment">
                              <span style={{ fontWeight: "bold" }}>
                                {post.zweet.userName}
                              </span>
                              {" | "}
                              <span style={{ color: "gray" }}>
                                Original Zweet
                              </span>
                            </div>
                            <p className="nameAndTimeComment">
                              {post.zweet.content}
                            </p>
                          </div>
                        </div>
                      </NavLink>
                      {Object.keys(user).length > 0 && (
                        <div className="postInteraction">
                          <div
                            className={"interaction like"}
                            style={
                              Object.keys(post).length > 0 &&
                              post.likeUsers !== null &&
                              post.likeUsers.some(
                                (e) => e.userId === user.userId
                              )
                                ? {
                                    color: "rgb(244, 101, 101)",
                                    cursor: "auto",
                                  }
                                : { cursor: "auto" }
                            }
                          >
                            <button
                              className="interactionLike"
                              style={{ cursor: "auto" }}
                            >
                              {Object.keys(post).length > 0 &&
                              post.likeUsers !== null &&
                              post.likeUsers.some(
                                (e) => e.userId === user.userId
                              ) ? (
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
                </div>
              )}
            </div>
          );
        })}
    </div>
  );
}

export default Posts;
