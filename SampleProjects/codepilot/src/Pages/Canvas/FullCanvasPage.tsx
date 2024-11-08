import { Outlet } from "react-router-dom";
import React,{ useEffect, useState } from "react";
import "../../App.css";
import { ITable } from "../../Class/Interfaces/ITable";
import Header from "../../Layouts/Header/Header";
import Navbar from "../../Layouts/Navbar/Navbar";
import PropertiesBar from "../../Layouts/PropertiesBar/PropertiesBar";
import MenuCanvas from "./MenuCanvas";
import { checkCollision, isCollidingWithBorderX, isCollidingWithBorderY } from "../../Components/CollisionDetection";
import axios from "axios"
import { RectangleTable } from "../../Class/Tables/RectangleTable";
import { CircleTable } from "../../Class/Tables/CircleTable";
import { TableFactory } from "../../Class/Factories/TableFactory";
import { FoodOnTable } from "../../Class/FoodOnTable";
import { Food } from "../../Class/Food";

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

  const [ordersOnTable, setOrdersOnTable] = useState<any>([])
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    console.log(isLoading);
    if(isLoading === false){
      setIsLoading(prev => !prev);

      console.log("Loading table types.");
      axios.get("http://localhost:5030/api/ShapeType")
      .then(response => {
        var data = response.data;
        var tablesData : {id: number, 
          title: string, 
          content: any[]} = {
          id: 1, 
          title: 'Table', 
          content: []
        }
  
        var content: any[] = [];
        data.map(object => {
          content.push( {
            shapeId: object.iD_SHAPE,
            name: object.shapE_TYPENAME,
            objectType: "Table",
            shapeType: object.shapE_TYPENAME === "Rectangle" ? "rectangle" : "circle",
            img: object.shapE_TYPENAME === "Rectangle" ? "/square.svg" : "/circle.svg",
            width: object.width,
            height: object.height,
            radius: object.radius
          });
        });
  
        tablesData = {...tablesData, content: content}
  
        setTables(prevTables => {
          prevTables.length = 0;
          prevTables.push(tablesData)
          return prevTables;
        });
  
        console.log("Table types loaded!", tables);
  
        console.log("Loading menu.");
        axios.get("http://localhost:5030/api/FoodType")
            .then(response => {
              //console.log(response.data);
              var data = response.data;
              var categories = data.map(object => object.fooD_TYPENAME).filter((object, index, self) => index === self.findIndex((t) => t === object));
              var orderData = categories.map((category, i) => {
                var order = {
                  id: i, 
                  title: category,
                  content: [],
                }
  
                var content: any[] = []
                data.map(object => {
                  if(object.fooD_TYPENAME === category){
                    content.push(
                      {
                        ID_FOOD: object.iD_FOOD, 
                        objectType: "Food", 
                        name: object.fooD_NAME, 
                        img: object.IMAGE_LINK,
                      }
                    );
                  }
                })
                
                return {...order, content: content};
              })
              //console.log(orderData);
              setOrders(orderData);
              console.log("Menu loaded!", orderData);
  
              console.log("Loading canvas.");
              axios.get("http://localhost:5030/api/TableFoods")
              .then(response => {
                var data = response.data;
                var loadedTables: ITable[] = data.map(table => {
                  const shapePrototypeIndex = tables[0].content.findIndex(x => x.shapeId === table.shapeId);
                  const shapeType: string = tables[0].content[shapePrototypeIndex].shapeType;
                  var newTable: ITable = TableFactory.createTable(table.tableId, table.shapeId, shapeType, table.x, table.y,table.width,table.height,table.radius);
                  newTable.tableStatus = table.tableStatus;
  
                  const foodsOnTable = table.foods.map(food => {
                    var foodOnTable: FoodOnTable = new FoodOnTable(new Food(food.food.foodId, food.food.foodName,food.food.amount_left, food.food.price, food.food.foodTypeStatus,food.food.imageURL), food.amount);
                    return foodOnTable;
                  });
                
                  newTable.foods = foodsOnTable;
                
                  return newTable;
                });
              
                console.log("Canvas loaded!",loadedTables);
                setItems(loadedTables);

              }).catch(error => {
                console.log("Error",error);
              });
            })
            .catch(error => {
              console.log("Error",error);
            });
  
  
      })
      .catch(error => {
        console.log("Error",error);
      });
    }
    //console.log("Data loaded!",tables, orders);
  },[])

  useEffect(() => {
    if(selectedIndex !== null && selectedIndex >= 0 && selectedIndex < items.length){
      var table = items[selectedIndex];
      var newOrdersOnTable = orders.map((order) => {
        var newOrder = {...order, content: []};
        var content: any[] = []
        if(table){
          table.foods.map((food) => {
            if(order.content.findIndex(x => x.name === food.food.foodName) !== -1){
              content.push({
                id: food.food.foodId,
                name: food.food.foodName,
                img: food.food.imageURL,
                amount: food.amount
              })
            }
          });
          return {...newOrder, content: content}
        }
      });

      setOrdersOnTable(newOrdersOnTable);
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      //delete table
      if (event.key === 'Delete') {
          deleteSelectedTable();
      }
    };
    window.addEventListener('keydown', handleKeyDown);

    //console.log(selectedIndex);

    return () => {
      window.removeEventListener('keydown', handleKeyDown);
    };
  },[selectedIndex, items]);

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
    setSaved(false);
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

    console.log("Saving data!");
    axios.post("http://localhost:5030/api/TableFoods", lockedItems).then(() => {
      console.log("Data saved!");
      setSaved(true);
    }).catch(error => {
      console.log("Error saving data",error);
    });

    console.log("Current Canvas Items:", lockedItems);

    setItems(lockedItems);
  };

  const drawItems = (canvas,context) => {
    context.clearRect(0, 0, canvas.width, canvas.height);
    items.forEach(item => {
      item.draw(context);
      const text = `Table ${item.tableId}: ${item.tableStatus}`;
      context.font = "12px Arial";
      context.fillStyle = "black";
      const textWidth = context.measureText(text).width;
      let centerX, centerY;
      if (item instanceof RectangleTable) {
          // Center for rectangle
          centerX = item.x + (item.width / 2) - (textWidth / 2);
          centerY = item.y + (item.height / 2) + 6;
      } else if (item instanceof CircleTable) {
          // Center for circle
          centerX = item.x - (textWidth / 2); // Circle's center x, minus half the text width
          centerY = item.y + 4; // Circle's center y, with a slight vertical offset
      }
      context.fillText(text, centerX, centerY);
    });
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
          drawItems={drawItems}
          />
        </div>
        {(selectedIndex !== null && selectedIndex >= 0 && selectedIndex < items.length) ?
          <PropertiesBar 
          clearTable={clearTable} 
          selectedTable={items[selectedIndex]}
          selectedIndex={selectedIndex}
          //setSelectedTable={setSelectedTable}
          ordersOnTable={ordersOnTable}
          /> : <></>
        }
        
      </div>
    </div>
  );
};

export default FullCanvasPage;