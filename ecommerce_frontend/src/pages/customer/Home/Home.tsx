import React, { useEffect, useState } from 'react'
import { toast } from 'react-toastify'
import Carousel from '~/Components/customer/Carousel/Carousel'
import VariantList from '~/Components/customer/Variant/VariantList'
import { BannerGet } from '~/Models/Banner'
import { VariantGet } from '~/Models/Variant'
import { bannerGetAPI } from '~/Services/BannerService'
import { variantGetAPI } from '~/Services/VariantService'

const Home = () => {
    const [banners, setBanners] = useState<BannerGet[]>()
    const [variants, setVariants] = useState<VariantGet[]>()

    useEffect(() => {
        bannerGetAPI()
            .then(res => {
                if (res?.data) {
                    setBanners(res?.data)
                }
            }).catch(err => toast.error(err))

        variantGetAPI()
            .then(res => {
                if (res?.data) {
                    setVariants(res?.data)
                }
            }).catch(err => toast.error(err))
    }, [])

    return (
        <div>
            {(banners && variants) ?
                <div>
                    <Carousel banners={banners} />


                    <div className="container-fluid pt-5">
                        <div className="row px-xl-5 pb-3">
                            <div className="col-lg-3 col-md-6 col-sm-12 pb-1">
                                <div className="d-flex align-items-center bg-light mb-4" style={{ padding: "30px" }}>
                                    <h1 className="fa fa-check text-primary m-0 mr-3"></h1>
                                    <h5 className="font-weight-semi-bold m-0">Quality Product</h5>
                                </div>
                            </div>
                            <div className="col-lg-3 col-md-6 col-sm-12 pb-1">
                                <div className="d-flex align-items-center bg-light mb-4" style={{ padding: "30px" }}>
                                    <h1 className="fa fa-shipping-fast text-primary m-0 mr-2"></h1>
                                    <h5 className="font-weight-semi-bold m-0">Free Shipping</h5>
                                </div>
                            </div>
                            <div className="col-lg-3 col-md-6 col-sm-12 pb-1">
                                <div className="d-flex align-items-center bg-light mb-4" style={{ padding: "30px" }}>
                                    <h1 className="fas fa-exchange-alt text-primary m-0 mr-3"></h1>
                                    <h5 className="font-weight-semi-bold m-0">14-Day Return</h5>
                                </div>
                            </div>
                            <div className="col-lg-3 col-md-6 col-sm-12 pb-1">
                                <div className="d-flex align-items-center bg-light mb-4" style={{ padding: "30px" }}>
                                    <h1 className="fa fa-phone-volume text-primary m-0 mr-3"></h1>
                                    <h5 className="font-weight-semi-bold m-0">24/7 Support</h5>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div className="container-fluid pt-5 pb-3">
                        <h2 className="section-title position-relative text-uppercase mx-xl-5 mb-4"><span className="bg-secondary pr-3">Featured Products</span></h2>
                        <VariantList variantList={variants} col={3} />
                    </div>

                    <div className="container-fluid pt-5 pb-3">
                        <div className="row px-xl-5">
                            <div className="col-md-6">
                                <div className="product-offer mb-30" style={{ height: "300px" }}>
                                    <img className="" src="https://demo.htmlcodex.com/1479/online-shop-website-template/img/offer-1.jpg" alt="" />
                                    <div className="offer-text">
                                        <h6 className="text-white text-uppercase">Save 20%</h6>
                                        <h3 className="text-white mb-3">Special Offer</h3>
                                        <a href="" className="btn btn-primary">Shop Now</a>
                                    </div>
                                </div>
                            </div>
                            <div className="col-md-6">
                                <div className="product-offer mb-30" style={{ height: "300px" }}>
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
                </div>

                : <>Loading</>}
        </div>
    )
}

export default Home
