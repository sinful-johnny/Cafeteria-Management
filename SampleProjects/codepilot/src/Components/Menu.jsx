import React from 'react';

const Menu = () => {
  const handleDragStart = (event, data) => {
    event.dataTransfer.setData('text/plain', JSON.stringify(data));
  };

  return (
    <div>
      <div
        draggable
        onDragStart={(event) => handleDragStart(event, { type: 'rectangle', color: 'blue' })}
        style={{
          width: '100px',
          height: '100px',
          backgroundColor: 'blue',
          margin: '10px',
          cursor: 'grab',
        }}
      >
        Drag me
      </div>
      <div style={{ width: '200px', height: '200px', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
        <div style={{ width: '100px', height: '100px', backgroundColor: 'red', borderRadius: '50%' }}
          draggable
          onDragStart={(event) => handleDragStart(event, { type: 'circle', color: 'red' })}
        ></div>
      </div>
    </div>
  );
};

export default Menu;
