import React from 'react'
import SliderImage from './SliderImage'
import { VariantGet } from '~/Models/Variant'

export type Props = {
    VariantList: VariantGet[]
    variantSelected: VariantGet
}

const SliderImageList = ({ VariantList, variantSelected }: Props) => {
    return (
        <div
            id="header-carousel"
            className="carousel slide carousel-fade mb-30 mb-lg-0"
            data-bs-ride="carousel"
        >
            {/* Nút điều hướng bên trái */}
            <a style={{ zIndex: "100" }} className="carousel-control-prev" href="#header-carousel" role="button" data-bs-slide="prev">
                <span style={{color: "red"}} className="carousel-control-prev-icon" aria-hidden="true"></span>
                <span className="visually-hidden">Previous</span>
            </a>

            {/* Nút điều hướng bên phải */}
            <a style={{ zIndex: "100" }} className="carousel-control-next" href="#header-carousel" role="button" data-bs-slide="next">
                <span className="carousel-control-next-icon" aria-hidden="true"></span>
                <span className="visually-hidden">Next</span>
            </a>

            <div className="carousel-inner">
                {VariantList.map(variant => (
                    <SliderImage variantSelected={variantSelected} variant={variant} key={variant.variantId} />
                ))}
            </div>
        </div>
    )
}

export default SliderImageList
