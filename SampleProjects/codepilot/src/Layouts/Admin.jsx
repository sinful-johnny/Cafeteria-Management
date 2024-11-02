import { Outlet } from "react-router-dom";
import { useState } from "react";
import Header from "./Header/Header";
import Navbar from "./Navbar/Navbar";
import "../App.css";

const Admin = () => {
  const [selectedPage, setSelectedPage] = useState("Table");

  return (
    <div className="Global--Container">
      <Header selectedPage={selectedPage} setSelectedPage={setSelectedPage}/>
      <div className="BodyStyle">
        <Navbar selectedPage={selectedPage}/>
        <div className="ContentStyle">
          <Outlet />
        </div>
      </div>
    </div>
  );
};

export default Admin;