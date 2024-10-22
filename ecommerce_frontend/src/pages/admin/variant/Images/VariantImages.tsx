import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import VariantImage from '~/Components/admin/VariantImage/VariantImage';
import { ImageGet } from '~/Models/Variant';
import { ImageDeleteAPI, ImageGetAPI, upLoadImagesAPI } from '~/Services/VariantService';
import UploadImages from './UploadImages';

const VariantImages = () => {
    const { id } = useParams<{ id: string }>(); // Ensure to type the id
    const [selectedImages, setSelectedImages] = useState<FileList | null>(null);
    const [images, setImages] = useState<ImageGet[]>([]);

    useEffect(() => {
        const fetchImages = async () => {
            if (id) {
                try {
                    const res = await ImageGetAPI(id);
                    if (res?.data) {
                        setImages(res.data);
                    } else {
                        toast.error('No images found for this variant.');
                    }
                } catch (error) {
                    toast.error('Error fetching images: ' + error);
                }
            }
        };

        fetchImages();
    }, [id]);

    const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSelectedImages(event.target.files);
    };

    const handleUpload = () => {
        if (id) {
            upLoadImagesAPI(selectedImages, Number(id))
                .then(res => {
                    if (res?.status == 200) {
                        setImages(res?.data)
                        toast.success("Images uploaded successfully!")
                    }
                }).catch(error => toast.error(error))
        }
    }

    const handleDelete = (idImage: number) => {
        if (id) {
            ImageDeleteAPI(idImage.toString(), id)
                .then(res => {
                    if (res?.data) {
                        setImages(res?.data)
                    }
                }).catch(err => toast.error(err))
        }
    }

    return (
        <div className="container-fluid service p-4" >
            <div className="container py-5 row flex-column" style={{ padding: "0 62px" }}>
                <div className="section-title mb-5 wow fadeInUp" data-wow-delay="0.2s">
                    <h4 className="display-4 text-center mb-4">Variant Images Management</h4>

                </div>

                <div className='d-flex justify-content-center' >

                    <UploadImages handleImageChange={handleImageChange} handleUpload={handleUpload} selectedImages={selectedImages} />
                </div>

                <div className="row g-4 justify-content-center">
                    {images.length > 0 ? (
                        images.map((image, index) => (
                            <VariantImage handleDelete={handleDelete} key={image.imageId} image={image} index={index + 1} />
                        ))
                    ) : (
                        <>Loading...</>
                    )}
                </div>
            </div>
        </div>
    );
};

export default VariantImages;
