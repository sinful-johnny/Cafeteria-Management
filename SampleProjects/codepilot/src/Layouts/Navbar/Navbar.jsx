import { NavLink } from "react-router-dom";
import React, { useEffect, useState } from 'react';
import { FaChevronDown, FaChevronUp } from 'react-icons/fa';

import "./Navbar.css";
import SearchBar from "../../Components/Search/SearchInput";

const Navbar = ({selectedPage}) => {
  const [openIndexes, setOpenIndexes] = useState({});
  const [items,setItems] = useState([]);

  //this state is completely bullshit
  const [rerender, setRerender] = useState(true);

  const [tables, setTables] = useState([]);
  const [orders, setOrders] = useState([]);

  const toggleItem = (index) => {
    setOpenIndexes((prev) => ({
      ...prev,
      [index]: !prev[index]
    }));
  };

  const tablesData = [
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
  ];

  const ordersData = [
    {id: 1, title: 'Tea', content: [
      {ID_FOOD: 1, objectType: "Food", name: "Honey Jasmine Green", img: "/tea1.svg"},
      {ID_FOOD: 2, objectType: "Food", name: "Raspberry Snow", img: "/tea2.svg"}
    ]},
    {id: 2, title: 'Coffee', content: [
      {ID_FOOD: 3, objectType: "Food", name: "Pure Coffee", img: "/coffee1.svg"},
      {ID_FOOD: 4, objectType: "Food", name: "Iced Coffee with Milk", img: "/coffee2.svg"}
    ]}
  ]

  useEffect(() => {
    setTables(tablesData);
    setOrders(ordersData);

    if(tables.length === 0 || orders.length === 0){
      setRerender(!rerender);
    }

    if(selectedPage === "Table"){
      setItems(tables);
    }else if(selectedPage === "Order"){
      setItems(orders);
    }else{
      console.log("Input Not recognized!");
    }
  },[selectedPage, rerender]);

  const spawnListComponent = (content) => {
    return content.map(shape => {
      const handleDragStart = (event, data) => {
        event.dataTransfer.setData('text/plain', JSON.stringify(data));
      };

      return (
        <div className="collaspsible-listItem"
          draggable
          onDragStart={(event) => handleDragStart(event, shape)}
          key={shape.name}
        >
          <img src={shape.img}/>
          <span>{shape.name}</span>
        </div>
      );
    });
  }

  const onSearch = (query) => {
    console.log("Search:", query)
  }

  const CollapsibleList = ({items}) => {
    return (
      items.map((item, index) => (
        <div key={item.id}>
          <div
            onClick={() => toggleItem(index)}
            className="collapsible-header"
          >
            <span>
              {openIndexes[index] ? <FaChevronUp /> : <FaChevronDown />}
            </span>
            <span>{item.title}</span>
          </div>
          {openIndexes[index] && (
            <div>
              {spawnListComponent(item.content)}
            </div>
          )}
        </div>
      ))
    )
  }
  
  return (
    <div className="admin-navbar">
      <div className="Search--Container">
        <SearchBar onSearch={onSearch} />
      </div>

      <div className="CollaspsibleList--container">
         <CollapsibleList items={items}/>
      </div>
    </div>
  );
};

export default Navbar;
