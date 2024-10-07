import React, { useEffect, useState } from 'react'
import { FaPen } from 'react-icons/fa'
import { useNavigate } from 'react-router-dom'
import { toast } from 'react-toastify'
import Table from '~/Components/admin/Table/Table'
import { AttributeGet } from '~/Models/Attribute'
import { attributeActiveAPI, attributeGetAPI } from '~/Services/AttributeService'




const Attribute = () => {
    const [attribues, setAttributes] = useState<AttributeGet[]>([])
    const navigate = useNavigate()

    const activeStatus = (attributeId: number): void => { // Hàm trả về kiểu void
        attributeActiveAPI(attributeId)
            .then(res => {
                if (res?.data) {
                    setAttributes((prev: AttributeGet[]): AttributeGet[] => {  
                        return prev.map((attribute: AttributeGet): any => 
                            {
                                return attribute.attributeId === attributeId
                                ? { ...attribute, status: !attribute.status }  
                                : attribute}
                            );
                        });
                    }
            }).catch(error => toast.error("Failed to update status"));
    };



    useEffect(() => {
        attributeGetAPI()
            .then(res => {
                if (res?.data) {
                    
                    setAttributes(res?.data)
                }
            }).catch(error => toast.error(error))
    }, [])


    const configs = [
        {
            label: "#",
            render: (attributeGet: AttributeGet, index: number) => index + 1,
        },
        {
            label: "attributeGet's Name",
            render: (attributeGet: AttributeGet) => attributeGet.name,
        },
        {
            label: "attributeGet's Status",
            render: (attributeGet: AttributeGet) =>
            (
                <td>
                    <div className="form-check form-switch">
                        <input className="form-check-input " type="checkbox" id="flexSwitchCheckDefault"
                            onChange={() => activeStatus(attributeGet.attributeId)}
                            checked={attributeGet.status} />
                    </div>
                </td>
            )
            ,
        },
        {
            label: "Action",
            render: (attributeGet: AttributeGet) => {
                return <td className='d-flex' >
                    <button type="button"
                        className="btn-sm btn-success d-flex align-items-center me-2"
                        onClick={() => navigate(`/admin/attribute/edit/${attributeGet.attributeId}`)}>
                        <FaPen className='me-2' />
                        Update / details
                    </button>
                </td>
            }
        }
    ]

    return (
        <div className='container-fluid pt-4 px-4' >
            
            <h1>Category Management</h1>
            <div className="col-12">
                <div className="custom-container rounded h-100 p-4">
                    <div className='d-flex' >
                        <h6 className="mb-4">Attribute List</h6>
                        <button className='admin-btn-primary ms-auto'
                            onClick={() => { navigate("/admin/attribue/create") }}
                        >
                            Create a new attribute

                        </button>
                    </div>
                    <div className="table-responsive"></div>
                    <Table data={attribues} configs={configs} />
                </div>
            </div>
        </div>
    )
}

export default Attribute
