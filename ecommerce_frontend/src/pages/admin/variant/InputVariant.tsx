import React from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import FormVariant, { VariantPostInput } from './FormVariant';
import { VariantPostAPI, upLoadImagesAPI } from '~/Services/VariantService';

const InputVariant = () => {
    const navigate = useNavigate();
    
    const handleSubmit = async (formInput: VariantPostInput, images: FileList | null) => {
        try {
            const response = await VariantPostAPI(formInput, images);

            if (response && response.data) {
                const variantId = response.data; 
                (response.data);
                

                if (images && images.length > 0 && variantId) {
                    await upLoadImagesAPI(images, variantId);
                    
                }

                toast.success('Variant created successfully!');
                navigate('/admin/variants'); 
            }
        } catch (error) {
            toast.error('An error occurred while creating the variant.');
        }
    };

    return (
        <div>
            <div className="custom-container m-4 rounded h-100 p-4">
                <h6 className="mb-4">Add a new variant</h6>
                <FormVariant isUpdate={false} handleVariant={handleSubmit} />
            </div>
        </div>
    );
};

export default InputVariant;
