import React, { useState } from 'react';
import "./CollaspsibleListItem.css";

const CollapsibleListItem = ({ shape }) => {
  const [isDragging, setIsDragging] = useState(false);

  const handleMouseDown = () => {
    setIsDragging(true);
  };

  const handleMouseUp = () => {
    setIsDragging(false);
  };

  const handleDragStart = (event, shape) => {
    // Your drag start logic here
  };

  return (
    <div className={`collapsible-listItem ${isDragging ? 'grabbing' : 'grab'}`}
      draggable
      onDragStart={(event) => handleDragStart(event, shape)}
      onMouseDown={handleMouseDown}
      onMouseUp={handleMouseUp}
      onMouseLeave={handleMouseUp}
    >
      <img src={shape.img} alt={shape.name}/>
      <span>{shape.name}</span>
    </div>
  );
};

const CollapsibleList = ({ content }) => {
  return (
    <div>
      {content.map(shape => (
        <CollapsibleListItem key={shape.name} shape={shape} />
      ))}
    </div>
  );
};

export default CollapsibleList;
