import axios from "axios"
import { handleError } from "~/Helpers/ErrorHandler"
import { AttributeGet, AttributePost, AttributeUpdate } from "~/Models/Attribute";
import { AttributeFormInput } from "~/pages/admin/Attribute/FormAttrbute";

const api = "https://localhost:7000/api/Attribute"

export const attributeGetAPI = async () => {
    try {
        const data = await axios.get<AttributeGet[]>(api);
        return data;
    } catch (error) {
        handleError(error)
    }
}

export const attributePostAPI = async (form: AttributeFormInput) => {
    try {
        const data = await axios.post<AttributePost[]>(api, form);
        return data;
    } catch (error) {
        handleError(error)
    }
}

export const attributeUpdateAPI = async (id: string, form: AttributeFormInput) => {
    try {
        const data = await axios.put<AttributeUpdate[]>(api + "/update/" + id, form);
        return data;
    } catch (error) {
        handleError(error)
    }
}


export const attributeActiveAPI = async (id: number) => {
    try {
        const data = await axios.put<AttributeGet>(api + "/updateStatus/" + id);
        return data;
    } catch (error) {   
        handleError(error)
    }
}

export const attributeGetByIdAPI = async (id: string) => {
    try {
        const data = await axios.get<AttributeGet>(api + "/getByID/" + id);
        return data;
    } catch (error) {
        handleError(error)
    }
}
