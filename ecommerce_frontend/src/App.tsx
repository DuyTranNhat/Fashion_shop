import { RouterProvider } from "react-router-dom"
import router from './routes/router';
import 'owl.carousel/dist/assets/owl.carousel.css';
import 'owl.carousel/dist/assets/owl.theme.default.css';
import './App.css'
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import 'owl.carousel/dist/assets/owl.carousel.css';
import 'owl.carousel';
import { WOW } from 'wowjs'
import { useEffect } from "react";
function App() {
  return (
    <>
     <RouterProvider router={router} />
     <ToastContainer />
    </>
  )
}

export default App
