import React, { useEffect, useState } from 'react'
import { toast } from 'react-toastify';
import { useAuth } from '~/Context/useAuth'
import { CartGet } from '~/Models/Cart';
import { Customer } from '~/Models/Customer';
import { CartGetAPI } from '~/Services/CartService';
import { customerGetAPI } from '~/Services/CustomerService';
import { checkoutAPI } from '~/Services/OrderService';
import { PAYMENTMETHOD_COD } from '~/Utils/constant';

const Checkout = () => {
    const { user } = useAuth()
    const [cartList, setCartList] = useState<CartGet[]>([])
    const [customer, setCustomer] = useState<Customer>();
    const [completedOrder, setCompletedOrder] = useState<boolean>(true);

    const [formData, setFormData] = useState({
        name: '',
        phone: '',
        address: '',
    });

    useEffect(() => {
        if (user) {
            customerGetAPI(user?.customerId)
                .then(res => {
                    if (res?.data) {
                        setCustomer(res?.data);
                        setFormData({
                            name: res.data.name,
                            phone: res.data.phone || '',
                            address: res.data.address || '',
                        });
                    }
                });
        }

        CartGetAPI()
            .then(res => {
                if (res?.data) {
                    setCartList(res?.data)
                }
            })
    }, [user]);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!customer) return;

        const checkoutForm = new FormData();
        checkoutForm.append('name', formData.name);
        checkoutForm.append('phone', formData.phone);
        checkoutForm.append('address', formData.address);
        checkoutForm.append('paymentMethod', PAYMENTMETHOD_COD);


        try {
            const res = await checkoutAPI(customer.customerId, checkoutForm);
            if (res?.status === 200) {
                toast.success('Order placed successfully!');
                setCompletedOrder(true)
                // Additional logic if necessary (e.g., redirect to order confirmation page)
            }
        } catch (error) {
            toast.error('Failed to place order.');
        }
    };

    return (
        !completedOrder
            ? (<div className="container-fluid">
                <div className="row px-xl-5">
                    <div className="col-lg-6">
                        <h5 className="section-title position-relative text-uppercase mb-3">
                            <span className="bg-secondary pr-3">Billing Address</span>
                        </h5>
                        <div className="bg-light p-30 mb-5">
                            <form onSubmit={handleSubmit}>
                                <div className="form-group">
                                    <label>Name</label>
                                    <input
                                        name="name"
                                        value={formData.name}
                                        onChange={handleInputChange}
                                        className="form-control"
                                        type="text"
                                        placeholder="Your Name"
                                    />
                                </div>
                                <div className="form-group">
                                    <label>Phone</label>
                                    <input
                                        name="phone"
                                        value={formData.phone}
                                        onChange={handleInputChange}
                                        className="form-control"
                                        type="text"
                                        placeholder="Your Phone Number"
                                    />
                                </div>
                                <div className="form-group">
                                    <label>Address</label>
                                    <textarea
                                        name="address"
                                        value={formData.address}
                                        onChange={handleInputChange}
                                        rows={8}
                                        className="form-control"
                                        placeholder="Your Address"
                                    />
                                </div>
                            </form>
                        </div>
                    </div>
                    <div className="col-lg-6">
                        <h5 className="section-title position-relative text-uppercase mb-3">
                            <span className="bg-secondary pr-3">Order Total</span>
                        </h5>
                        <div className="bg-light p-30 mb-5">
                            <div className="border-bottom">
                                <h6 className="mb-3">Products</h6>
                                {cartList.map((item) => (
                                    <div key={item.variant.variantId} className="d-flex justify-content-between">
                                        <p style={{ width: '70%' }}>
                                            {`${item.variant.variantName} (${item.variant.values
                                                .map((value) => `${value.attributeName}: ${value.value1}`)
                                                .join(', ')})`}
                                        </p>
                                        <p>
                                            {item.quantity} X{' '}
                                            {item.variant.salePrice.toLocaleString('vi-VN', {
                                                style: 'currency',
                                                currency: 'VND',
                                            })}
                                        </p>
                                    </div>
                                ))}
                            </div>
                            <div className="border-bottom pt-3 pb-2">
                                <div className="d-flex justify-content-between mb-3">
                                    <h6>Subtotal</h6>
                                    <h6>
                                        {cartList
                                            .reduce((total, item) => total + item.totalPrice, 0)
                                            .toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}
                                    </h6>
                                </div>
                                <div className="d-flex justify-content-between">
                                    <h6 className="font-weight-medium">Shipping</h6>
                                    <h6 className="font-weight-medium">0</h6>
                                </div>
                            </div>
                            <div className="pt-2">
                                <div className="d-flex justify-content-between mt-2">
                                    <h5>Total</h5>
                                    <h5>
                                        {cartList
                                            .reduce((total, item) => total + item.totalPrice, 0)
                                            .toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}
                                    </h5>
                                </div>
                            </div>
                        </div>
                        <div className="mb-5">
                            <h5 className="section-title position-relative text-uppercase mb-3">
                                <span className="bg-secondary pr-3">Payment</span>
                            </h5>
                            <div className="bg-light p-30">
                                <button
                                    onClick={handleSubmit}
                                    className="btn btn-block btn-primary font-weight-bold py-3"
                                >
                                    Place Order
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>)
            : (
            <div className="container-fluid">
                <div className="row px-xl-5">
                    <div className="col-lg-8">
                        <h5 className="section-title position-relative text-uppercase mb-3"><span className="bg-secondary pr-3">Billing Address</span></h5>
                        <div className="bg-light p-30 mb-5">
                            <div className="row">
                                <h3>Cảm ơn bạn đã đặt hàng!</h3>
                                <h6 className="mb-3">Products</h6>
                                <div className="d-flex justify-content-between">
                                    <p>Product Name 1</p>
                                    <p>$150</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-4">
                        <h5 className="section-title position-relative text-uppercase mb-3"><span className="bg-secondary pr-3">Your Bills</span></h5>
                        <div className="bg-light p-30 mb-5">
                            <div className="border-bottom">
                                <h6 className="mb-3">Products</h6>
                                <div className="d-flex justify-content-between">
                                    <p>Product Name 1</p>
                                    <p>$150</p>
                                </div>
                                <div className="d-flex justify-content-between">
                                    <p>Product Name 2</p>
                                    <p>$150</p>
                                </div>
                                <div className="d-flex justify-content-between">
                                    <p>Product Name 3</p>
                                    <p>$150</p>
                                </div>
                            </div>
                            <div className="border-bottom pt-3 pb-2">
                                <div className="d-flex justify-content-between mb-3">
                                    <h6>Subtotal</h6>
                                    <h6>$150</h6>
                                </div>
                                <div className="d-flex justify-content-between">
                                    <h6 className="font-weight-medium">Shipping</h6>
                                    <h6 className="font-weight-medium">$10</h6>
                                </div>
                            </div>
                            <div className="pt-2">
                                <div className="d-flex justify-content-between mt-2">
                                    <h5>Total</h5>
                                    <h5>$160</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            )
    );
};

export default Checkout;
