import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { UserProfile, UserProfileToken } from "~/Models/User";
import { RegisterFormsInputs } from "~/pages/customer/Register/Register";
import { LoginFormsInputs } from "~/pages/customer/Login/Login";
const api = "https://localhost:7000/api/";

export const loginAPI = async (form: LoginFormsInputs) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/login", form);
    return data;
  } catch (error) {
    handleError(error);
  }
};
export const registerAPI = async (form: RegisterFormsInputs) => {
  
  try {
    const data = await axios.post<UserProfileToken>(api + "account/register", form);
    return data;
  } catch (error) {
    handleError(error);
  }
};