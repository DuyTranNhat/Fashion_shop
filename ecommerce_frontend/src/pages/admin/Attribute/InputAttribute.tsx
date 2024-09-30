import React, { useEffect, useState } from 'react'
import { ProductPostAPI } from '~/Services/ProductService'
import { toast } from 'react-toastify'
import { ProductGet } from '~/Models/Product'
import { useNavigate } from 'react-router-dom'
import FormAttrbute, { AttributeFormInput } from './FormAttrbute'
import { attributePostAPI } from '~/Services/AttributeService'

const InputAttribute = () => {
    const navigate = useNavigate();


    const handleAttribute = (formInput: AttributeFormInput) => {
        attributePostAPI(formInput)
        .then(res => {
            if (res?.status == 201) {
                navigate("/admin/attributes")
                toast.success("add successfully")
            }
        }).catch(error => toast.error(error))
    }

    return (
            <div className="bg-light rounded h-100 p-4">
                <h6 className="mb-4">Create a new attribue for variant</h6>

                <FormAttrbute handleAttribute={handleAttribute}  />
            </div>
    )
}

export default InputAttribute
