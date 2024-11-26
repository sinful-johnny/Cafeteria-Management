import CollapsibleListPenis from "../../Components/CollaspsiblePenis/CollaspsibleListPenis";
import "./PropertiesBar.css";
import React, { useEffect, useState } from 'react';

const PropertiesBar = ({clearTable , selectedIndex, selectedTable, ordersOnTable}) => {
    const [openIndexes, setOpenIndexes] = useState({});
    
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