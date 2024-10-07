import React from 'react'
import { VariantGet } from '~/Models/Variant';

export type Props = {
    col: number;
    variant: VariantGet
}

const baseUrl = 'https://localhost:7000/';

const VariantItem = ({ col, variant } : Props) => {
    return (
        <div className={`col-lg-${col} col-md-6 col-sm-6 pb-1`}>
            <div className="product-item bg-light mb-4">
                <div className="product-img position-relative overflow-hidden">
                    <img className="img-fluid w-100" src={baseUrl + variant.images[0].imageUrl} alt="" />
                    <div className="product-action">
                        <a className="btn btn-outline-dark btn-square" href=""><i className="fa fa-shopping-cart"></i></a>
                        <a className="btn btn-outline-dark btn-square" href=""><i className="far fa-heart"></i></a>
                        <a className="btn btn-outline-dark btn-square" href=""><i className="fa fa-sync-alt"></i></a>
                        <a className="btn btn-outline-dark btn-square" href=""><i className="fa fa-search"></i></a>
                    </div>
                </div>
                <div className="text-center p-4">
                    <a className="h6 text-decoration-none multi-line-truncate" href=""> {variant.variantName}</a>
                    <div className="d-flex align-items-center justify-content-center mt-2">
                        <h5>{new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(variant.salePrice)}</h5><h6 className="text-muted ml-2"><del>(bổ xung sau)</del></h6>
                    </div>
                    <div className="d-flex align-items-center justify-content-center mb-1">
                        <small className="fa fa-star text-primary mr-1"></small>
                        <small className="fa fa-star text-primary mr-1"></small>
                        <small className="fa fa-star text-primary mr-1"></small>
                        <small className="fa fa-star text-primary mr-1"></small>
                        <small className="fa fa-star text-primary mr-1"></small>
                        <small>(99)</small>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default VariantItem
