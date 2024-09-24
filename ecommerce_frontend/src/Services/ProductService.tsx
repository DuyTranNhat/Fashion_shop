import axios from "axios"
import { toast } from "react-toastify"
import { handleError } from "~/Helpers/ErrorHandler"
import { ProductGet, ProductPost, ProductPut } from "~/Models/Product"
import { ProductFormInput } from "~/pages/admin/Product/FormProduct"

const api = "https://localhost:7000/api/Product"

export const ProductPostAPI = async (form: ProductFormInput) => {
    try {
        const data = axios.post<ProductPost>(api, {...form})
        return data
    } catch (error) {
        handleError(error)
    }
}

export const ProductGetAPI = async () => {
    try {
        const data = axios.get<ProductGet[]>(api)
        return data
    } catch (error) {
        handleError(error)
    }
}

export const ProductGetByID = async (id: string) => {
    try {
        const data = await axios.get<ProductGet>(api + "/getByID/" + id)
        return data
    } catch (error) {
        handleError(error)
    }
}

export const ProductUpdateAPI = async (id: string, form: ProductFormInput) => {
    try {
        const data = await axios.put<ProductPut>(api + "/update/" + id, {...form})
        return data
    } catch(error) {
        handleError(error)
    }
}
