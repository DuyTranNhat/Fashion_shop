import { createBrowserRouter } from 'react-router-dom';
import AdminLayout from '../layouts/AdminLayout';
import CustomerLayout from '../layouts/CustomerLayout';
import Product from '../pages/admin/Product/Product';
import Shop from '../pages/customer/Shop';
import Category from '../pages/admin/Category/Category';

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
