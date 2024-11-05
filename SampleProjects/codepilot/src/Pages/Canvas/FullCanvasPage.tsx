import { Outlet } from "react-router-dom";
import React,{ useEffect, useState } from "react";
import "../../App.css";
import { ITable } from "../../Class/Interfaces/ITable";
import Header from "../../Layouts/Header/Header";
import Navbar from "../../Layouts/Navbar/Navbar";
import PropertiesBar from "../../Layouts/PropertiesBar/PropertiesBar";
import MenuCanvas from "./MenuCanvas";
import { checkCollision, isCollidingWithBorderX, isCollidingWithBorderY } from "../../Components/CollisionDetection";

interface FullCanvasPageProps{
    items: ITable[];
    setItems: (items: ITable[]) => void;
}

const FullCanvasPage: React.FC<FullCanvasPageProps> = () => {
  const [selectedPage, setSelectedPage] = useState<string>("Table");
  const [items, setItems] = useState<ITable[]>([]);
  const [isSaved, setSaved] = useState(true);
  //canvasSize: width, height
  const [canvasSize, setCanvasSize] = useState([600, 400]);
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);
  const [isTableMenu, setIsTableMenu] = useState(true);
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

    const handleKeyDown = (event: KeyboardEvent) => {
      //delete table
      if (event.key === 'Delete') {
          deleteSelectedTable();
      }
    };
    window.addEventListener('keydown', handleKeyDown);
    return () => {
      window.removeEventListener('keydown', handleKeyDown);
    };
  },[selectedIndex]);

  useEffect(() => {
    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.key === 's' && event.ctrlKey) {
        event.preventDefault(); // Prevent default "Save Page" behavior
        // Only save if there are unsaved changes
        if (!isSaved) { 
          save();
        }
      }
    };
    window.addEventListener('keydown', handleKeyDown);
    return () => {
      window.removeEventListener('keydown', handleKeyDown);
    };
  }, [isSaved, items]);

  const deleteSelectedTable = () => {
    if (selectedIndex !== null) {
        const newItems = items.filter((_, index) => index !== selectedIndex);
        setItems(newItems);
        setSelectedIndex(null); // Clear selection after deletion
    }
};

  function clearTable(index){
    setItems(items.map((item,i) => {
      if(i === index){
        //do some sorts of database operation then proceed
        if (item.tableStatus === "unlocked") return item; 
        item.foods.length = 0;
        item.tableStatus = "locked";
        return item
      }else{return item}
    }))
    setSaved(false);
  }

  const save = () => {
    let hasCollision = false;
    for (let i = 0; i < items.length; i++) {
      for (let j = i + 1; j < items.length; j++) {
        if (checkCollision(items[i], items[j])) {
          hasCollision = true;
          console.log(`Collision detected between item ${i} and item ${j}`);
          break;
        }
      }
      if (isCollidingWithBorderX(items[i], canvasSize[0])){
        hasCollision = true;
      } else if (isCollidingWithBorderY(items[i], canvasSize[1])){
        hasCollision = true;
      }

      if (hasCollision) break;
    }

    if (hasCollision) {
      console.log("Cannot save due to collision.");
      return;
    }
    const lockedItems = items.map(item => {
      if(item.tableStatus === "occupied"){
        null;
      }
      else{
      item.tableStatus = "locked";
      }
      return item;
    });
    console.log("Current Canvas Items:", lockedItems);
    setSaved(true);
    setItems(lockedItems);
  };

  return (
    <div className="Global--Container">
      <Header selectedPage={selectedPage} setSelectedPage={setSelectedPage} setIsTableMenu={setIsTableMenu} save={save} isSaved={isSaved}/>
      <div className="BodyStyle">
        <Navbar selectedPage={selectedPage} orders={orders} tables={tables}/>
        <div className="ContentStyle">
          <MenuCanvas 
          items={items} 
          setItems={setItems} 
          selectedIndex={selectedIndex} 
          setSelectedIndex={setSelectedIndex}
          isTableMenu={isTableMenu}
          isSaved={isSaved}
          setSaved={setSaved}
          canvasSize={canvasSize}
          />
        </div>
        {(selectedIndex !== null && selectedIndex >= 0 && selectedIndex < items.length) ?
          <PropertiesBar 
          items={items} 
          clearTable={clearTable} 
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