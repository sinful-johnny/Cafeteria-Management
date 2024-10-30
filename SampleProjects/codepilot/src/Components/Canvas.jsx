import React, { useRef, useEffect, useState } from 'react';

const Canvas = () => {
  const canvasRef = useRef(null);
  const [items, setItems] = useState([]);

  useEffect(() => {
    const canvas = canvasRef.current;
    const context = canvas.getContext('2d');

    const drawItems = () => {
      context.clearRect(0, 0, canvas.width, canvas.height);
      items.forEach(item => {
        if(item.shape === "rectangle"){
          context.fillStyle = item.color;
          context.fillRect(item.x, item.y, item.width, item.height);
        }else if(item.shape === "circle"){
          context.beginPath();
          context.arc(item.x, item.y,50, 0, Math.PI * 2,false);
          context.fillStyle = item.color;
          context.fill();
        }

      });
    };

    drawItems();
  }, [items]);

  const handleDrop = (event) => {
    event.preventDefault();
    const data = JSON.parse(event.dataTransfer.getData('text/plain'));
    const rect = canvasRef.current.getBoundingClientRect();
    const x = event.clientX - rect.left;
    const y = event.clientY - rect.top;

    setItems([...items, { x, y, width: 100, height: 50, color: data.color, shape: data.type }]);
  };

  const handleDragOver = (event) => {
    event.preventDefault();
  };

  return (
    <canvas
      ref={canvasRef}
      width={600}
      height={400}
      onDrop={handleDrop}
      onDragOver={handleDragOver}
      style={{ border: '1px solid black' }}
    />
  );
};

export default Canvas;
