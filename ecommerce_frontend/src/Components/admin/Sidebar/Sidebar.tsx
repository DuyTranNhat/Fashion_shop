import React from 'react'
import { useNavigate } from 'react-router-dom'
import { useAuth } from '~/Context/useAuth'

const AdminSidebar = () => {
    const { user } = useAuth()
    const navigate = useNavigate()
    return (
        <div className="custom-container--right sidebar pb-3">
            <nav className="navbar navbar-light pl-0">
                <a href="index.html" className="navbar-brand mx-4 mb-3">
                    <h3 style={{color: "#0d6efd"}} className=""><i className="fa fa-hashtag me-2"></i>DASHMIN</h3>
                </a>
                <div className="d-flex align-items-center ms-4 mb-4">
                    <div className="position-relative">
                        <img className="rounded-circle" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSADESsnqLTB7q95kJhJXXqRra6IqT3zbBhRA&s" alt="" style={{width: "40px", height: "40px"}}/>
                            <div className="bg-success rounded-circle border border-2 border-white position-absolute end-0 bottom-0 p-1"></div>
                    </div>
                    <div className="ms-3">
                        <h6 className="mb-0">{user?.name}</h6>
                        <span>Admin</span>
                    </div>
                </div>
                <div className="navbar-nav w-100">
                    <a href="index.html" className="nav-item nav-link active"><i className="fa fa-tachometer-alt me-2"></i>Dashboard</a>
                    <div className="nav-item dropdown" style={{cursor:"pointer"}}>
                        <a className="nav-link dropdown-toggle" data-bs-toggle="dropdown"><i className="fa fa-laptop me-2"></i>Product</a>
                        <div className="dropdown-menu bg-transparent border-0">
                            <a onClick={() => navigate("/admin/products")}  className="dropdown-item no-color-change" >Module Product</a>
                            <a onClick={() => navigate("/admin/attributes")} className="dropdown-item no-color-change">attributes' Variant</a>
                            <a onClick={() => navigate("/admin/variants")} className="dropdown-item no-color-change">Variant</a>
                        </div>
                    </div>
                    <a onClick={() => navigate("/admin/products")} style={{cursor:"pointer"}} className="nav-item nav-link"><i className="fa fa-keyboard me-2"></i>Module Product</a>
                    <a onClick={() => navigate("/admin/attributes")} style={{cursor:"pointer"}} className="nav-item nav-link"><i className="fa fa-keyboard me-2"></i>Attributes</a>
                    <a onClick={() => navigate("/admin/variants")} style={{cursor:"pointer"}} className="nav-item nav-link"><i className="fa fa-keyboard me-2"></i>Variant</a>

                    <a onClick={() => navigate("/admin/suppliers")} style={{cursor:"pointer"}} className="nav-item nav-link"><i className="fa fa-keyboard me-2"></i>Supplier</a>
                    <a onClick={() => navigate("/admin/categories")} style={{cursor:"pointer"}} className="nav-item nav-link"><i className="fa fa-table me-2"></i>Categories</a>
                    <a href="chart.html" className="nav-item nav-link"><i className="fa fa-chart-bar me-2"></i>Charts</a>
                    <div className="nav-item dropdown" style={{cursor:"pointer"}}>
                        <a href="#" className="nav-link dropdown-toggle" data-bs-toggle="dropdown"><i className="far fa-file-alt me-2"></i>Banner</a>
                        <div className="dropdown-menu bg-transparent border-0">
                            <a onClick={() => navigate("/admin/slider")} className="dropdown-item no-color-change">Slider</a>
                            <a onClick={() => navigate("/admin/slider")} className="dropdown-item no-color-change">Slider</a>
                            <a onClick={() => navigate("/admin/slider")} className="dropdown-item no-color-change">Slider</a>
                            <a onClick={() => navigate("/admin/slider")} className="dropdown-item no-color-change">Slider</a>
                        </div>
                    </div>
                </div>
            </nav>
        </div>
    )
}

export default AdminSidebar
