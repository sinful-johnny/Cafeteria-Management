import React, { useRef, useEffect, useState, DragEvent } from 'react';
import {ITable} from "../Class/Interfaces/ITable";
import { TableFactory } from '../Class/Factories/TableFactory';
import { checkCollision, isCollidingWithBorderY, isCollidingWithBorderX } from './CollisionDetection';
import { RectangleTable } from '../Class/Tables/RectangleTable';
import { CircleTable } from '../Class/Tables/CircleTable';
import { Food } from '../Class/Food';

const Canvas: React.FC = () => {
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const [items, setItems] = useState<ITable[]>([]);

  const [isDragging, setIsDragging] = useState<boolean>(false);
  const [dragIndex, setDragIndex] = useState<number | null>(null);
  const [dragOffset, setDragOffset] = useState<{ x: number; y: number }>({ x: 0, y: 0 });
  const [CollidingWithObject, setCollideObject] = useState<boolean>(false);

  useEffect(() => {
    const canvas = canvasRef.current;
    if (canvas) {
      const context = canvas.getContext('2d');
      if (context) {
        
        const drawItems = () => {
          context.clearRect(0, 0, canvas.width, canvas.height);
          items.forEach(item => {
            item.draw(context);
          });
        };
        drawItems();

        const handleMouseDown = (e: MouseEvent) => {
          const rect = canvas.getBoundingClientRect();
          const x = e.clientX - rect.left;
          const y = e.clientY - rect.top;
          const index = items.length - 1 - items.slice().reverse().findIndex(item => item.isMouseInRange(x,y));
          if (index >= 0 && index < items.length) {
            const item = items[index];
            setIsDragging(true);
            setDragIndex(index);
            setDragOffset({ x: x - item.x, y: y - item.y });
          }
          setItems(prevItems => prevItems.map((item, i) => {
            item.isHovered = item.isMouseInRange(x,y);
            return item;
          }));
        };

        const handleMouseMove = (e: MouseEvent) => {
          const rect = canvas.getBoundingClientRect();
          const x = e.clientX - rect.left;
          const y = e.clientY - rect.top;
          let newItems = items;
          let hasCollision = false;
          if (isDragging && dragIndex !== null) {
            newItems = items.map((item, i) => {
              if(i === dragIndex){
                let newX = x - dragOffset.x;
                let newY = y - dragOffset.y;
                let tempItem;
                if (item instanceof RectangleTable) {
                  tempItem = TableFactory.createTable("rectangle", newX, newY);
                } else if (item instanceof CircleTable) {
                  tempItem = TableFactory.createTable("circle", newX, newY);
                }
                if (isCollidingWithBorderX(tempItem, canvas.width)) {
                  null;
                } else {
                  item.x = newX;
                }
                if (isCollidingWithBorderY(tempItem, canvas.height)) {
                  null;
                } else {
                  item.y = newY;
                }
              }
              return item;
            });
            // Check for collisions and log them to the console
            for (let i = 0; i < newItems.length; i++) {
              for (let j = i + 1; j < newItems.length; j++) {
                if (checkCollision(newItems[i], newItems[j])) {
                  hasCollision = true;
                  console.log(`Collision detected between item ${i} and item ${j}`);
                  break;
                }
              }
              if (hasCollision) break;
            }
          }
          //setCollideBorder
          setCollideObject(hasCollision)
          setItems(prevItems => newItems.map((item, i) => {
            item.isHovered = item.isMouseInRange(x,y);
            return item;
          }));
          drawItems();
        };

        const handleMouseUp = () => {
          setIsDragging(false);
          setDragIndex(null);
        };

        canvas.addEventListener('mousedown', handleMouseDown);
        canvas.addEventListener('mousemove', handleMouseMove);
        canvas.addEventListener('dragover', handleMouseMove);
        canvas.addEventListener('mouseup', handleMouseUp);
        canvas.addEventListener('mouseleave', handleMouseUp);

        return () => {
          canvas.removeEventListener('mousedown', handleMouseDown);
          canvas.removeEventListener('mousemove', handleMouseMove);
          canvas.removeEventListener('dragover', handleMouseMove);
          canvas.removeEventListener('mouseup', handleMouseUp);
          canvas.removeEventListener('mouseleave', handleMouseUp);
        };
      }
    }
  }, [items, isDragging, dragIndex, dragOffset]);

  const handleDrop = (event: DragEvent<HTMLCanvasElement>) => {
    event.preventDefault();
    const data = JSON.parse(event.dataTransfer.getData('text/plain'));
    const rect = canvasRef.current?.getBoundingClientRect();
    if (rect) {
      const x = event.clientX - rect.left;
      const y = event.clientY - rect.top;

      try{
        if(data.objectType === "Table"){
          try {
            setItems([...items, TableFactory.createTable(data.shapeType, x, y)]);
          } catch (error) {
            console.log(error);
          }
        }else if(data.objectType === "Food"){
          const index = items.length - 1 - items.slice().reverse().findIndex(item => item.isMouseInRange(x,y));
  
          if(index >= 0 && index < items.length){
            //Table detected
            //Add food to a table
            var newItems = items.map((item,i) => {
              if(i === index){
                if(item.food === undefined){
                  item.food = [];
                }
                item.food.push(new Food(data.name, 1, 1, "Available"));
              }
              return item;
            });

            setItems(prevItems => newItems.map((item, i) => {
              return item;
            }));
            //console.log(items);
          }
        }else{
          console.log("Not implemented!");
        }
      }catch(error){
        console.log("Error", error);
      }
    }
  };

  const handleDragOver = (event: DragEvent<HTMLCanvasElement>) => {
    event.preventDefault();
  };

  return (
    <canvas
      ref={canvasRef}
      width={600}
      height={400}
      onDrop={handleDrop}
      onDragOver={handleDragOver}
      style={{ border: '1px solid black', maxHeight: "400px", maxWidth: "600px" }}
    />
  );
};

export default Canvas;
