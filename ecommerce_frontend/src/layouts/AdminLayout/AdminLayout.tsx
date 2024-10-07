import React from 'react'
import { Outlet } from "react-router";
import Navbar from '~/Components/admin/Navbar/Navbar';
import Sidebar from '~/Components/admin/Sidebar/Sidebar';
import { UserProvider } from '~/Context/useAuth';


const AdminLayout = () => {
    
    return (
        <UserProvider>
            <div className="container-fluid position-relative bg-white d-flex p-0">
                <Sidebar />
                <div className="content">
                    <Navbar />
                    <Outlet />
                </div>
            
            
            </div>
        </UserProvider>
    )
}

export default AdminLayout
