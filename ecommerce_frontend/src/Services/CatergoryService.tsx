import axios from 'axios'
import { CategoryGet } from '../Models/Category';
import { handleError } from '../Helpers/ErrorHandler';

const api = "https://localhost:7251/api/Category"

export const categoryGetAPI = async () => {
    try {
        const data = await axios.get<CategoryGet[]>(api);
        return data;
    } catch (error) {
        handleError(error);
    }
}

