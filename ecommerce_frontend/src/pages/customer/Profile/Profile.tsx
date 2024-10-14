import React, { useEffect, useState } from 'react';
import { useAuth } from '~/Context/useAuth';
import { Customer } from '~/Models/Customer';
import { customerGetAPI, updateCustomerAPI } from '~/Services/CustomerService';
import { toast } from 'react-toastify';

const baseUrl = 'https://localhost:7000/';

const Profile = () => {
    const { user } = useAuth();
    const [customer, setCustomer] = useState<Customer>();
    const [imagePreview, setImagePreview] = useState<string | null>(null);
    const [image, setImage] = useState<File | null>(null);
    const [formData, setFormData] = useState({  
        name: '',
        email: '',
        phone: '',
        address: '',
    });

    useEffect(() => {
        if (user) {
            customerGetAPI(user?.customerId)
                .then(res => {
                    if (res?.data) {
                        setCustomer(res?.data);
                        setFormData({
                            name: res.data.name,
                            email: res.data.email,
                            phone: res.data.phone || '',
                            address: res.data.address || '',
                        });
                        // Set image preview to customer image URL
                        if (res.data.imageUrl) {
                            setImagePreview(baseUrl + res.data.imageUrl);
                        }
                    }
                });
        }
    }, [user]);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { id, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [id]: value,
        }));
    };

    const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files.length > 0) {
            setImage(e.target.files[0]);
            setImagePreview(URL.createObjectURL(e.target.files[0]));
        }
    };
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (!customer) return;

        const updatedProfile = new FormData();
        updatedProfile.append('name', formData.name);
        updatedProfile.append('email', formData.email);
        updatedProfile.append('phone', formData.phone);
        updatedProfile.append('address', formData.address);

        if (image) {
            updatedProfile.append('image', image);
        }

        updateCustomerAPI(customer.customerId, updatedProfile)
            .then(res => {
                if (res?.status === 200) {
                    toast.success('Profile updated successfully!');
                }
            })
            .catch(error => {
                toast.error('Failed to update profile.');
            });
    };


    return (
        customer ? (
            <div className="row px-xl-5">
                <div className="col-lg-7 mb-5" id="contactForm" onSubmit={handleSubmit}>
                    <form className="contact-form bg-light p-30">
                        <div className="control-group mt-3">
                            <input
                                type="text"
                                className="form-control"
                                id="name"
                                placeholder="Your Name"
                                required
                                value={formData.name}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div className="control-group mt-3">
                            <input
                                type="email"
                                className="form-control"
                                id="email"
                                placeholder="Your Email"
                                required
                                value={formData.email}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div className="control-group mt-3">
                            <input
                                type="text"
                                className="form-control"
                                id="phone"
                                required
                                placeholder="Your Phone"
                                value={formData.phone}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div className="control-group mt-3">
                            <textarea
                                className="form-control"
                                rows={8}
                                id="address"
                                placeholder="Address"
                                value={formData.address}
                                onChange={handleInputChange}
                            ></textarea>
                        </div>
                        <div>
                            <button className="btn btn-primary py-2 px-4  mt-3" type="submit">Save</button>
                        </div>
                    </form>
                </div>

                <div className="col-lg-5 mb-5">
                    <div className="bg-light p-30 mb-30 d-flex justify-content-center">
                        <img
                            style={{ width: "250px", height: "250px" }}
                            className='img-fluid object-fit rounded-2'
                            src={imagePreview ? imagePreview : `https://tse4.mm.bing.net/th?id=OIP.xo-BCC1ZKFpLL65D93eHcgHaGe&pid=Api&P=0&h=220`}
                            alt="Profile"
                        />
                    </div>
                    <div className="bg-light p-30 mb-3 d-flex justify-content-center">
                        <input type='file'
                            id="formFile"
                            accept="image/*"
                            onChange={handleImageChange} />
                    </div>
                </div>
            </div>
        ) : <h1>Loading</h1>
    );
};

export default Profile;
