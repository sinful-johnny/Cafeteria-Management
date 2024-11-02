import { Outlet } from "react-router-dom";
import { useState } from "react";
import Header from "./Header/Header";
import Navbar from "./Navbar/Navbar";
import "../App.css";
import PropertiesBar from "./PropertiesBar/PropertiesBar";
import React from "react";
import { ITable } from "../Class/Interfaces/ITable";


const Admin: React.FC = () => {
  const [selectedPage, setSelectedPage] = useState<string>("Table");
  const [items, setItems] = useState<ITable[]>([]);

  return (
    <div className="Global--Container">
      <Header selectedPage={selectedPage} setSelectedPage={setSelectedPage}/>
      <div className="BodyStyle">
        <Navbar selectedPage={selectedPage}/>
        <div className="ContentStyle">
          <Outlet />
        </div>
        <PropertiesBar selectedPage={undefined} />
      </div>
    </div>
  );
};

export default Admin;
