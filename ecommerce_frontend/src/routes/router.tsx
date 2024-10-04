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
import Attribute from '~/pages/admin/Attribute/Attribute';
import InputAttribute from '~/pages/admin/Attribute/InputAttribute';
import EditAttribute from '~/pages/admin/Attribute/EditAttribute';
import { Slide } from 'react-toastify';
import Banner from '~/pages/admin/Banner/Banner';
import InputBanner from '~/pages/admin/Banner/InputBanner';
import EditBanner from '~/pages/admin/Banner/EditBanner';
import Variant from '~/pages/admin/variant/Variant';
import VariantImages from '~/pages/admin/variant/Images/VariantImages';
import InputVariant from '~/pages/admin/variant/InputVariant';
import EditVariant from '~/pages/admin/variant/EditVariant';


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
                path: "suppliers",
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
            {
                path: "attributes",
                element: <Attribute />
            },
            {
                path: "attribue/create",
                element: <InputAttribute />
            },
            {
                path: "attribute/edit/:id",
                element: <EditAttribute />
            },
            {
                path: "slider",
                element: <Banner />
            },
            {
                path: "slider/create",
                element: <InputBanner />
            },
            {
                path: "banner/edit/:id",
                element: <EditBanner />
            },
            {
                path: "variants",
                element: <Variant />
            },
            {
                path: "variantImgages/:id",
                element: <VariantImages />
            },
            {
                path: "variant/create",
                element: <InputVariant />
            },
            {
                path: "variant/update/:id",
                element: <EditVariant />
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
