import React from 'react'
import { CartGet } from '~/Models/Cart'

type Props = {
    cart: CartGet,
    handleIncrease: (idCart: number) => void,
    handleDecrease: (idCart: number) => void,
    handleRemoveCart: (idCart: number) => void,
}

const baseUrl = 'https://localhost:7000/';

const CartItem = ({ cart, handleIncrease, handleDecrease, handleRemoveCart }: Props) => {
    return (
        <tr>
            <td className="align-middle"><img src={`${baseUrl}${cart.variant?.images[0].imageUrl}`} alt="" style={{ width: "50px" }} /></td>
            <td className="align-middle"> {cart.variant.variantName}</td>
            <td className="align-middle">
                <ul>
                    {cart.variant.values.map(value => <li>{`${value.attributeName}:${value.value1}`}</li>)}
                </ul>
            </td>
            <td className="align-middle">
                {cart.variant.salePrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</td>
            <td className="align-middle">
                <div className='d-flex align-items-center flex-column justify-content-center' >
                    <div className="input-group quantity mx-auto" style={{ width: "100px" }}>
                        <div className="input-group-btn">
                            <button onClick={() => handleDecrease(cart.cartId)} className="btn btn-sm btn-primary btn-minus" >
                                <i className="fa fa-minus"></i>
                            </button>
                        </div>
                        <input type="text" className="form-control form-control-sm bg-secondary border-0 text-center" value={cart.quantity} />
                        <div className="input-group-btn">
                            <button onClick={() => handleIncrease(cart.cartId)} className="btn btn-sm btn-primary btn-plus">
                                <i className="fa fa-plus"></i>
                            </button>
                        </div>
                    </div>
                    <p className='mt-3 fw-bold'>(available: {cart.variant.quantity})</p>
                </div>
            </td>
            <td className="align-middle">
                {cart?.totalPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}

            </td>
            <td className="align-middle">
                <button className="btn btn-sm btn-danger"
                        onClick={() => handleRemoveCart(cart.cartId)}
                    >
                    <i className="fa fa-times"></i>
                </button>
            </td>
        </tr>
    )
}

export default CartItem
