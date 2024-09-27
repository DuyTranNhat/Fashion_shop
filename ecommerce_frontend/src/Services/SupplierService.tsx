import axios from "axios";
import { handleError } from "~/Helpers/ErrorHandler";
import { SupplierGet, SupplierPost, SupplierPut } from "~/Models/Supplier";
import { SupplierFormInput } from "~/pages/admin/Supplier/FormSupplier";

const api = "https://localhost:7000/api/Supplier";

export const supplierGetAPI = async () => {
    try {
        const data = await axios.get<SupplierGet[]>(api);
        return data;
    } catch (error) {
        handleError(error);
    }
}

export const supplietUpfateStatusAPI = async (id: number) => {
    try {
        const data = await axios.put(api + "/updateStatus/" + id)
        return data;
    }  catch (error) {
        handleError(error);
    }
}

export const supplierPostAPI = async (formUpdate: SupplierFormInput) => {
        try {
            const data = await axios.post<SupplierPost>(api, { ...formUpdate });
            return data;
        } catch (error) {
            handleError(error)
        }
}

export const supplierPutAPI = async (id: string, formUpdate: SupplierFormInput) => {
    try {
        const data = await axios.put<SupplierPut>(api + "/" + id, { ...formUpdate })
        return data;
    } catch (error) {
        handleError(error)
    }
}

export const supplierGetByIdAPI =  async (id: string) => {
    try {
        const data = await axios.get<SupplierGet>(api + "/getByID/" + id);
        return data;
    } catch (error) {
        handleError(error)
    }
}
