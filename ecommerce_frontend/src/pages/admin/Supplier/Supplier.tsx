import React, { useEffect, useState } from 'react'
import { FaPen } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import Table from '~/Components/admin/Table/Table';
import { SupplierGet } from '~/Models/Supplier'
import { supplierGetAPI, supplietUpfateStatusAPI } from '~/Services/SupplierService';

const Supplier = () => {
    const [suppliers, setSuppliers] = useState<SupplierGet[]>([]);

    const navigate = useNavigate();

    useEffect(() => {
        getSuppliers()
    }, [])

    console.log(suppliers);




    const getSuppliers = () => {
        supplierGetAPI()
            .then(res => {
                if (res?.data) {
                    setSuppliers(res?.data);
                }
            }).
            catch(error => {
                toast.warning(error)
                setSuppliers([])
            })
    }

    const onStatusChange = (supplierID: number) => {
        updateStatusAPI(supplierID)

    }

    const updateStatusAPI = (supplierID: number) => {
        supplietUpfateStatusAPI(supplierID)
            .then(res => {
                if (res?.data) {
                    const updateSuppliers = suppliers.map(supllier => {
                        return supllier.supplierId == res?.data.supplierId
                            ? { ...supllier, status: res?.data.status }
                            : supllier
                    })

                    setSuppliers(updateSuppliers)
                }
            })
    }

    const configs = [
        {
            label: "#",
            render: (supplier: SupplierGet, index: number) => index + 1,
        },
        {
            label: "supplier's Name",
            render: (supplier: SupplierGet) => supplier.name,
        },
        {
            label: "supplier's Email",
            render: (supplier: SupplierGet) => supplier.email,
        },
        {
            label: "supplier's Phone",
            render: (supplier: SupplierGet) => supplier.phone,
        },
        {
            label: "supplier's Address",
            render: (supplier: SupplierGet) => supplier.address,
        },
        {
            label: "supplier's Status",
            render: (supplier: SupplierGet) =>
            (
                <td>
                    <div className="form-check form-switch">
                        <input className="form-check-input " type="checkbox" id="flexSwitchCheckDefault" onChange={() => onStatusChange(supplier.supplierId)} checked={supplier.status} />
                    </div>
                </td>
            )
            ,
        },

        {
            label: "Action",
            render: (supplier: SupplierGet) => {
                return <td className='d-flex' >
                    <button type="button"
                        className="btn-sm btn-success d-flex align-items-center me-2"
                        onClick={() => navigate(`/admin/supplier/edit/${supplier.supplierId}`)}>
                        <FaPen className='me-2' />
                        Update
                    </button>
                </td>
            }
        }
    ]


    return (
        <div className='container-fluid pt-4 px-4' >
            <h1 className='py-3' >Supplier Management</h1>
            <div className="col-12">
                <div className="rounded custom-container  h-100 p-4">
                    <div className='d-flex py-2' >
                        <h6 className="mb-4">Supplier List</h6>
                        <button className='btn btn-primary ms-auto'
                            onClick={() => { navigate("/admin/supplier/create") }}
                        >
                            Create a new supplier

                        </button>
                    </div>
                    <div className="table-responsive"></div>
                    <Table data={suppliers} configs={configs} />
                </div>
            </div>
        </div>
    )
}

export default Supplier
