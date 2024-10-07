import React, { useState } from 'react'
import { Outlet } from "react-router";
import Carousel from '~/Components/customer/Carousel/Carousel';
import Footer from '~/Components/customer/Footer/Footer';
import Header from '~/Components/customer/Header/Header';
import Navbar from '~/Components/customer/Navbar/Navbar';
import { UserProvider } from '~/Context/useAuth';
// import './CustomerLayout.css'


const CustomerLayout = () => {

  return (
    <UserProvider>
      <div>
        <Header />
        <Navbar />
        <main>
          <Outlet />
        </main>
        <Footer />
      </div>
    </UserProvider>
  )
}

export default CustomerLayout
