import CollapsibleList from "../../Components/CollaspsibleList/CollaspsibleList";
import "./PropertiesBar.css";
import React, { useEffect, useState } from 'react';

const PropertiesBar = ({selectedPage}) => {
    const [openIndexes, setOpenIndexes] = useState({});
    const [items,setItems] = useState([]);
  
    //this state is completely bullshit
    const [rerender, setRerender] = useState(true);

    const [orders, setOrders] = useState([]);
  
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
        setItems(ordersData);
    },[selectedPage, rerender]);
    
    return (
      <div className="admin-PropertiesBar">
        <div className="Title">
          <h4>Properties</h4>
        </div>
        <div className="CollaspsibleList--container">
           <CollapsibleList items={items} openIndexes={openIndexes} setOpenIndexes={setOpenIndexes}/>
        </div>
      </div>
    );
  };
  
  export default PropertiesBar;