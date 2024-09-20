export interface CategoryGet {
    categoryId: number;
    name?: string;
    status?: boolean;
    subCategories: CategoryGet[];
}