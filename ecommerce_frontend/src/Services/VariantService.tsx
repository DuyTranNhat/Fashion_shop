import axios from "axios";
import { handleError } from "~/Helpers/ErrorHandler";
import { ImageGet, VariantGet } from "~/Models/Variant";
import { VariantPostInput } from "~/pages/admin/variant/FormVariant";

const api = "https://localhost:7000/api/Variant";

export const variantGetAPI = async () => {
    try {
        const data = await axios.get<VariantGet[]>(api + "/get-all-variants");
        return data;
    } catch (error) {
        handleError(error)
    }
}

export const VariantPostAPI = async (formInput: VariantPostInput, images: FileList | null) => {
    try {
        const response = await axios.post<number>(api + "/create-variant", formInput)
        return response
    } catch (error) {
        handleError(error)
    }
}

export const ImageGetAPI = async (idVariant: string) => {
    try {
        const response = await axios.get<ImageGet[]>(api + "/get-images-by-variant/" + idVariant)
        return response
    } catch (error) {
        handleError(error)
    }
}



export const VariantUpdateAPI = async (id: string, formInput: VariantPostInput) => {
    try {
        const response = await axios.put<number>(api + `/update-variant/${id}`, formInput)
        return response
    } catch (error) {
        handleError(error)
    }
}


export const VariantGetByIdAPI = async (id: string) => {
    try {
        const response = await axios.get<VariantGet>(api + "/getByID/" + id)
        return response
    } catch (error) {
        handleError(error)
    }
}

export const ImageDeleteAPI = async (idImage: string, idVariant: string) => {
    try {
        const response = await axios.delete<ImageGet[]>(api + `/delete-image/${idImage}/variant/${idVariant}`)
        return response
    } catch (error) {
        handleError(error)
    }
}


export const upLoadImagesAPI = async (images: FileList | null, variantId: number) => {
    try {
        if (images && images.length > 0) {
            const formData = new FormData();

            // Append images to FormData
            for (let i = 0; i < images.length; i++) {
                formData.append('fileImages', images[i]);
            }

            // Include Variant ID in the FormData
            formData.append('VariantId', variantId.toString());

            // Make the API call to upload images
            const imageUploadResponse = await axios.post(api + "/upload-images-variant", formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });
            return imageUploadResponse;
        }
    } catch (error) {
        handleError(error)
    }
}
