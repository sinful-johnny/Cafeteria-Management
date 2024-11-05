import React, { useEffect, useState } from 'react';
import Canvas  from '../../Components/Canvas.tsx';

const MenuCanvas = ({items, setItems, selectedIndex, setSelectedIndex, isTableMenu, isSaved, setSaved, canvasSize}) => {
  return (
    <div style={{ display: 'flex', height: canvasSize[1], width: canvasSize[0] }}>
      <Canvas 
        items={items} 
        setItems={setItems} 
        selectedIndex={selectedIndex} 
        setSelectedIndex={setSelectedIndex} 
        isTableMenu={isTableMenu} 
        isSaved={isSaved} 
        setSaved={setSaved}
        canvasSize={canvasSize}
      />
    </div>
  );
};

export default MenuCanvas;
