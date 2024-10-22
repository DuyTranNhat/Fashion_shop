import axios from "axios"
import { handleError } from "~/Helpers/ErrorHandler"
import { Customer } from "~/Models/User"

const api = "https://localhost:7000/api/Customer/"

export const customerGetAPI = async (idUser: number) => {
    try {
        const data = await axios.get<Customer>(api + "GetById/" + idUser)
        return data
    } catch (   error) {
        handleError(error)
    }
}

export const updateCustomerAPI = async (idUser: number, form: FormData) => {
    try {
        const data = await axios.put<Customer>(api + "updateProfile/" + idUser, form)
        return data
    } catch (error) {
        handleError(error)
    }
}