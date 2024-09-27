import { yupResolver } from '@hookform/resolvers/yup';
import React, { useEffect, useState } from 'react'
import { useForm } from 'react-hook-form';
import { toast } from 'react-toastify';
import * as yup from 'yup'
import { CategoryGet } from '~/Models/Category';
import { SupplierGet } from '~/Models/Supplier';
import { categoryGetAPI } from '~/Services/CatergoryService';
import { supplierGetAPI } from '~/Services/SupplierService';


type Props = {
    product?: ProductFormInput | null;
    handleProduct: (form: ProductFormInput) => void;
}

export type ProductFormInput = {
    categoryId: number;
    supplierId: number;
    name: string;
    description: string;
}

// Define validation schema using Yup
const validationSchema = yup.object().shape({
    name: yup.string().required('Name is required').max(255, 'Name is too long'),
    description: yup.string().required('Description is required').max(1000, 'Description is too long'),
    categoryId: yup.number().required('Please select a category').min(1, 'Invalid category selection'),
    supplierId: yup.number().required('Please select a supplier').min(1, 'Invalid supplier selection'),
});

const FormProduct = ({ handleProduct, product }: Props) => {
    // useForm hook with validation
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<ProductFormInput>({
        resolver: yupResolver(validationSchema),
        defaultValues: product || { name: "", description: "", categoryId: 0, supplierId: 0 },
    });

    const [suppliers, setSuppliers] = useState<SupplierGet[]>([]);
    const [categories, setCategories] = useState<CategoryGet[]>([]);
    const [supplierValue, setSupplierValue] = useState<number>(product?.supplierId ?? 0)
    const [categoryValue, setCategoryValue] = useState<number>(product?.categoryId ?? 0)


    const handleCategoryChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedValue = Number(event.target.value);
        setCategoryValue(selectedValue);
    };

    // Hàm xử lý thay đổi supplier
    const handleSupplierChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedValue = Number(event.target.value);
        setSupplierValue(selectedValue);
    };

    // Fetch categories and suppliers on component mount
    useEffect(() => {
        supplierGetAPI()
            .then((res) => {
                if (res?.data) {
                    setSuppliers(res?.data);
                }
            })
            .catch((error) => toast.error(error));

        categoryGetAPI()
            .then((res) => {
                if (res?.data) {
                    setCategories(res?.data);
                }
            })
            .catch((error) => toast.error(error));
    }, []);

    // Handle form submission
    const onSubmit = (data: ProductFormInput) => {
        handleProduct(data);
    };

    return (
        <form action="" onSubmit={handleSubmit(onSubmit)}>
            {/* Name Input */}
            <div className="form-floating mb-3">
                <input
                    type="text"
                    className={`form-control ${errors.name ? 'is-invalid' : ''}`}
                    placeholder="Product Name"
                    {...register('name')}
                />
                <label htmlFor="name">Name Product</label>
                {errors.name && <div className="invalid-feedback">{errors.name.message}</div>}
            </div>

            {/* Description Input */}
            <div className="form-floating mb-3">
                <textarea
                    className={`form-control ${errors.description ? 'is-invalid' : ''}`}
                    style={{height: "300px"}}
                    placeholder="Product Description"
                    {...register('description')}
                />
                <label htmlFor="description">Description Product</label>
                {errors.description && <div className="invalid-feedback">{errors.description.message}</div>}
            </div>

            {/* Category Select */}
            <div className="form-floating mb-3">
                <select
                    className={`form-select ${errors.categoryId ? 'is-invalid' : ''}`}
                    value={categoryValue ?? '0'}
                    {...register('categoryId')}
                    onChange={handleCategoryChange}
                >
                    <option value="0">Select Category</option>
                    {categories.map((category) => (
                        <option key={category.categoryId} value={category.categoryId}>
                            {category.name}
                        </option>
                    ))}
                </select>
                <label htmlFor="categoryId">Category</label>
                {errors.categoryId && <div className="invalid-feedback">{errors.categoryId.message}</div>}
            </div>

            {/* Supplier Select */}
            <div className="form-floating mb-3">
                <select
                    className={`form-select ${errors.supplierId ? 'is-invalid' : ''}`}
                    {...register('supplierId')}
                    value={supplierValue ?? 0} 
                    onChange={handleSupplierChange}
                >
                    <option value="0">Select Supplier</option>
                    {suppliers.map((supplier) => (
                        <option key={supplier.supplierId} value={supplier.supplierId}>
                            {supplier.name}
                        </option>
                    ))}
                </select>
                <label htmlFor="supplierId">Supplier</label>
                {errors.supplierId && <div className="invalid-feedback">{errors.supplierId.message}</div>}
            </div>

            {/* Submit Button */}
            <button type="submit" className="btn btn-primary">
                Submit
            </button>
        </form>
    );
};

export default FormProduct;