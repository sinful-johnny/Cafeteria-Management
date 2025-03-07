import React, { useRef, useEffect, useState, DragEvent } from 'react';
import {ITable} from "../Class/Interfaces/ITable";
import { TableFactory } from '../Class/Factories/TableFactory';
import { checkCollision, isCollidingWithBorderY, isCollidingWithBorderX } from './CollisionDetection';
import { RectangleTable } from '../Class/Tables/RectangleTable';
import { CircleTable } from '../Class/Tables/CircleTable';
import { Food } from '../Class/Food';
import { FoodOnTable } from '../Class/FoodOnTable';

interface CanvasProps{
  items: ITable[];
  setItems: (items: ITable[]) => void;
  selectedIndex: Number | null;
  setSelectedIndex: (index: number) => void;
  isTableMenu: boolean;
  isSaved: boolean;
  setSaved: (isSaved: boolean) => void;
  canvasSize: number[];
  drawItems: (canvas, context) => void;
}

const Canvas: React.FC<CanvasProps> = ({items, setItems, selectedIndex, setSelectedIndex, isTableMenu, isSaved, setSaved, canvasSize, drawItems}) => {
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
        
        //const drawItems = () => {
        //  context.clearRect(0, 0, canvas.width, canvas.height);
        //  items.forEach(item => {
        //    item.draw(context);
        //    const text = `Table ${item.tableId}: ${item.tableStatus}`;
        //    context.font = "12px Arial";
        //    context.fillStyle = "black";
        //    const textWidth = context.measureText(text).width;
        //    let centerX, centerY;
        //    if (item instanceof RectangleTable) {
        //        // Center for rectangle
        //        centerX = item.x + (item.width / 2) - (textWidth / 2);
        //        centerY = item.y + (item.height / 2) + 6;
        //    } else if (item instanceof CircleTable) {
        //        // Center for circle
        //        centerX = item.x - (textWidth / 2); // Circle's center x, minus half the text width
        //        centerY = item.y + 4; // Circle's center y, with a slight vertical offset
        //    }
        //    context.fillText(text, centerX, centerY);
        //  });
        //};

        drawItems(canvas,context);

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
          drawItems(canvas,context);
        };

        const handleMouseMove = (e: MouseEvent) => {
          let newItems = items;
          const rect = canvas.getBoundingClientRect();
          const x = e.clientX - rect.left;
          const y = e.clientY - rect.top;
          if (isTableMenu){
            if (isDragging && dragIndex !== null) {
              newItems = items.map((item, i) => {
                if(i === dragIndex){
                  if(item.tableStatus === "occupied") return item;
                  let newX = x - dragOffset.x;
                  let newY = y - dragOffset.y;
                  let tempItem;
                  if (item instanceof RectangleTable) {
                    tempItem = TableFactory.createTable(items.length + 1, item.shapeId,"rectangle", newX, newY, item.width, item.height, 0);
                  } else if (item instanceof CircleTable) {
                    tempItem = TableFactory.createTable(items.length + 1, item.shapeId,"circle", newX, newY, 0, 0, item.radius);
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
          }
        
          setItems(newItems.map((item, i) => {
            item.isHovered = item.isMouseInRange(x,y);
            return item;
          }));

          drawItems(canvas,context);
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

    const generateTableId = (items) => {
      if(items.length === 0) {
        return 1;
      }
      const listId = items.map(item => {
        return Number(item.tableId);
      })
      //console.log(listId)
      const res =  Math.max(...listId);

      return res + 1;
    }

    if (rect) {
      const x = event.clientX - rect.left;
      const y = event.clientY - rect.top;

      try{
        if(data.objectType === "Table"){
          try {
            setItems([...items, TableFactory.createTable(generateTableId(items), data.shapeId,data.shapeType, x, y,data.width ? data.width : 0,data.height ? data.height : 0, data.radius ? data.radius : 0 )]);
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

                const foodIndex = item.foods.findIndex(x => x.food.foodId === data.ID_FOOD);
                if(foodIndex !== -1){
                  item.foods[foodIndex].amount += 1;
                }else{
                  item.foods.push(new FoodOnTable(new Food(data.ID_FOOD,data.name, 1, 1, "Available", data.img), 1));
                }
                
                item.tableStatus = "occupied";
                //console.log(item);
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
      width={`${canvasSize[0]}px`}
      height={`${canvasSize[1]}px`}
      onDrop={handleDrop}
      onDragOver={handleDragOver}
      style={{ border: '1px solid black', maxHeight: `${canvasSize[1]}px`, maxWidth: `${canvasSize[0]}px` }}
    />
    </div>
  );
};

export default Canvas;
