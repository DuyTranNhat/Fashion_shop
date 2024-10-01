import axios from "axios";
import { handleError } from "~/Helpers/ErrorHandler";
import { AttributeGet } from "~/Models/Attribute";
import { BannerGet } from "~/Models/Banner";

const api = "https://localhost:7000/api/slide"

export const bannerGetAPI = async () => {
    try {
        const data = await axios.get<BannerGet[]>(api);
        return data;
    } catch (error) {
        handleError(error)
    }
}

export const bannerDeleteAPI = async (id : number) => {
    try {
        const data = await axios.delete(api + "/delete/" + id);
        return data;
    } catch (error) {
        handleError(error)
    }
}

export const bannerPostAPI = async (formData: FormData) => {
    try {
       const data = await axios.post(api, formData)
      return data;
    } catch (error) {
        handleError(error)
    }
}


export const bannerUpdateAPI = async (id: number, formData: FormData) => {
    try {
        console.log(formData);
       const data = await axios.put(api + '/update/' + id, formData)
      return data;
    } catch (error) {
        handleError(error)
    }
}

export const bannerGetByIdAPI = async (id: string) => {
    try {
        const data = await axios.get<BannerGet>(api + "/getByID/" + id);
        return data;
    } catch (error) {
        handleError(error)
    }
}