import React, { useEffect, useState } from 'react';
import { FaPen } from 'react-icons/fa';
import { FiTrash } from 'react-icons/fi';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import Table from '~/Components/admin/Table/Table';
import { BannerGet } from '~/Models/Banner';
import { bannerDeleteAPI, bannerGetAPI } from '~/Services/BannerService';

const baseUrl = 'https://localhost:7000/'; 

const Banner = () => {
    const navigate = useNavigate();
    const [banners, setBanners] = useState<BannerGet[]>([]);
    const [loading, setLoading] = useState<boolean>(true); // Loading state

    useEffect(() => {
        const fetchBanners = async () => {
            try {
                const res = await bannerGetAPI();
                if (res?.data) {
                    setBanners(res.data);
                }
            } catch (error) {
                toast.error('Error fetching banners');
            } finally {
                setLoading(false); // Set loading to false after fetching
            }
        };

        fetchBanners();
    }, []);

    const handleDelete = async (id: number) => {
        try {
            const res = await bannerDeleteAPI(id);
            if (res?.status === 204) {
                // Update state to remove the deleted banner
                setBanners((prevBanners) => prevBanners.filter(banner => banner.slideId !== id));
                toast.success("Delete successfully");
            }
        } catch (error) {
            toast.error('Error deleting banner');
        }
    }

    const configs = [
        {
            label: "#",
            render: (_: BannerGet, index: number) => index + 1,
        },
        {
            label: "Title",
            render: (banner: BannerGet) => banner.title,
        },
        {
            label: "Image",
            render: (banner: BannerGet) => (
                <img
                    className="rounded-circle img-fluid"
                    src={baseUrl + banner.imageUrl}
                    alt=""
                    style={{ width: '40px', height: '40px', objectFit: 'cover' }}
                />
            ),
        },
        {
            label: "Description",
            render: (banner: BannerGet) => banner.description,
        },
        {
            label: "Action",
            render: (banner: BannerGet) => (
                <div className='d-flex flex-start'>
                    <button
                        type="button"
                        className="btn btn-success d-flex align-items-center me-2"
                        onClick={() => navigate(`/admin/banner/edit/${banner.slideId}`)}
                    >
                        <FaPen />
                    </button>
                    <button
                        type="button"
                        className="btn btn-danger d-flex align-items-center me-2"
                        onClick={() => handleDelete(banner.slideId)}
                    >
                        <FiTrash />
                    </button>
                </div>
            ),
        }
    ];

    return (
        <div className='container-fluid pt-4 px-4'>
            <h1 className='py-3'>Banner Management</h1>
            <div className="col-12">
                <div className="custom-container rounded h-100 p-4">
                    <div className='d-flex py-2'>
                        <h6 className="mb-4">Banner List</h6>
                        <button
                            className='admin-btn-primary ms-auto'
                            onClick={() => navigate("/admin/slider/create")}
                        >
                            Create a new slider
                        </button>
                    </div>
                    <div className="table-responsive">
                        {loading ? (
                            <h1>Loading...</h1>
                        ) : (
                            <Table data={banners} configs={configs} />
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Banner;
