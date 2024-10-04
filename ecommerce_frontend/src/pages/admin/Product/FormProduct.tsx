import { yupResolver } from '@hookform/resolvers/yup';
import React, { useEffect, useState } from 'react';
import { useForm, useFieldArray } from 'react-hook-form';
import { toast } from 'react-toastify';
import * as yup from 'yup';
import { AttributeGet } from '~/Models/Attribute';
import { CategoryGet } from '~/Models/Category';
import { SupplierGet } from '~/Models/Supplier';
import { attributeGetAPI } from '~/Services/AttributeService';
import { categoryGetAPI } from '~/Services/CatergoryService';
import { supplierGetAPI } from '~/Services/SupplierService';

type Props = {
    product?: ProductFormInput | null;
    handleProduct: (form: ProductFormInput) => void;
    isUpdate?: boolean; 
};

export type CreateProuctAttributeDto = {
    attributeId: number
};

export type ProductFormInput = {
    categoryId: number;
    supplierId: number;
    name: string;
    description: string;
    attributes: CreateProuctAttributeDto[];
};

// Define validation schema using Yup
const validationSchema = yup.object().shape({
    name: yup.string().required('Name is required').max(255, 'Name is too long'),
    description: yup.string().required('Description is required').max(1000, 'Description is too long'),
    categoryId: yup.number().required('Please select a category').min(1, 'Invalid category selection'),
    supplierId: yup.number().required('Please select a supplier').min(1, 'Invalid supplier selection'),
    attributes: yup
        .array()
        .of(
            yup.object().shape({
                attributeId: yup.number().required('Attribute is required').min(1, 'Invalid attribute selection'),
            })
        )
        .min(1, 'At least one value is required') // Bắt buộc ít nhất 1 giá trị
        .required(),
});

const FormProduct = ({ handleProduct, product, isUpdate }: Props) => {
    // useForm hook with validation
    const [suppliers, setSuppliers] = useState<SupplierGet[]>([]);
    const [categories, setCategories] = useState<CategoryGet[]>([]);
    const [attributesGet, setAttributes] = useState<AttributeGet[]>([]);
    const [supplierValue, setSupplierValue] = useState<number>(product?.supplierId ?? 0);
    const [categoryValue, setCategoryValue] = useState<number>(product?.categoryId ?? 0);

    const {
        register,
        handleSubmit,
        control,
        formState: { errors },
    } = useForm<ProductFormInput>({
        resolver: yupResolver(validationSchema),
        defaultValues: product || { name: '', description: '', categoryId: 0, supplierId: 0, attributes: [] },
    });

    const { fields, append, remove } = useFieldArray({
        control,
        name: 'attributes',
    });

    const handleAttributeChange = (attributeId: number, checked: boolean) => {
        if (checked) {
            append({ attributeId });
        } else {
            const index = fields.findIndex((field) => field.attributeId === attributeId);
            if (index !== -1) remove(index);
        }
    };


    const handleCategoryChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedValue = Number(event.target.value);
        setCategoryValue(selectedValue);
    };

    // Handle supplier change
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

        attributeGetAPI()
            .then((res) => {
                if (res?.data) {
                    setAttributes(res?.data);
                }
            })
            .catch((error) => toast.error(error));
    }, []);

    // Handle form submission
    const onSubmit = (data: ProductFormInput) => {
        handleProduct(data);
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
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
                    style={{ height: '300px' }}
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

            {/* Attributes Section */}
            <div className="mb-3">
                <h6>Attributes</h6>
                {attributesGet.map((attr) => (
                    <div key={attr.attributeId} className="form-check">
                        <input
                            className="form-check-input"
                            type="checkbox"
                            value={attr.attributeId}
                            checked={fields.some((field) => field.attributeId === attr.attributeId)}
                            onChange={(e) => handleAttributeChange(attr.attributeId, e.target.checked)}
                        />
                        <label className="form-check-label" htmlFor={`attribute-${attr.attributeId}`}>
                            {attr.name}
                        </label>
                    </div>
                ))}
                {errors.attributes && <div className="invalid-feedback d-block">{errors.attributes.message}</div>}
            </div>

            {/* Submit Button */}
            <button type="submit" className="mt-4 btn btn-primary">
                Submit
            </button>
        </form>
    );
};

export default FormProduct;
