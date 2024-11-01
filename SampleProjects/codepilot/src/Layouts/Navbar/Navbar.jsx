import { NavLink } from "react-router-dom";
import React, { useEffect, useState } from 'react';
import { FaChevronDown, FaChevronUp } from 'react-icons/fa';

import "./Navbar.css";
import SearchBar from "../../Components/Search/SearchInput";

const Navbar = () => {
  const classNameFunc = ({ isActive }) =>
    isActive ? "admin-nav active-admin-nav" : "admin-nav";

  const [openIndexes, setOpenIndexes] = useState({});
  const [items,setItems] = useState([]);

  const toggleItem = (index) => {
    setOpenIndexes((prev) => ({
      ...prev,
      [index]: !prev[index]
    }));
  };

  const tablesData = [
    { id: 1, title: 'Table', content: [
      {shapeId: 1, name: "Square", shapeType: "rectangle", color: "blue", img: "/square.svg"},
      {shapeId: 2, name: "Circular", shapeType: "circle", color: "red", img: "/circle.svg"}
    ]}
  ];

  useEffect(() => {
    setItems(tablesData);
  },[])

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
  
  return (
    <div className="admin-navbar">
      <div className="Search--Container">
        <SearchBar onSearch={onSearch} />
      </div>

      {items.map((item, index) => (
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
      ))}
    </div>
  );
};

export default Navbar;
