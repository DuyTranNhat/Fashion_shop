import { CategoryGet } from "./Category";
import { SupplierGet } from "./Supplier";

export type ProductGet = {
    productId: number;
    name: string;
    description: string;
    categoryDto: CategoryGet;
    supplierDto: SupplierGet;
}

export type ProductPost = {
    categoryId: number;
    supplierId: number;
    name: string;
    description: string;
}

export type ProductPut = {
    categoryId: number;
    supplierId: number;
    name: string;
    description: string;
}