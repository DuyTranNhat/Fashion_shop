import React, { useEffect, useState } from 'react';
import { useForm, Controller } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { toast } from 'react-toastify';
import { attributeGetAPI } from '~/Services/AttributeService';
import { AttributeGet } from '~/Models/Attribute';
import { ProductGetByID } from '~/Services/ProductService';

type Props = {
    handleVariant: (form: VariantPostInput, images: FileList | null) => void;
    initialData?: VariantPostInput | null;
    isUpdate: boolean; // Thêm prop để xác định là cập nhật hay tạo mới
};

export type ValueDto = {
    valueId: number;
};

export type VariantPostInput = {
    productId: number;
    variantName: string;
    importPrice: number;
    salePrice: number;
    values: ValueDto[];
};

// Validation schema using Yup  
const validationSchema = yup.object().shape({
    productId: yup.number().required('Please select a product').min(1, 'Invalid product selection'),
    variantName: yup.string().required('Variant name is required').max(255, 'Variant name is too long'),
    importPrice: yup.number().required('Import price is required').min(0, 'Price must be positive'),
    salePrice: yup.number().required('Sale price is required').min(0, 'Price must be positive'),
    values: yup.array().of(
        yup.object().shape({
            valueId: yup.number().required('Value is required').min(1, 'Invalid value selection'),
        })
    ).min(1, 'At least one value is required').required(),
});

const FormVariant = ({ handleVariant, initialData, isUpdate }: Props) => {
    const [attributes, setAttributes] = useState<AttributeGet[]>([]);
    const [selectedImages, setSelectedImages] = useState<FileList | null>(null);

    const {
        register,
        handleSubmit,
        control,
        setValue,
        getValues,
        formState: { errors },
    } = useForm<VariantPostInput>({
        resolver: yupResolver(validationSchema),
        defaultValues: initialData || {
            productId: 0,
            variantName: '',
            importPrice: 0,
            salePrice: 0,
            values: [],
        },
    });

    // Handle blur event on productId input to fetch attributes
    const handleProductIdBlur = () => {
        const productId = getValues('productId');
        if (productId && productId > 0) {
            ProductGetByID(productId.toString())
                .then((res) => {
                    if (res?.data) {
                        setAttributes(res.data.attributes);
                    } else if (!res) {
                        toast.error("Product ID Not Found!")
                        setAttributes([]);
                    }
                })
                .catch((error) => toast.error('Failed to fetch attributes: ' + error.message));
        }
    };

    const onSubmit = (data: VariantPostInput) => {
        handleVariant(data, selectedImages);
    };

    const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSelectedImages(event.target.files);
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            {/* Product ID Input */}
            <div className="form-floating mb-3">
                <input
                    type="number"
                    className={`form-control ${errors.productId ? 'is-invalid' : ''}`}
                    {...register('productId')}
                    placeholder="Product ID"
                    onBlur={handleProductIdBlur}
                    disabled={isUpdate} // Disable input if isUpdate
                />
                <label htmlFor="productId">Product ID</label>
                {errors.productId && <div className="invalid-feedback">{errors.productId.message}</div>}
            </div>

            {/* Variant Name Input */}
            <div className="form-floating mb-3">
                <input
                    type="text"
                    className={`form-control ${errors.variantName ? 'is-invalid' : ''}`}
                    {...register('variantName')}
                    placeholder="Variant Name"
                />
                <label htmlFor="variantName">Variant Name</label>
                {errors.variantName && <div className="invalid-feedback">{errors.variantName.message}</div>}
            </div>

            {/* Import Price Input */}
            <div className="form-floating mb-3">
                <input
                    type="number"
                    className={`form-control ${errors.importPrice ? 'is-invalid' : ''}`}
                    {...register('importPrice')}
                    placeholder="Import Price"
                    disabled={isUpdate} // Disable input if isUpdate
                />
                <label htmlFor="importPrice">Import Price</label>
                {errors.importPrice && <div className="invalid-feedback">{errors.importPrice.message}</div>}
            </div>

            {/* Sale Price Input */}
            <div className="form-floating mb-3">
                <input
                    type="number"
                    className={`form-control ${errors.salePrice ? 'is-invalid' : ''}`}
                    {...register('salePrice')}
                    placeholder="Sale Price"
                    disabled={isUpdate} // Disable input if isUpdate
                />
                <label htmlFor="salePrice">Sale Price</label>
                {errors.salePrice && <div className="invalid-feedback">{errors.salePrice.message}</div>}
            </div>

            {/* Attributes and Values Selection */}
            <div
                className={`mb-3 ${isUpdate && 'd-none'}`}
            >
                <h6>Values</h6>
                <div className="row">
                    {attributes.map((attribute) => (
                        <div key={attribute.attributeId} className="mb-2 col-4">
                            <strong>{attribute.name}</strong>
                            <Controller
                                name="values"
                                control={control}
                                render={({ field }) => {
                                    // Đảm bảo rằng field.value là một mảng ValueDto[]
                                    const valuesArray = Array.isArray(field.value) ? field.value : [];

                                    return (
                                        <select
                                            className={`form-select ${errors.values ? 'is-invalid' : ''}`}
                                            value={
                                                valuesArray.find((v: ValueDto) => attribute.values.some(val => val.valueId === v.valueId))?.valueId || ''
                                            }
                                            onChange={(e) => {
                                                const selectedValueId = Number(e.target.value);
                                                const updatedValues = valuesArray.filter(
                                                    (v: ValueDto) => v.valueId !== attribute.attributeId
                                                );

                                                if (selectedValueId > 0) {
                                                    updatedValues.push({ valueId: selectedValueId });
                                                }

                                                setValue('values', updatedValues);
                                            }}
                                        >
                                            <option value="">Select a value</option>
                                            {attribute.values.map((value) => (
                                                <option key={value.valueId} value={value.valueId}>
                                                    {value.value1}
                                                </option>
                                            ))}
                                        </select>
                                    );
                                }}
                            />
                        </div>
                    ))}
                    {errors.values && <div className="invalid-feedback d-block">{errors.values.message}</div>}
                </div>
            </div>

            {/* Multiple Image Upload */}
            <div
                className={`mb-3 ${isUpdate && 'd-none'}`}

            >
                <label htmlFor="fileImages" className="form-label">
                    Upload Images
                </label>
                <input
                    type="file"
                    className="form-control"
                    id="fileImages"
                    multiple
                    onChange={handleImageChange}
                />
            </div>

            {/* Submit Button */}
            <button type="submit" className="mt-4 btn btn-primary">
                Submit
            </button>
        </form>
    );
};

export default FormVariant;
