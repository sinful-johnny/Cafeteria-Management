import CollapsibleList from "../../Components/CollaspsibleList/CollaspsibleList";
import "./PropertiesBar.css";
import React, { useEffect, useState } from 'react';

const PropertiesBar = ({items, setItems, selectedIndex, orders}) => {
    const [openIndexes, setOpenIndexes] = useState({});
    const [selectedTable, setSelectedTable] = useState(items[selectedIndex]);
    const [ordersOnTable, setOrdersOnTable] = useState(orders.map((order) => {
      return {...order, content: []}
    }));

    useEffect(() => {
      setSelectedTable(items[selectedIndex]);
      var newOrdersOnTable = orders.map((order) => {
        var newOrder = {...order, content: []};
        selectedTable.foods.map((food) => {
          if(order.content.findIndex(x => x.name === food.foodName) !== -1){
            newOrder.content.push({
              id: food.foodId,
              name: food.foodName,
              img: food.imageURL
            })
          }
        });

        return newOrder;
      });

      setOrdersOnTable(newOrdersOnTable);

      //console.log(newOrdersOnTable);
    }, [selectedIndex, items]);
    
    return (
      <div className="admin-PropertiesBar">
        <div className="Title">
          <h4>Orders on Table {selectedTable.tableId}</h4>
        </div>
        <div className="CollaspsibleList--container">
          {ordersOnTable && 
            <CollapsibleList items={ordersOnTable} openIndexes={openIndexes} setOpenIndexes={setOpenIndexes}/>
          }
        </div>
      </div>
    );
  };
  
  export default PropertiesBar;