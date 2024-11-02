import React, { useEffect, useState } from 'react';

import "./Navbar.css";
import SearchBar from "../../Components/Search/SearchInput";
import CollapsibleList from "../../Components/CollaspsibleList/CollaspsibleList";

const Navbar = ({selectedPage}) => {
  const [openIndexes, setOpenIndexes] = useState({});
  const [items,setItems] = useState([]);

  //this state is completely bullshit
  const [rerender, setRerender] = useState(true);

  const [tables, setTables] = useState([]);
  const [orders, setOrders] = useState([]);

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

  const onSearch = (query) => {
    console.log("Search:", query)
  }
  
  return (
    <div className="admin-navbar">
      <div className="Search--Container">
        <SearchBar onSearch={onSearch} />
      </div>

      <div className="CollaspsibleList--container">
         <CollapsibleList items={items} openIndexes={openIndexes} setOpenIndexes={setOpenIndexes}/>
      </div>
    </div>
  );
};

export default Navbar;
