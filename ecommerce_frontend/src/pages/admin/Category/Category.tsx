import React, { useEffect, useState } from 'react'
import { CategoryGet } from '../../../Models/Category';
import { FaPen } from "react-icons/fa";
import { SlTrash } from "react-icons/sl";
import { toast } from 'react-toastify';
import { categoryGetAPI } from '~/Services/CatergoryService';
import RecursiveTable from '~/Components/admin/Table/RecursiveTable';

const configs = [
    {
        label: "#",
        render: (category: CategoryGet) => category.categoryId,
    },
    {
        label: "Category's Name",
        render: (category: CategoryGet) => category.name,
    },
    {
        label: "Category's Status",
        render: (category: CategoryGet) =>
        (
            <td>
                <div className="form-check form-switch">
                    <input className="form-check-input " type="checkbox" id="flexSwitchCheckDefault" checked={category.status} />
                </div>
            </td>
        )
        ,
    },

    {
        label: "Action",
        render: (Category: CategoryGet) => {
           return  <td className='d-flex' >
                <button type="button" className="btn-sm btn-success d-flex align-items-center me-2">
                    <FaPen className='me-2' />
                    Update
                </button>
            </td>
        }
    }

]

const Category = () => {
    const [catgories, setCategories] = useState<CategoryGet[]>([]);

    useEffect(() => {
        getCategories();
    }, [])

    const getCategories = () => {
        categoryGetAPI()
        .then(res => {
            if (res?.data)  
                setCategories(res.data)
        }).
        catch(error => {
            toast.warning(error)
            setCategories([])
        })
    }

    return (
        <div className='container-fluid custom-container pt-4 my-4' >
            <h1>Category RecursiveTable</h1>
            <RecursiveTable configs={configs} data={catgories} />
        </div>
    )
}

export default Category
