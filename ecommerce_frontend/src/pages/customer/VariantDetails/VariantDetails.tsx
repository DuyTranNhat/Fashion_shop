import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import SliderImageList from '~/Components/customer/SliderImage/SliderImageList';
import { useAuth } from '~/Context/useAuth';
import { CartPost } from '~/Models/Cart';
import { ProductGet } from '~/Models/Product';
import { VariantGet } from '~/Models/Variant';
import { addCartAPI } from '~/Services/CartService';
import { ProductGetByID } from '~/Services/ProductService';

type AttributeSelected = {
    idValue: number,
    idAttr: number,
};

const VariantDetails = () => {
    const { idVariant, idProduct } = useParams<{ idVariant: string, idProduct: string }>();
    const [product, setProduct] = useState<ProductGet | null>(null);
    const [selectedValues, setSelectedValues] = useState<AttributeSelected[]>([]);
    const [variantSelected, setVariantSelected] = useState<VariantGet | undefined>();
    const [quantity, setQuantity] = useState<number>(1);
    const [inavailableError, setInavailableError] = useState<boolean>(false);
    const { user, isLoggedIn } = useAuth()
    const navigate = useNavigate()

    useEffect(() => {
        if (idProduct) {
            ProductGetByID(idProduct)
                .then(res => {
                    if (res?.data) {
                        setProduct(res.data);
                        const variant = res.data.variants.find(v => v.variantId === Number(idVariant));
                        setVariantSelected(variant);
                        if (variant?.values) {
                            const newSelectedValues = variant.values.map(value => ({
                                idValue: value.valueId,
                                idAttr: value.attributeID,
                            }));
                            setSelectedValues(newSelectedValues);
                        }
                    }
                });
        }
    }, [idProduct, idVariant]);

    const handleValueChange = (attributeId: number, idValue: number) => {
        setSelectedValues(prevSelectedValues => {
            const value = prevSelectedValues.some(item => item.idAttr === attributeId)
                ? prevSelectedValues.map(item =>
                    item.idAttr === attributeId ? { idAttr: attributeId, idValue } : item
                )
                : [...prevSelectedValues, { idAttr: attributeId, idValue }];
            return value;
        });
    };

    useEffect(() => {
        const handleSelectVariant = () => {
            if (selectedValues && product) {
                const variantSelected = product?.variants.find(variant =>
                    variant.values.every(variantsValue =>
                        selectedValues.some(selectedValue => selectedValue.idValue === variantsValue.valueId)
                    )
                );
                if (variantSelected) {
                    setVariantSelected(variantSelected);
                    setInavailableError(false);
                } else {
                    setInavailableError(true);
                    toast.error('Product out of stock!');
                }
            }
        };
        handleSelectVariant();
    }, [selectedValues, product]);

    const handleQuantityChange = (delta: number) => {
        setQuantity(prevQuantity => {
            const newQuantity = prevQuantity + delta;
            if (newQuantity < 1) return 1;  // Prevent quantity below 1
            if (variantSelected && newQuantity > variantSelected.quantity) {
                toast.error('Exceeds available stock');
                return variantSelected.quantity;  // Prevent exceeding stock
            }
            return newQuantity;
        });
    };

    const handleAddToCart = () => {
        if (variantSelected && quantity <= variantSelected.quantity && !inavailableError) {
            if (isLoggedIn()) {
                const cartPost: CartPost = {
                    customerId: user?.customerId!,
                    variantId: variantSelected.variantId,
                    quantity: quantity
                }
                addCartAPI(cartPost)
                    .then(res => {
                        if (res?.status === 201)
                            toast.success(`Add ${quantity} items!`)
                    }).catch(error => toast.success(error))
            } else { navigate("/access/login") }
        } else {
            toast.error('Invalid quantity!');
        }
    };

    return (
        product && variantSelected && selectedValues ? (
            <div className="container-fluid pb-5">
                <div className="row px-xl-5">
                    <div className="col-lg-5 mb-30">
                        <SliderImageList VariantList={product.variants} variantSelected={variantSelected} />
                    </div>
                    <div className="col-lg-7 h-auto mb-30">
                        <div className="h-100 bg-light p-30">
                            <h3>{product.name}</h3>
                            <div className="d-flex mb-3">
                                <div className="text-primary mr-2">
                                    <small className="fas fa-star"></small>
                                    <small className="fas fa-star"></small>
                                    <small className="fas fa-star"></small>
                                    <small className="fas fa-star-half-alt"></small>
                                    <small className="far fa-star"></small>
                                </div>
                                <small className="pt-1">(99 Reviews)</small>
                            </div>
                            <h3 className="font-weight-semi-bold mb-4">
                                {variantSelected.salePrice?.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}
                            </h3>

                            {product.attributes.map(attribute => (
                                <div className="d-flex mb-3" key={attribute.attributeId}>
                                    <strong className="text-dark mr-3">{attribute.name}:</strong>
                                    {attribute.values.map(value => (
                                        <div key={value.valueId} className="custom-control custom-radio custom-control-inline">
                                            <input type="radio"
                                                className="custom-control-input"
                                                checked={selectedValues.some((item: AttributeSelected) => item.idValue === value.valueId && item.idAttr === attribute.attributeId)}
                                                onChange={() => handleValueChange(attribute.attributeId, value.valueId)}
                                                id={value.valueId.toString()}
                                                name={attribute.attributeId.toString()} />
                                            <label className="custom-control-label" htmlFor={value.valueId.toString()}>{value.value1}</label>
                                        </div>
                                    ))}
                                </div>
                            ))}

                            <div className="d-flex align-items-center mb-4 pt-2">
                                <div className="input-group quantity mr-3" style={{ width: "130px" }}>
                                    <div className="input-group-btn">
                                        <button className="btn btn-primary btn-minus" onClick={() => handleQuantityChange(-1)}>
                                            <i className="fa fa-minus"></i>
                                        </button>
                                    </div>
                                    <input type="text" className="form-control bg-secondary border-0 text-center" value={quantity} readOnly />
                                    <div className="input-group-btn">
                                        <button className="btn btn-primary btn-plus" onClick={() => handleQuantityChange(1)}>
                                            <i className="fa fa-plus"></i>
                                        </button>
                                    </div>
                                </div>
                                <button className="btn btn-primary px-3" onClick={handleAddToCart}>
                                    <i className="fa fa-shopping-cart mr-1"></i> Add To Cart
                                </button>
                            </div>
                            <div className="d-flex mb-3">
                                {inavailableError ?
                                    <strong className="text-danger mr-3">Quantity: 0</strong>
                                    : <strong className="text-dark mr-3">Quantity: {variantSelected.quantity} items available</strong>
                                }
                            </div>
                            <div className="d-flex pt-2">
                                <strong className="text-dark mr-2">Share on:</strong>
                                <div className="d-inline-flex">
                                    <a className="text-dark px-2" href="">
                                        <i className="fab fa-facebook-f"></i>
                                    </a>
                                    <a className="text-dark px-2" href="">
                                        <i className="fab fa-twitter"></i>
                                    </a>
                                    <a className="text-dark px-2" href="">
                                        <i className="fab fa-linkedin-in"></i>
                                    </a>
                                    <a className="text-dark px-2" href="">
                                        <i className="fab fa-pinterest"></i>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        ) : <>Loading</>
    );
};

export default VariantDetails;
