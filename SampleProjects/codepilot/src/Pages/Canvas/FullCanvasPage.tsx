import { Outlet } from "react-router-dom";
import React,{ useEffect, useState } from "react";
import "../../App.css";
import { ITable } from "../../Class/Interfaces/ITable";
import Header from "../../Layouts/Header/Header";
import Navbar from "../../Layouts/Navbar/Navbar";
import PropertiesBar from "../../Layouts/PropertiesBar/PropertiesBar";
import MenuCanvas from "./MenuCanvas";

interface FullCanvasPageProps{
    items: ITable[];
    setItems: (items: ITable[]) => void;
}

const FullCanvasPage: React.FC<FullCanvasPageProps> = () => {
  const [selectedPage, setSelectedPage] = useState<string>("Table");
  const [items, setItems] = useState<ITable[]>([]);
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  return (
    <div className="Global--Container">
      <Header selectedPage={selectedPage} setSelectedPage={setSelectedPage}/>
      <div className="BodyStyle">
        <Navbar selectedPage={selectedPage}/>
        <div className="ContentStyle">
          <MenuCanvas 
          items={items} 
          setItems={setItems} 
          selectedIndex={selectedIndex} 
          setSelectedIndex={setSelectedIndex}
          />
        </div>
        <PropertiesBar selectedPage={undefined} />
      </div>
    </div>
  );
};

export default FullCanvasPage;