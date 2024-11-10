import './App.css'

import { Routes, Route } from "react-router-dom";

import { Login } from './Pages/Auth/Login';
import Admin from './Layouts/Admin.tsx';
import FullCanvasPage from './Pages/Canvas/FullCanvasPage.tsx';

function App() {

  return (
    <>
      <Routes>
        <Route path="/"
          element={<Login/>}
        ></Route>
        <Route path='/canvas' element={<FullCanvasPage/>}></Route>
      </Routes>
    </>
  )
}

export default App
