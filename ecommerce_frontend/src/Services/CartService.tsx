import axios from "axios"
import { toast } from "react-toastify";
import { handleError } from "~/Helpers/ErrorHandler"
import { CartGet, CartPost } from "~/Models/Cart";




const api = "https://localhost:7000/api/Cart"


export const addCartAPI = async (cartPost: CartPost) => {
    try {
        const data = await axios.post(api + "/addtoCart", cartPost);
        return data
    } catch (error) {
        handleError(error)
    }
}

export const increaseQuantityAPI = async (idCart: number) => {
    try {
        const data = await axios.post(api + "/increaseQuantity/" + idCart);
        return data
    } catch (error) {

        handleError(error)
    }
}

export const decreaseQuantityAPI = async (idCart: number) => {
    try {
        const data = await axios.post(api + "/decreaseQuantity/" + idCart);
        return data
    } catch (error) {
       handleError(error)
    }
}

export const removeCartAPI = async (idCart: number) => {
    try {
        const data = await axios.delete(api + "/removecartitem/" + idCart);
        return data
    } catch (error) {
       handleError(error)
    }
}

export const CartGetAPI = async () => {
    try {
        const data = await axios.get<CartGet[]>(api);
        return data
    } catch (error) {
        handleError(error)
    }
}

