import { RouterProvider } from "react-router-dom"
import router from './routes/router';
import OwlCarousel from 'react-owl-carousel';
import 'owl.carousel/dist/assets/owl.carousel.css';
import 'owl.carousel/dist/assets/owl.theme.default.css';
import './App.css'

function App() {
  return (
     <RouterProvider router={router} />
  )
}

export default App
