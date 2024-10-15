import { VariantGet } from "./Variant";

export type CheckoutPost = {
      address: string,
      phone: string,
      paymentMethod: string,
      shippingService: string,
      notes: string
}

export interface OrderGet {
    orderId: number;
    customerName: string;
    orderDate: string;
    totalAmount: number;
    status: string | null;
    address: string;    
    phone: string | null;
    paymentMethod: string | null;
    shippingService: string | null;
    notes: string | null;
    orderDetails: OrderDetailGet[];
}

export interface OrderDetailGet {
    orderDetailId: number;
    orderId: number;
    variant: VariantGet;
    quantity: number;
    price: number;
}