import React, { useRef, useEffect, useState } from 'react';

// Define the interface for the rectangle
interface Rectangle {
  id: number;
  x: number;
  y: number;
  width: number;
  height: number;
  isHovered: boolean;
}

const DraggableCanvas: React.FC = () => {
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const [rectangles, setRectangles] = useState<Rectangle[]>([
    { id: 1, x: 50, y: 50, width: 150, height: 100, isHovered: false },
    { id: 2, x: 250, y: 150, width: 150, height: 100, isHovered: false }
  ]);
  const [isDragging, setIsDragging] = useState<boolean>(false);
  const [dragIndex, setDragIndex] = useState<number | null>(null);
  const [dragOffset, setDragOffset] = useState<{ x: number; y: number }>({ x: 0, y: 0 });

  useEffect(() => {
    const canvas = canvasRef.current;
    if (canvas) {
      const context = canvas.getContext('2d');
      if (context) {
        const draw = () => {
          context.clearRect(0, 0, canvas.width, canvas.height);
          rectangles.forEach(rect => {
            context.fillStyle = 'green';
            context.fillRect(rect.x, rect.y, rect.width, rect.height);
            if (rect.isHovered) {
              context.strokeStyle = 'yellow';
              context.lineWidth = 5;
              context.strokeRect(rect.x, rect.y, rect.width, rect.height);
            }
          });
        };

        draw();

        const handleMouseDown = (e: MouseEvent) => {
          const rect = canvas.getBoundingClientRect();
          const x = e.clientX - rect.left;
          const y = e.clientY - rect.top;
          const index = rectangles.findIndex(r => x > r.x && x < r.x + r.width && y > r.y && y < r.y + r.height);
          if (index !== -1) {
            const rectangle = rectangles[index];
            setIsDragging(true);
            setDragIndex(index);
            setDragOffset({ x: x - rectangle.x, y: y - rectangle.y });
          }
          setRectangles(rectangles.map((r, i) => {
            const isHovered = x > r.x && x < r.x + r.width && y > r.y && y < r.y + r.height;
            return { ...r, isHovered };
          }));
        };

        const handleMouseMove = (e: MouseEvent) => {
          const rect = canvas.getBoundingClientRect();
          const x = e.clientX - rect.left;
          const y = e.clientY - rect.top;
          let newRectangles = rectangles;
          if (isDragging && dragIndex !== null) {
            newRectangles = rectangles.map((r, i) => {
              return i === dragIndex ? { ...r, x: x - dragOffset.x, y: y - dragOffset.y } : r;
            });
          }
          setRectangles(newRectangles.map((r, i) => {
            const isHovered = x > r.x && x < r.x + r.width && y > r.y && y < r.y + r.height;
            return { ...r, isHovered };
          }));
          draw();
        };

        const handleMouseUp = () => {
          setIsDragging(false);
          setDragIndex(null);
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
      }
    }
  }, [rectangles, isDragging, dragIndex, dragOffset]);

  return <canvas ref={canvasRef} width={600} height={400} style={{ border: '1px solid black' }} />;
};

export default DraggableCanvas;
