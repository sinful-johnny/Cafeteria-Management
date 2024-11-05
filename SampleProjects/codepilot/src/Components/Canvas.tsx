import React, { useRef, useEffect, useState, DragEvent } from 'react';
import {ITable} from "../Class/Interfaces/ITable";
import { TableFactory } from '../Class/Factories/TableFactory';
import { checkCollision, isCollidingWithBorderY, isCollidingWithBorderX } from './CollisionDetection';
import { RectangleTable } from '../Class/Tables/RectangleTable';
import { CircleTable } from '../Class/Tables/CircleTable';
import { Food } from '../Class/Food';

interface CanvasProps{
  items: ITable[];
  setItems: (items: ITable[]) => void;
  selectedIndex: Number | null;
  setSelectedIndex: (index: number) => void;
  isTableMenu: boolean;
  isSaved: boolean;
  setSaved: (isSaved: boolean) => void;
  canvasSize: number[];
}

const Canvas: React.FC<CanvasProps> = ({items, setItems, selectedIndex, setSelectedIndex, isTableMenu, isSaved, setSaved, canvasSize}) => {
  const canvasRef = useRef<HTMLCanvasElement>(null);
  //const [items, setItems] = useState<ITable[]>([]);

  const [isDragging, setIsDragging] = useState<boolean>(false);
  const [dragIndex, setDragIndex] = useState<number | null>(null);
  const [dragOffset, setDragOffset] = useState<{ x: number; y: number }>({ x: 0, y: 0 });

  useEffect(() => {
    const canvas = canvasRef.current;
    if (canvas) {
      const context = canvas.getContext('2d');
      if (context) {
        
        const drawItems = () => {
          context.clearRect(0, 0, canvas.width, canvas.height);
          items.forEach(item => {
            item.draw(context);
            const text = `Table ${item.tableId}: ${item.tableStatus}`;
            context.font = "12px Arial";
            context.fillStyle = "black";
            const textWidth = context.measureText(text).width;
            let centerX, centerY;
            if (item instanceof RectangleTable) {
                // Center for rectangle
                centerX = item.x + (item.width / 2) - (textWidth / 2);
                centerY = item.y + (item.height / 2) + 6;
            } else if (item instanceof CircleTable) {
                // Center for circle
                centerX = item.x - (textWidth / 2); // Circle's center x, minus half the text width
                centerY = item.y + 4; // Circle's center y, with a slight vertical offset
            }
            context.fillText(text, centerX, centerY);
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
          
          //console.log("Index", index);
          setSelectedIndex(index);
          //console.log(selectedIndex)
          setItems(items.map((item,i) => {
             if(i === index) {
              item.isSelected = true;
             }else{
              item.isSelected = false;
             }
             return item;
          }))
        };

        const handleMouseMove = (e: MouseEvent) => {
          if (!isTableMenu) return;
          const rect = canvas.getBoundingClientRect();
          const x = e.clientX - rect.left;
          const y = e.clientY - rect.top;
          let newItems = items;
          if (isDragging && dragIndex !== null) {
            newItems = items.map((item, i) => {
              if(i === dragIndex){
                if(item.tableStatus === "occupied") return item;
                let newX = x - dragOffset.x;
                let newY = y - dragOffset.y;
                let tempItem;
                if (item instanceof RectangleTable) {
                  tempItem = TableFactory.createTable(items.length + 1,"rectangle", newX, newY);
                } else if (item instanceof CircleTable) {
                  tempItem = TableFactory.createTable(items.length + 1,"circle", newX, newY);
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
                item.tableStatus = "unlocked";
                setSaved(false);
              }
              return item;
            });
          }

          setItems(newItems.map((item, i) => {
            item.isHovered = item.isMouseInRange(x,y);
            return item;
          }));
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
            setItems([...items, TableFactory.createTable(items.length + 1,data.shapeType, x, y)]);
          } catch (error) {
            console.log(error);
          }
        }else if(data.objectType === "Food"){
          const index = items.length - 1 - items.slice().reverse().findIndex(item => item.isMouseInRange(x,y));
  
          if(index >= 0 && index < items.length){
            //Table detected
            //Add food to a locked table
            var newItems = items.map((item,i) => {
              if(i === index){
                if(item.foods === undefined){
                  item.foods = [];
                }
                if (item.tableStatus === "unlocked") return item;
                item.foods.push(new Food(data.ID_FOOD,data.name, 1, 1, "Available", data.img));
                item.tableStatus = "occupied";
              }
              return item;
            });

            setItems(newItems.map((item, i) => {
              return item;
            }));
          }
        }else{
          console.log("Not implemented!");
        }
      }catch(error){
        console.log("Error", error);
      }
    }
    setSaved(false);
  };

  const handleDragOver = (event: DragEvent<HTMLCanvasElement>) => {
    event.preventDefault();
  };

  return (
    <div>
    <canvas
      ref={canvasRef}
      width={canvasSize[0]}
      height={canvasSize[1]}
      onDrop={handleDrop}
      onDragOver={handleDragOver}
      style={{ border: '1px solid black', maxHeight: "400px", maxWidth: "600px" }}
    />
    </div>
  );
};

export default Canvas;
