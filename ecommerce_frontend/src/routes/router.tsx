import { createBrowserRouter } from 'react-router-dom';
import AdminLayout from '../layouts/AdminLayout';
import CustomerLayout from '../layouts/CustomerLayout';
import Product from '../pages/admin/Product';
import Shop from '../pages/customer/Shop';

const router = createBrowserRouter([
    {
        path: "/admin",
        element: <AdminLayout />,
        children: [
            {
                path: "products",
                element: <Product />,
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
