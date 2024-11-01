import './App.css'

import { Routes, Route } from "react-router-dom";

import { Login } from './Pages/Auth/Login';
import { CanvasPage } from './Pages/Canvas/CanvasPage';
import DraggableCanvas from './Pages/Canvas/DraggableCanvas.tsx';
import MenuCanvas from './Pages/Canvas/MenuCanvas';
import Admin from './Layouts/Admin.jsx';
import CollapsibleList from './Pages/Collapsible List/CollaspsibleList.jsx';

function App() {

  return (
    <>
      <Routes>
        <Route path="/login"
          element={<Login/>}
        ></Route>
        <Route path='/' element={<MenuCanvas/>}></Route>
        <Route path='/draggable' element={<DraggableCanvas/>}></Route>
        <Route path='/admin' element={<Admin/>}>
          <Route path='/admin/canvas' element={<MenuCanvas/>} />
          <Route path='/admin/list' element={<CollapsibleList/>}/>
        </Route>
      </Routes>
    </>
  )
}

export default App
