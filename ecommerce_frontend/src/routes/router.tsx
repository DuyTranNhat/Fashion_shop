import { createBrowserRouter } from 'react-router-dom';
import AdminLayout from '../layouts/AdminLayout';
import CustomerLayout from '../layouts/CustomerLayout';
import Product from '../pages/admin/Product/Product';
import Shop from '../pages/customer/Shop';
import Category from '../pages/admin/Category/Category';
import Supplier from '~/pages/admin/Supplier/Supplier'; 
import InputSupplier from '~/pages/admin/Supplier/InputSupplier';
import EditSupplier from '~/pages/admin/Supplier/EditSupplier';
import InputProduct from '~/pages/admin/Product/InputProduct';
import EditProduct from '~/pages/admin/Product/EditProduct';

const router = createBrowserRouter([
    {
        path: "/admin",
        element: <AdminLayout />,
        children: [
            {
                path: "products",
                element: <Product />,
            },
            {
                path: "categories",
                element: <Category />,
            },
            {
                path: "Supplier",
                element: <Supplier />,
            },
            {
                path: "supplier/create",
                element: <InputSupplier />,
            },
            {
                path: "supplier/edit/:id",
                element: <EditSupplier />,
            },
            {
                path: "product/create",
                element: <InputProduct />,
            },
            {
                path: "product/edit/:id",
                element: <EditProduct />,
            },
        ],
    },
    {
        path: "/",
        element: <CustomerLayout />,
        children: [
            {
                path: "shop",
                element: <Shop />,
            }
        ],
    },
]);

export default router;
