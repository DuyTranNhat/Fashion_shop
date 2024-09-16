import React from 'react'
import { Outlet } from "react-router";

const CustomerLayout = () => {
  return (
    <div>
      <header>Customer Header</header>
      <aside>Customer Sidebar</aside>
      <main>
        <Outlet />
      </main>
    </div>
  )
}

export default CustomerLayout
