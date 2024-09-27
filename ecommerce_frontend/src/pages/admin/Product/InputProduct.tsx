import React from 'react'
import FormProduct, { ProductFormInput } from './FormProduct'
import { ProductPostAPI } from '~/Services/ProductService'
import { toast } from 'react-toastify'
import { ProductGet } from '~/Models/Product'
import { useNavigate } from 'react-router-dom'

const InputProduct = () => {
    const navigate = useNavigate();

    const handleProduct = (formInput: ProductFormInput) => {
        ProductPostAPI(formInput)
            .then(res => {
                if (res?.status == 201) {
                    toast.success("Add successfully!")
                    navigate("/admin/products")
                }
            })
    }

    return (
            <div className="bg-light rounded h-100 p-4">
                <h6 className="mb-4">Add a new model product</h6>

                <FormProduct handleProduct={handleProduct} />
            </div>
    )
}

export default InputProduct
