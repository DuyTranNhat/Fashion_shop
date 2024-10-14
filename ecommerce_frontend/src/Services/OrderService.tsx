import axios from "axios"
import { handleError } from "~/Helpers/ErrorHandler"
import { CheckoutPost } from "~/Models/Order"

const api = "https://localhost:7000/api/order/checkout"

export const checkoutAPI = async (customerId: number, checkoutForm: FormData) => {
    try {
        const data = await axios.post(`${api}/customerID/${customerId}`, checkoutForm)
        return data
    } catch (error) {
        handleError(error)
    }
}