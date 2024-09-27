import React, { useEffect, useState } from 'react'
import { ProductPostAPI } from '~/Services/ProductService'
import { toast } from 'react-toastify'
import { ProductGet } from '~/Models/Product'
import { useNavigate, useParams } from 'react-router-dom'
import FormAttrbute, { AttributeFormInput } from './FormAttrbute'
import { attributeGetByIdAPI, attributePostAPI, attributeUpdateAPI } from '~/Services/AttributeService'
import { AttributeGet } from '~/Models/Attribute'

const EditAttribute = () => {
    const [attribute, setAttribute] = useState<AttributeGet>()

    const { id } = useParams()
    const navigate = useNavigate();

    useEffect(() => {
        if (id) {
            attributeGetByIdAPI(id)
                .then(res => {
                    if (res?.data) {
                        setAttribute(res?.data)
                    }
                }).catch(error => toast.error(error))
        }
    }, [])

    const handleAttribute = (formInput: AttributeFormInput) => {
        if (id) {
            attributeUpdateAPI(id, formInput)
                .then(res => {
                    if (res?.status == 200) {
                        navigate("/admin/attributes")
                        toast.success("update successfully")
                    }
                }).catch(error => toast.error(error))
        }
    }


    return (
        <div className="bg-light rounded h-100 p-4">
            <h6 className="mb-4">Update a new attribue for variant</h6>

            {attribute
                ? <FormAttrbute handleAttribute={handleAttribute} attribute={attribute} />
                : <h1>VscLoading....</h1>}
        </div>
    )
}

export default EditAttribute
