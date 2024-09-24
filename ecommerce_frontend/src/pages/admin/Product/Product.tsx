import React, { useEffect, useState } from 'react'
import { FaPen } from 'react-icons/fa'
import { useNavigate } from 'react-router-dom'
import Table from '~/Components/admin/Table/Table'
import { ProductGet } from '~/Models/Product'
import { ProductGetAPI } from '~/Services/ProductService'




const Product = () => {
    const [products, setProducts] = useState<ProductGet[]>([])
    const navigate = useNavigate()

    useEffect(() => {
        ProductGetAPI()
            .then(res => {
                if (res?.data) {
                    setProducts(res?.data)
                    console.log(res?.data);

                }
            })
    }, [])

    const configs = [
        {
            label: "#",
            render: (product: ProductGet, index: number) => index + 1,
        },
        {
            label: "Product's Name",
            render: (product: ProductGet) => <p style={{width: '500px'}} >{product.name}</p>,
        },
        {
            label: "Product's Category",
            render: (product: ProductGet) => product.categoryDto.name,
        },
        {
            label: "Product's Supplier",
            render: (product: ProductGet) => product.supplierDto.name,
        },
        {
            label: "Action",
            render: (product: ProductGet) => {
                return <td className='d-flex' >
                    <button type="button"
                        className="btn-sm btn-success d-flex align-items-center me-2"
                        onClick={() => navigate(`/admin/product/edit/${product.productId}`)}>
                        <FaPen className='me-2' />
                        Update
                    </button>
                </td>
            }
        }
    ]
    

    return (
        <div className='container-fluid pt-4 px-4' >

            <div className="col-12">
                <div className="bg-light rounded h-100 p-4">
                    <div className='d-flex' >
                        <h6 className="mb-4">Product Sample List</h6>
                        <button className='btn btn-primary ms-auto'
                            onClick={() => { navigate("/admin/product/create") }}
                        >
                            Create a new sample

                        </button>
                    </div>
                    <div className="table-responsive"></div>
                    <Table data={products} configs={configs} />
                </div>
            </div>
        </div>
    )
}

export default Product
