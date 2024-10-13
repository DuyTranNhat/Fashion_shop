import React, { useEffect, useState } from 'react'
import { FaPen } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';
import Table from '~/Components/admin/Table/Table';
import { VariantGet } from '~/Models/Variant';
import { variantGetAPI } from '~/Services/VariantService'
import { RiProhibitedLine } from "react-icons/ri";
import { toast } from 'react-toastify';

const baseUrl = 'https://localhost:7000';


const Variant = () => {
    const navigate = useNavigate();
    const [variants, setVariants] = useState<VariantGet[]>([])


    useEffect(() => {
        variantGetAPI()
            .then(res => {
                if (res?.data) {
                    setVariants(res?.data)
                }
            }).catch(error => toast.error(error))
    }, [])


    const configs = [
        {
            label: "#",
            render: (variant: VariantGet, index: number) => index + 1,
        },
        {
            label: "Variant's Name",
            render: (variant: VariantGet) => variant.variantName,
        },
        {
            label: "Variant's Price",
            render: (variant: VariantGet) =>
                new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(variant.salePrice),
        },
        {
            label: "Variant's Quantity",
            render: (variant: VariantGet) => variant.quantity
        },

        {
            label: <p style={{ width: '160px' }} >Image</p>,
            render: (variant: VariantGet) => (
                <div >
                    {
                        variant.images.slice(0, 3).map(img => (
                            <img
                                className="rounded-circle img-fluid me-2"
                                src={baseUrl + "/" + img.imageUrl}
                                alt=""
                                style={{ width: '40px', height: '40px' }}
                            />
                        ))

                    }
                    <button type="button"
                        onClick={() => navigate("/admin/variantImgages/" + variant.variantId)}
                        className="btn-sm mt-3 btn-success p-2 d-flex align-items-center"
                    >
                        <FaPen className='me-2' /> Details
                    </button>
                </div>

            ),
        },

        {
            label: "Variant's Values",
            render: (variant: VariantGet) =>
            (
                <ul className="list-group list-group-flush">
                    {
                        variant.values.map(value =>
                        (
                            <>
                                <li className="list-group-item bg-transparent">{value.value1}</li>
                            </>
                        )
                        )
                    }

                </ul>
            ),
        },
        {
            label: "Option",
            render: (variant: VariantGet) =>
            (
                <div className="d-flex ">
                    <button type="button"
                        className="btn-sm btn-success d-flex align-items-center me-2"
                        onClick={() => navigate(`/admin/variant/update/${variant.variantId}`)}>
                        <FaPen className='me-2' />
                        Update
                    </button>

                    {variant.status == 'available'
                        ? (
                            <button type="button"
                                className="btn-sm btn-success d-flex align-items-center me-2"
                            >
                                <RiProhibitedLine className='me-2' />
                                {variant.status}
                            </button>
                        )
                        : (

                            <button type="button"
                                className="btn-sm btn-danger d-flex align-items-center me-2"
                            >
                                <RiProhibitedLine className='me-2' />
                                {variant.status}
                            </button>
                        )}
                </div>
            )
            ,
        },
    ]



    return (
        variants
            ? (<div className='container-fluid pt-4 px-4' >
                <h1 className='py-3' >Variant Management</h1>
                <div className="col-12">
                    <div className="rounded custom-container  h-100 p-4">
                        <div className='d-flex py-2' >
                            <h6 className="mb-4">Variant List</h6>
                            <button className='admin-btn-primary     ms-auto'
                                onClick={() => { navigate("/admin/variant/create") }}
                            >
                                Create a new Variant

                            </button>
                        </div>
                        <div className="table-responsive"></div>
                        <Table data={variants} configs={configs} />
                    </div>
                </div>
            </div>)
            : <>Loading</>
    )
}

export default Variant


