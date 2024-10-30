import React from 'react';
import Menu from '../../Components/Menu';
import Canvas  from '../../Components/Canvas.tsx';

const MenuCanvas = () => {
  return (
    <div style={{ display: 'flex' }}>
      <Menu />
      <Canvas />
    </div>
  );
};

export default MenuCanvas;
