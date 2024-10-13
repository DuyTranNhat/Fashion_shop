import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../Context/useAuth";
import { toast } from "react-toastify";
type Props = { children: React.ReactNode };
const ProtectedRouteAdmin = ({ children }: Props) => {
  const location = useLocation();
  const { isLoggedIn, user } = useAuth();
  return (isLoggedIn() && user?.role === 'admin') ? (
    <>{children}</>
  ) : (
    <>
      {toast.error("your access was denied")}
      <Navigate to="/access/login" state={{ from: location }} replace />
    </>
  );
};
export default ProtectedRouteAdmin;