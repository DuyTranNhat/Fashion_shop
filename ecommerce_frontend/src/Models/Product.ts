import { CreateProuctAttributeDto } from "~/pages/admin/Product/FormProduct";
import { AttributeGet } from "./Attribute";
import { CategoryGet } from "./Category";
import { SupplierGet } from "./Supplier";
import { VariantGet } from "./Variant";

export type ProductGet = {
    productId: number;
    name: string;
    description: string;
    categoryDto: CategoryGet;
    supplierDto: SupplierGet;
    attributes: AttributeGet[];
    variants: VariantGet[]
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