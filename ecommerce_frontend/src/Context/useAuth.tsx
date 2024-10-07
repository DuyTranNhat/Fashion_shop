import React, { Children, useContext, useEffect, useState } from "react";
import { UserProfile, UserProfileToken } from "../Models/User"
import { createContext } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { loginAPI, registerAPI } from "../Services/AuthService";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { RegisterFormsInputs } from "~/pages/customer/Register/Register";
import { LoginFormsInputs } from "~/pages/customer/Login/Login";

type UserContextType = {
    user: UserProfile | null;
    token: string | null;
    registerUser: (form: RegisterFormsInputs) => void;
    login: (form: LoginFormsInputs) => void;
    logout: () => void;
    isLoggedIn: () => boolean;
    isReady: boolean
};

type Props = { children: React.ReactNode }

const UserContext = createContext<UserContextType>({} as UserContextType);

export const UserProvider = ({ children }: Props) => {
    const navigate = useNavigate();

    const [token, setToken] = useState<string | null>(null);
    const [user, setUser] = useState<UserProfile | null>(null);
    const [isReady, setIsReady] = useState<boolean>(false);

    useEffect(() => {
        const user = localStorage.getItem("user");
        const token = localStorage.getItem("token");

        if (user && token) {
            setUser(JSON.parse(user));
            setToken(token);
            axios.defaults.headers.common["Authorization"] = "Bearer " + token;
        }
        setIsReady(true);   
    }, [token]);

    const registerUser = async (form: RegisterFormsInputs) => {
        await registerAPI(form)
            .then((res) => {
                if (res) {
                    localStorage.setItem("token", res?.data.token);
                    const UserObj = {
                        name: res?.data.name,
                        email: res?.data.email,
                        role: res?.data.role,
                    }

                    localStorage.setItem("user", JSON.stringify(UserObj));
                    setToken(res?.data.token);
                    setUser(UserObj!);
                    toast.success("Register success!");
                    navigate("/")

                }
            }).catch(e => toast.warning("server error occured"));
    }

    const login = async (form: LoginFormsInputs) => {
        await loginAPI(form)
            .then(res => {
                if (res) {
                    localStorage.setItem("token", res?.data.token);
                    const UserObject = {
                        name: res?.data.name,
                        email: res?.data.email,
                        role: res?.data.role,
                    }


                    localStorage.setItem("user", JSON.stringify(UserObject));
                    setUser(UserObject);
                    setToken(res?.data.token);

                    if (res?.data.role === "admin") {
                        navigate("/admin/variants")
                    }
                    if (res?.data.role === "user") {
                        navigate("/")
                    }
                    
                    toast.success("Login Successfully!");
                   
                }
            }).catch(ex => toast.warning("Server error occured"))
    }

    const isLoggedIn = () => {
        return !!user;
    };

    const logout = () => {
        localStorage.removeItem("user")
        localStorage.removeItem("token")
        setUser(null)
        setToken(null)
        setIsReady(false)
        navigate("/access/login")
        toast.success("Log out")
    }

    return (
        <UserContext.Provider value={{ user, token, registerUser, login, logout, isLoggedIn, isReady }} >
            {isReady ? children : null}
        </UserContext.Provider>
    )
}

export const useAuth = () => {
    return useContext(UserContext)
}