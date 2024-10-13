import { VariantGet } from "./Variant"

export type CartPost = {
    customerId: number,
    variantId: number,
    quantity: number,
}

export type CartGet = {
    cartId: number,
    quantity: number,
    totalPrice: number,
    variant: VariantGet,
}