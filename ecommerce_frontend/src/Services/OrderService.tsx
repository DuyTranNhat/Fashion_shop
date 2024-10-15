import axios from "axios"
import { handleError } from "~/Helpers/ErrorHandler"
import { CheckoutPost, OrderGet } from "~/Models/Order"

const api = "https://localhost:7000/api/order"

export const checkoutAPI = async (customerId: number, checkoutForm: FormData) => {
    try {
        const data = await axios.post(`${api}/checkout/customerID/${customerId}`, checkoutForm)
        return data
    } catch (error) {
        handleError(error)
    }
}

export const CheckoutCompletedGetAPI = async (orderID: number) => {
    try {
        const data = await axios.get<OrderGet>(`${api}/getByID/${orderID}`)
        return data
    } catch (error) {
        handleError(error)
    }
}