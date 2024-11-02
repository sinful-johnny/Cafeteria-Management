import './App.css'

import { Routes, Route } from "react-router-dom";

import { Login } from './Pages/Auth/Login';
import Admin from './Layouts/Admin.tsx';
import FullCanvasPage from './Pages/Canvas/FullCanvasPage.tsx';

function App() {

  return (
    <>
      <Routes>
        <Route path="/login"
          element={<Login/>}
        ></Route>
        <Route path='/' element={<FullCanvasPage/>}></Route>
        <Route path='/admin' element={<Admin/>}>
        </Route>
      </Routes>
    </>
  )
}

export default App
