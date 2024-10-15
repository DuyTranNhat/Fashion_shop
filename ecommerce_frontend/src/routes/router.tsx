import { createBrowserRouter } from 'react-router-dom';
import AdminLayout from '../layouts/AdminLayout/AdminLayout';
import CustomerLayout from '../layouts/CustomerLayout/CustomerLayout';
import Product from '../pages/admin/Product/Product';
import Shop from '../pages/customer/Shop/Shop';
import Category from '../pages/admin/Category/Category';
import Supplier from '~/pages/admin/Supplier/Supplier';
import InputSupplier from '~/pages/admin/Supplier/InputSupplier';
import EditSupplier from '~/pages/admin/Supplier/EditSupplier';
import InputProduct from '~/pages/admin/Product/InputProduct';
import EditProduct from '~/pages/admin/Product/EditProduct';
import Attribute from '~/pages/admin/Attribute/Attribute';
import InputAttribute from '~/pages/admin/Attribute/InputAttribute';
import EditAttribute from '~/pages/admin/Attribute/EditAttribute';
import Banner from '~/pages/admin/Banner/Banner';
import InputBanner from '~/pages/admin/Banner/InputBanner';
import EditBanner from '~/pages/admin/Banner/EditBanner';
import Variant from '~/pages/admin/variant/Variant';
import VariantImages from '~/pages/admin/variant/Images/VariantImages';
import InputVariant from '~/pages/admin/variant/InputVariant';
import EditVariant from '~/pages/admin/variant/EditVariant';
import Home from '~/pages/customer/Home/Home';
import Login from '~/pages/customer/Login/Login';
import LoginLayout from '~/layouts/LoginLayout/LoginLayout';
import '~/App.css'
import Register from '~/pages/customer/Register/Register';
import ProtectedRouteAdmin from './ProtectedRouteAdmin';
import VariantDetails from '~/pages/customer/VariantDetails/VariantDetails';
import Cart from '~/pages/customer/Cart/Cart';
import Profile from '~/pages/customer/Profile/Profile';
import Checkout from '~/pages/customer/Checkout/Checkout';
import CompletedCheckout from '~/pages/customer/Checkout/CompletedCheckout';

const router = createBrowserRouter([
    {
        
        path: "/admin",
        element: <AdminLayout />,
        children: [
            {
                
                path: "products",
                element: <ProtectedRouteAdmin>
                    <Product />
                </ProtectedRouteAdmin>,
            },
            {
                path: "categories",
                element: <ProtectedRouteAdmin>
                    <Category />
                </ProtectedRouteAdmin>,
            },
            {
                path: "suppliers",
                element: <ProtectedRouteAdmin>
                    <Supplier />
                </ProtectedRouteAdmin>,
            },
            {
                path: "supplier/create",
                element: <ProtectedRouteAdmin>
                    <InputSupplier />
                </ProtectedRouteAdmin>,
            },
            {
                path: "supplier/edit/:id",
                element: <ProtectedRouteAdmin>
                    <EditSupplier />
                </ProtectedRouteAdmin>,
            },
            {
                path: "product/create",
                element: <ProtectedRouteAdmin>
                    <InputProduct />
                </ProtectedRouteAdmin>,
            },
            {
                path: "product/edit/:id",
                element: <ProtectedRouteAdmin>
                    <EditProduct />
                </ProtectedRouteAdmin>,
            },
            {
                path: "attributes",
                element: <ProtectedRouteAdmin>
                    <Attribute />
                </ProtectedRouteAdmin>
            },
            {
                path: "attribue/create",
                element: <ProtectedRouteAdmin>
                    <InputAttribute />
                </ProtectedRouteAdmin>
            },
            {
                path: "attribute/edit/:id",
                element: <ProtectedRouteAdmin>
                    <EditAttribute />
                </ProtectedRouteAdmin>
            },
            {
                path: "slider",
                element: <ProtectedRouteAdmin>
                    <Banner />
                </ProtectedRouteAdmin>
            },
            {
                path: "slider/create",
                element: <ProtectedRouteAdmin>
                    <InputBanner />
                </ProtectedRouteAdmin>
            },
            {
                path: "banner/edit/:id",
                element: <ProtectedRouteAdmin>
                    <EditBanner />
                </ProtectedRouteAdmin>
            },
            {
                path: "variants",
                element: <ProtectedRouteAdmin>
                    <Variant />
                </ProtectedRouteAdmin>
            },
            {
                path: "variantImgages/:id",
                element: <ProtectedRouteAdmin>
                    <VariantImages />
                </ProtectedRouteAdmin>
            },
            {
                path: "variant/create",
                element: <ProtectedRouteAdmin>
                    <InputVariant />
                </ProtectedRouteAdmin>
            },
            {
                path: "variant/update/:id",
                element: <ProtectedRouteAdmin>
                    <EditVariant />
                </ProtectedRouteAdmin>
            },
            

        ],
    },
    {
        path: "",
        element: <CustomerLayout />,
        children: [
            {
                path: "",
                element: <Home />,
            },
            {
                path: "shop",
                element: <Shop />,
            },
            {
                path: "variantDetails/variant/:idVariant/product/:idProduct",
                element: <VariantDetails />,
            },
            {
                path: "cart",
                element: <ProtectedRouteAdmin>
                    <Cart />
                </ProtectedRouteAdmin>
            },
            {
                path: "profile",
                element: <ProtectedRouteAdmin>
                    <Profile />
                </ProtectedRouteAdmin>
            },
            {
                path: "checkout",
                element: <Checkout />,
            },
            {
                path: "completedCheckout/:idOrder",
                element: <CompletedCheckout />,
            },
        ],
    },
    {
        path: "/access",
        element: <LoginLayout />,
        children: [
            {
                path: "login",
                element: <Login />,
            },
            {
                path: "register",
                element: <Register />,
            }
        ],
    },
]);

export default router;
