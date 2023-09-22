import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./ManageUsers.css";

function ManageUsers({ logOut }) {
  const navigate = useNavigate();

  const [users, setUsers] = useState([]);

  const handleEdit = (user, id) => {
    fetch("https://localhost/api/users/edit", {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
      },
      body: JSON.stringify({
        UserId: user.userId,
        Email: document.getElementById(`Email${id}`).value,
        UserRole: +document.getElementById(`Select${id}`).value,
        Bio: document.getElementById(`Bio${id}`).value,
      }),
    }).then((res) => {
      if (res.status === 204) fetchUsers();
    });
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = () => {
    fetch("https://localhost/api/users", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
      },
    })
      .then((res) => {
        if (res.status === 200) return res.json();
        else if (res.status === 401) logOut();
      })
      .then((json) => {
        setUsers(json);
      });
  };

  return (
    <div className="manageUsersPage">
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
          <h2>Manage Users</h2>
          {users.length > 0 && <p>{users.length} users</p>}
        </div>
      </header>
      <div className="userPageHolder">
        {users.length > 0 &&
          users.map((user, index) => {
            return (
              <div className="userPageUser" key={index}>
                <p>UserId: {user.userId}</p>
                <p>Username: {user.userName}</p>
                <div>
                  <p>Email: {user.email}</p>
                  <input
                    type="email"
                    minLength="1"
                    maxLength="320"
                    pattern="[a-z0-9._%+\-]+@[a-z0-9.\-]+\.[a-z]{2,}$"
                    required
                    id={`Email${index}`}
                    defaultValue={user.email}
                  />
                </div>
                <div>
                  <p>Bio: {user.bio}</p>
                  <input
                    type="text"
                    maxLength={160}
                    id={`Bio${index}`}
                    defaultValue={user.bio}
                  />
                </div>
                <div>
                  <p>
                    User role:{" "}
                    {user.userRole === 2
                      ? "Admin"
                      : user.userRole === 1
                      ? "Subscriber"
                      : "User"}
                  </p>
                  <select id={`Select${index}`}>
                    <option value={0}>User</option>
                    <option value={1}>Subscriber</option>
                    <option value={2}>Admin</option>
                  </select>
                </div>
                <button onClick={() => handleEdit(user, index)}>
                  Submit Edit
                </button>
              </div>
            );
          })}
      </div>
    </div>
  );
}

export default ManageUsers;
