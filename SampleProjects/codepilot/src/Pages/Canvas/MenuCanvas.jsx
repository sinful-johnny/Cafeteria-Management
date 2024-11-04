import React, { useEffect, useState } from 'react';
import Canvas  from '../../Components/Canvas.tsx';

const MenuCanvas = ({items, setItems, selectedIndex, setSelectedIndex, isTableMenu}) => {
  return (
    <div style={{ display: 'flex' }}>
      <Canvas items={items} setItems={setItems} selectedIndex={selectedIndex} setSelectedIndex={setSelectedIndex} isTableMenu={isTableMenu}/>
    </div>
  );
};

export default MenuCanvas;
