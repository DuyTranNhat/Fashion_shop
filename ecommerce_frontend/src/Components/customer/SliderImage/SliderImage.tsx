import React, { act } from 'react'
import { VariantGet } from '~/Models/Variant'

export type Props = {
    variant: VariantGet
    variantSelected: VariantGet
}

const baseUrl = 'https://localhost:7000/';

const SliderImage = ({ variant, variantSelected }: Props) => {
    return (
        <div className={`carousel-item position-relative ${variantSelected.variantId == variant.variantId ? 'active' : ''}
        `} style={{ height: "430px" }}>
            <img className="position-absolute w-100 h-100" src={`${baseUrl}${variant.images[0].imageUrl}`} style={{ objectFit: "cover" }} />
            <div className="carousel-caption d-flex flex-column align-items-center justify-content-center"style={{background: "rgba(61, 70, 77, 0.0)"}} >
                {/* <div className="p-3" style={{ maxWidth: "700px" }}>
                    <h1 className="display-4 text-white mb-3 animate__animated animate__fadeInDown">Men Fashion</h1>
                    <p className="mx-md-5 px-5 animate__animated animate__bounceIn">Lorem rebum magna amet lorem magna erat diam stet. Sadips duo stet amet amet ndiam elitr ipsum diam</p>
                    <a className="btn btn-outline-light py-2 px-4 mt-3 animate__animated animate__fadeInUp" href="#">Shop Now</a>
                </div> */}
            </div>
        </div>
    )
}

export default SliderImage
