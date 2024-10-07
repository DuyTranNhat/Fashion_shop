import React, { useState } from 'react'
import { BannerGet } from '~/Models/Banner';

type Props = {
    banners: BannerGet[]
};

const baseUrl = 'https://localhost:7000/';

const Carousel = ({ banners }: Props) => {
    const [bannerActive, setBannerActive] = useState<number>(28)

    return (
        <div className="container-fluid mb-3">
            <div className="row px-xl-5">
                <div className="col-lg-8">
                    <div id="header-carousel" className="carousel slide carousel-fade mb-30 mb-lg-0" data-ride="carousel">
                        <ol className="carousel-indicators">
                            {banners.map((banner) => {
                                return <li data-target="#header-carousel"
                                    data-slide-to={banner.slideId}
                                    className={banner.slideId == bannerActive ? "active" : ""}
                                    onClick={() => setBannerActive(banner.slideId)}
                                ></li>
                            })}
                        </ol>



                        <div className="carousel-inner">
                            {banners.map(banner => {
                                return <div className={`carousel-item position-relative ${banner.slideId == bannerActive ? "active" : ""}`} style={{ height: "430px" }}>
                                    <img className="position-absolute w-100 h-100" src={baseUrl + banner.imageUrl} style={{ objectFit: "cover" }} />
                                    <div className="carousel-caption d-flex flex-column align-items-center justify-content-center">
                                        <div className="p-3" style={{ maxWidth: "700px" }}>
                                            <h1 className="display-4 text-white mb-3 animate__animated animate__fadeInDown">Men Fashion</h1>
                                            <p className="mx-md-5 px-5 animate__animated animate__bounceIn">Lorem rebum magna amet lorem magna erat diam stet. Sadips duo stet amet amet ndiam elitr ipsum diam</p>
                                            <a className="btn btn-outline-light py-2 px-4 mt-3 animate__animated animate__fadeInUp" href="#">Shop Now</a>
                                        </div>
                                    </div>
                                </div>
                            })}
                        </div>
                    </div>
                </div>
                <div className="col-lg-4">
                    <div className="product-offer mb-30" style={{ height: "200px" }}>
                        <img className="img-fluid" src="https://demo.htmlcodex.com/1479/online-shop-website-template/img/offer-1.jpg" alt="" />
                        <div className="offer-text">
                            <h6 className="text-white text-uppercase">Save 20%</h6>
                            <h3 className="text-white mb-3">Special Offer</h3>
                            <a href="" className="btn btn-primary">Shop Now</a>
                        </div>
                    </div>
                    <div className="product-offer mb-30" style={{ height: "200px" }}>
                        <img className="img-fluid" src="https://demo.htmlcodex.com/1479/online-shop-website-template/img/offer-2.jpg" alt="" />
                        <div className="offer-text">
                            <h6 className="text-white text-uppercase">Save 20%</h6>
                            <h3 className="text-white mb-3">Special Offer</h3>
                            <a href="" className="btn btn-primary">Shop Now</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Carousel
