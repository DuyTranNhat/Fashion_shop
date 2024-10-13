import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import FormVariant, { VariantPostInput } from './FormVariant';
import { VariantGetByIdAPI, VariantUpdateAPI, upLoadImagesAPI } from '~/Services/VariantService';

const EditVariant = () => {
    const { id } = useParams<{ id: string }>(); // Assuming you pass the variant ID in the URL
    const navigate = useNavigate();
    const [initialData, setInitialData] = useState<VariantPostInput | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchVariant = async () => {
            try {
                if (id) {
                    const response = await VariantGetByIdAPI(id);
                    if (response && response.data) {
                        setInitialData({
                            productId: response.data.productId,
                            variantName: response.data.variantName,
                            importPrice: response.data.importPrice,
                            salePrice: response.data.salePrice,
                            values: response.data.values, // Adjust this according to your API response
                        });
                    } else {
                        toast.error('Variant not found!');
                    }
                }
            } catch (error) {
                toast.error('Failed to fetch variant details.');
            } finally {
                setLoading(false);
            }
        };

        fetchVariant();
    }, [id]);

    const handleSubmit = async (formInput: VariantPostInput, images: FileList | null) => {
        try {
            if (id) {
                const response = await VariantUpdateAPI(id, formInput); // Adjust API function as necessary
                if (response && response.data) {
                    const variantId = response.data;
    
                    if (images && images.length > 0) {
                        await upLoadImagesAPI(images, variantId);
                    }
    
                    toast.success('Variant updated successfully!');
                    navigate('/admin/variants'); 
                }
            }
        } catch (error) {
            toast.error('An error occurred while updating the variant.');
        }
    };

    if (loading) {
        return <div>Loading...</div>; // Show a loading state while fetching data
    }

    return (
        <div>
            <div className="custom-container m-4 rounded h-100 p-4">
                <h6 className="mb-4">Edit Variant</h6>
                {initialData && (
                    <FormVariant handleVariant={handleSubmit} initialData={initialData} isUpdate={true} />
                )}
            </div>
        </div>
    );
};

export default EditVariant;
