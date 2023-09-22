import React, { useContext } from "react";
import { Navigate } from "react-router-dom";
import { UserContext } from "../App";

function ProtectedRoute({ children }) {
  const user = useContext(UserContext);
  return user.userRole === 2 ? children : <Navigate to="/error/401" />;
}

export default ProtectedRoute;
