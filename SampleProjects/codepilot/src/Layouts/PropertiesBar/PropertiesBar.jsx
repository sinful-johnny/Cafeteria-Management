import CollapsibleListPenis from "../../Components/CollaspsiblePenis/CollaspsibleListPenis";
import "./PropertiesBar.css";
import React, { useEffect, useState } from 'react';

const PropertiesBar = ({clearTable , selectedIndex, selectedTable, ordersOnTable}) => {
    const [openIndexes, setOpenIndexes] = useState({});
    //const [selectedTable, setSelectedTable] = useState(items[selectedIndex]);
    //const [ordersOnTable, setOrdersOnTable] = useState(orders.map((order) => {
    //  return {...order, content: []}
    //}));

    //useEffect(() => {
    //  setSelectedTable(items[selectedIndex]);
    //  var newOrdersOnTable = orders.map((order) => {
    //    var newOrder = {...order, content: []};
    //    selectedTable.foods.map((food) => {
    //      if(order.content.findIndex(x => x.name === food.foodName) !== -1){
    //        newOrder.content.push({
    //          id: food.foodId,
    //          name: food.foodName,
    //          img: food.imageURL
    //        })
    //      }
    //    });
//
    //    return newOrder;
    //  });
//
    //  setOrdersOnTable(newOrdersOnTable);
//
    //  //console.log(ordersOnTable);
    //  //console.log(newOrdersOnTable);
    //}, [items]);

    //console.log(ordersOnTable)
    
    return (
      <div className="admin-PropertiesBar">
        <div className="Title">
          <h4>Table {selectedTable.tableId}</h4>
        </div>
        <div className="CollaspsibleList--container">
          {(ordersOnTable) && 
            <CollapsibleListPenis items={ordersOnTable} openIndexes={openIndexes} setOpenIndexes={setOpenIndexes}/>
          }
        </div>
        <div className="ClearButton-Container">
          <button onClick={() => clearTable(selectedIndex)} className="ClearButton">Clear the table</button>
        </div>
        
      </div>
    );
  };
  
  export default PropertiesBar;