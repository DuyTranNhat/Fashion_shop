import { CreateProuctAttributeDto } from "~/pages/admin/Product/FormProduct";
import { AttributeGet } from "./Attribute";
import { CategoryGet } from "./Category";
import { SupplierGet } from "./Supplier";

export type ProductGet = {
    productId: number;
    name: string;
    description: string;
    categoryDto: CategoryGet;
    supplierDto: SupplierGet;
    attributes: AttributeGet[];
}

export type ProductPost = {
    categoryId: number;
    supplierId: number;
    name: string;
    description: string;
    attributes: CreateProuctAttributeDto[];

}

export type ProductPut = {
    categoryId: number;
    supplierId: number;
    name: string;
    description: string;
}