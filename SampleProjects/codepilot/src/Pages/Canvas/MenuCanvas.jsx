import React, { useEffect, useState } from 'react';
import Canvas  from '../../Components/Canvas.tsx';

const MenuCanvas = ({items, setItems, selectedIndex, setSelectedIndex}) => {
  return (
    <div style={{ display: 'flex' }}>
      <Canvas items={items} setItems={setItems} selectedIndex={selectedIndex} setSelectedIndex={setSelectedIndex}/>
    </div>
  );
};

export default MenuCanvas;
