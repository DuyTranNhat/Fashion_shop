import React, { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { toast } from 'react-toastify'
import { ProductGet, ProductPost } from '~/Models/Product'
import { ProductGetByID, ProductUpdateAPI } from '~/Services/ProductService'
import FormProduct, { ProductFormInput } from './FormProduct'

const EditProduct = () => {
  const { id } = useParams<{ id: string }>()
  const [product, setProduct] = useState<ProductPost | null>();
  const navigate = useNavigate()

  useEffect(() => {
    if (id) {
      ProductGetByID(id)
        .then(res => {
          if (res?.data) {
            const data: ProductGet = res.data;

            setProduct({
              name: data.name,
              description: data.description,
              categoryId: data.categoryDto.categoryId,
              supplierId: data.supplierDto.supplierId,
              attributes: data.attributes
            });
          }
        })
        .catch(error => {
          toast.error("Failed to fetch product details.");
        });
    }
  }, [id]);


  const handleProduct = (form: ProductFormInput) => {
    if (id) {
      ProductUpdateAPI(id, form)
        .then(res => {
          if(res?.status == 200) {
            toast.success("Update product model successfully!")
            navigate("/admin/products") 
          }
        }).catch(error => toast.error(error)) 
    }
  }

  return (
    <div className=" rounded h-100 p-4 custom-container m-4">
      <h6 className="mb-4">Edit a new model product</h6>
      {product
        ? <FormProduct handleProduct={handleProduct} product={product!} />
        : <p>Loading supplier data...</p>}
    </div>
  )
}

export default EditProduct
