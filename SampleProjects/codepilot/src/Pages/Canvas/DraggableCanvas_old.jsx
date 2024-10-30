import React, { useRef, useEffect, useState } from 'react';

const DraggableCanvas = () => {
  const canvasRef = useRef(null);
  const [isDragging, setIsDragging] = useState(false);
  const [dragOffset, setDragOffset] = useState({ x: 0, y: 0 });
  const [position, setPosition] = useState({ x: 50, y: 50 });
  const [isHovered, setIsHovered] = useState(false);
  const [isClicked, setIsClicked] = useState(false);

  useEffect(() => {
    const canvas = canvasRef.current;
    const context = canvas.getContext('2d');

    const draw = () => {
      context.clearRect(0, 0, canvas.width, canvas.height);
      context.fillStyle = isHovered ? 'rgba(173, 216, 230, 1)' : 'green';
      context.fillRect(position.x, position.y, 50, 50);
      if (isHovered || isClicked) {
        context.strokeStyle = 'yellow';
        context.lineWidth = 5;
        context.strokeRect(position.x, position.y, 50, 50);
      }
    };

    draw();

    const handleMouseDown = (e) => {
      const rect = canvas.getBoundingClientRect();
      const x = e.clientX - rect.left;
      const y = e.clientY - rect.top;
      if (x > position.x && x < position.x + 50 && y > position.y && y < position.y + 50) {
        setIsDragging(true);
        setDragOffset({ x: x - position.x, y: y - position.y });
        setIsClicked(true);
      } else {
        setIsClicked(false);
      }
      draw();
    };

    const handleMouseMove = (e) => {
      const rect = canvas.getBoundingClientRect();
      const x = e.clientX - rect.left;
      const y = e.clientY - rect.top;
      if (isDragging) {
        setPosition({ x: x - dragOffset.x, y: y - dragOffset.y });
      }
      if (x > position.x && x < position.x + 50 && y > position.y && y < position.y + 50) {
        setIsHovered(true);
      } else {
        setIsHovered(false);
      }
      draw();
    };

    const handleMouseUp = () => {
      setIsDragging(false);
    };

    canvas.addEventListener('mousedown', handleMouseDown);
    canvas.addEventListener('mousemove', handleMouseMove);
    canvas.addEventListener('mouseup', handleMouseUp);
    canvas.addEventListener('mouseleave', handleMouseUp);

    return () => {
      canvas.removeEventListener('mousedown', handleMouseDown);
      canvas.removeEventListener('mousemove', handleMouseMove);
      canvas.removeEventListener('mouseup', handleMouseUp);
      canvas.removeEventListener('mouseleave', handleMouseUp);
    };
  }, [isDragging, dragOffset, position, isHovered, isClicked]);

  return <canvas ref={canvasRef} width={600} height={400} style={{ border: '1px solid black' }} />;
};

//export default DraggableCanvas