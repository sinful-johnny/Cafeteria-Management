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
  const [tables, setTables] = useState([
    { id: 1, title: 'Table', content: [
      {shapeId: 1, name: "Square", objectType: "Table", shapeType: "rectangle", color: "blue", img: "/square.svg"},
      {shapeId: 2, name: "Circular", objectType: "Table", shapeType: "circle", color: "red", img: "/circle.svg"}
    ]},
    //{id: 2, title: 'Tea', content: [
    //  {ID_FOOD: 1, objectType: "Food", name: "Honey Jasmine Green", img: "/tea1.svg"},
    //  {ID_FOOD: 2, objectType: "Food", name: "Raspberry Snow", img: "/tea2.svg"}
    //]},
    //{id: 3, title: 'Coffee', content: [
    //  {ID_FOOD: 3, objectType: "Food", name: "Pure Coffee", img: "/coffee1.svg"},
    //  {ID_FOOD: 4, objectType: "Food", name: "Iced Coffee with Milk", img: "/coffee2.svg"}
    //]},
    //{id: 4, title: 'Coffee', content: [
    //  {ID_FOOD: 3, objectType: "Food", name: "Pure Coffee", img: "/coffee1.svg"},
    //  {ID_FOOD: 4, objectType: "Food", name: "Iced Coffee with Milk", img: "/coffee2.svg"}
    //]},
    //{id: 5, title: 'Coffee', content: [
    //  {ID_FOOD: 3, objectType: "Food", name: "Pure Coffee", img: "/coffee1.svg"},
    //  {ID_FOOD: 4, objectType: "Food", name: "Iced Coffee with Milk", img: "/coffee2.svg"}
    //]},
    //{id: 6, title: 'Coffee', content: [
    //  {ID_FOOD: 3, objectType: "Food", name: "Pure Coffee", img: "/coffee1.svg"},
    //  {ID_FOOD: 4, objectType: "Food", name: "Iced Coffee with Milk", img: "/coffee2.svg"}
    //]}
  ]);
  const [orders, setOrders] = useState([
    {id: 1, title: 'Tea', content: [
      {ID_FOOD: 1, objectType: "Food", name: "Honey Jasmine Green", img: "/tea1.svg"},
      {ID_FOOD: 2, objectType: "Food", name: "Raspberry Snow", img: "/tea2.svg"}
    ]},
    {id: 2, title: 'Coffee', content: [
      {ID_FOOD: 3, objectType: "Food", name: "Pure Coffee", img: "/coffee1.svg"},
      {ID_FOOD: 4, objectType: "Food", name: "Iced Coffee with Milk", img: "/coffee2.svg"}
    ]}
  ]);

  const [selectedTable, setSelectedTable] = useState<ITable | null>(null);

  useEffect(() => {
    if(selectedIndex !== null){
      setSelectedTable(items[selectedIndex]);
      console.log(items[selectedIndex]);
    }
      
  },[selectedIndex]);



  return (
    <div className="Global--Container">
      <Header selectedPage={selectedPage} setSelectedPage={setSelectedPage}/>
      <div className="BodyStyle">
        <Navbar selectedPage={selectedPage} orders={orders} tables={tables}/>
        <div className="ContentStyle">
          <MenuCanvas 
          items={items} 
          setItems={setItems} 
          selectedIndex={selectedIndex} 
          setSelectedIndex={setSelectedIndex}
          />
        </div>
        {(selectedIndex !== null && selectedIndex >= 0 && selectedIndex < items.length) ?
          <PropertiesBar 
          items={items} 
          setItems={setItems} 
          selectedIndex={selectedIndex} 
          //selectedTable={selectedTable}
          //setSelectedTable={setSelectedTable}
          orders={orders} 
          /> : <></>
        }
        
      </div>
    </div>
  );
};

export default FullCanvasPage;