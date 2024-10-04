export type ImageGet = {
    imageId: number;
    variantId: number;
    imageUrl: string;
  }
  
export type ValueGet = {
    valueId: number;
    value1: string;
  }
  
export type VariantGet = {
    variantId: number;
    productId: number;
    variantName: string;
    importPrice: number;
    salePrice: number;
    quantity: number;
    status: string;
    images: ImageGet[];
    values: ValueGet[];
}

