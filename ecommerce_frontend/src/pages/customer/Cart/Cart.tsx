import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { toast } from 'react-toastify'
import CartList from '~/Components/customer/Cart/CartList'
import { CartGet } from '~/Models/Cart'
import { CartGetAPI, decreaseQuantityAPI, increaseQuantityAPI, removeCartAPI } from '~/Services/CartService'

const Cart = () => {
    const [cartList, setCartList] = useState<CartGet[]>([])
    const navigate = useNavigate()

    useEffect(() => {
        CartGetAPI()
            .then(res => {
                if (res?.data) {
                    setCartList(res?.data)
                }
            })
    }, [])

    const handleDecrease = (idCart: number) => {
        decreaseQuantityAPI(idCart)
            .then((res: any) => {
                if (res?.status === 201) {
                    const data: CartGet = res?.data;
                    setCartList(prev => {
                        return prev.map(item =>
                            item.cartId === data.cartId
                                ? data : item
                        );
                    });
                }
            })
            .catch(error => toast.error(error));
    }


    const handleIncrease = (idCart: number) => {
        increaseQuantityAPI(idCart)
            .then(res => {
                if (res?.status === 201) {
                    if (res?.status === 201) {
                        const data: CartGet = res?.data
                        setCartList(prev => {
                            return prev.map(item =>
                                item.cartId === data.cartId
                                    ? data : item
                            )
                        })
                    }
                }
            }).catch(error => toast.error(error))
    }

    const handleRemoveCart = (idCart: number) => {
        removeCartAPI(idCart)
            .then(res => {
                if (res?.status === 200) {
                    toast.success(res?.data.message)
                    const newCartList = cartList.filter(item => item.cartId !== idCart)
                    setCartList(newCartList)
                }
            }).catch(error => toast.error(error))
    }

    return (
        <div className="container-fluid">
            <div className="row px-xl-5">
                <div className="col-lg-9 table-responsive mb-5">
                    <table className="table table-light table-borderless table-hover text-center mb-0">
                        <thead className="thead-dark">
                            <tr>
                                <th></th>
                                <th>Products</th>
                                <th>Attributes</th>
                                <th>Price</th>
                                <th>Quantity</th>
                                <th>Total</th>
                                <th>Remove</th>
                            </tr>
                        </thead>
                        <tbody className="align-middle">
                            {
                                cartList ? (
                                    <CartList handleRemoveCart={handleRemoveCart} handleDecrease={handleDecrease} handleIncrease={handleIncrease} carts={cartList} />
                                ) : <>Loading</>
                            }
                        </tbody>
                    </table>
                </div>
                <div className="col-lg-3">
                    <form className="mb-30" action="">
                        <div className="input-group">
                            <input type="text" className="form-control border-0 p-4" placeholder="Coupon Code" />
                            <div className="input-group-append">
                                <button className="btn btn-primary">Apply Coupon</button>
                            </div>
                        </div>
                    </form>
                    <h5 className="section-title position-relative text-uppercase mb-3"><span className="bg-secondary pr-3">Cart Summary</span></h5>
                    <div className="bg-light p-30 mb-5">
                        <div className="border-bottom pb-2">
                            <div className="d-flex justify-content-between mb-3">
                                <h6>Subtotal</h6>
                                <h6> {cartList.reduce((total, item) => {
                                    return total + item.totalPrice;
                                }, 0).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}
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
                                    {cartList.reduce((total, item) => {
                                        return total + item.totalPrice;
                                    }, 0).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}
                                </h5>
                            </div>
                            <button
                                onClick={() => navigate("/checkout")}
                                className="btn btn-block btn-primary font-weight-bold my-3 py-3
                                ">
                                Checkout (COD)
                            </button>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    )
}

export default Cart
