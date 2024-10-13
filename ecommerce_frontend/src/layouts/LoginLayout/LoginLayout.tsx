import React from 'react'
import { Outlet } from 'react-router'
import './LoginLayout.css'
import { UserProvider } from '~/Context/useAuth'

const LoginLayout = () => {
  return (
    <UserProvider>
      <div style={{background: "linear-gradient(90deg, #C7C5F4, #776BCC)"}} className="container-fluid position-relative bg-white d-flex p-0">
        <Outlet />
      </div>
    </UserProvider>
  )
}

export default LoginLayout
