import React from 'react'
import { CircleTable } from '../Class/Tables/CircleTable';
import { RectangleTable } from '../Class/Tables/RectangleTable';

//rectangle-rectangle Collision
export const isRectangleCollidingWithRectangle = (rect1, rect2) => {
    return (
      rect1.x < rect2.x + rect2.width &&
      rect1.x + rect1.width > rect2.x &&
      rect1.y < rect2.y + rect2.height &&
      rect1.y + rect1.height > rect2.y
    );
  };
  
  // Circle-Circle Collision
  export const isCircleCollidingWithCircle = (circle1, circle2) => {
    const dx = circle1.x - circle2.x;
    const dy = circle1.y - circle2.y;
    const distance = Math.sqrt(dx * dx + dy * dy);
    return distance < circle1.radius + circle2.radius;
  };
  
  // Circle-Rectangle Collision
  export const isCircleCollidingWithRectangle = (circle, rect) => {
    const distX = Math.abs(circle.x - (rect.x + rect.width / 2));
    const distY = Math.abs(circle.y - (rect.y + rect.height / 2));
  
    if (distX > rect.width / 2 + circle.radius) return false;
    if (distY > rect.height / 2 + circle.radius) return false;
  
    if (distX <= rect.width / 2) return true;
    if (distY <= rect.height / 2) return true;
  
    const dx = distX - rect.width / 2;
    const dy = distY - rect.height / 2;
    return dx * dx + dy * dy <= circle.radius * circle.radius;
  };
  
  // Generalized Collision Check
  export const checkCollision = (shape1, shape2) => {
    if (shape1 instanceof RectangleTable && shape2 instanceof RectangleTable) {
      return isRectangleCollidingWithRectangle(shape1, shape2);
    } else if (shape1 instanceof CircleTable && shape2 instanceof CircleTable) {
      return isCircleCollidingWithCircle(shape1, shape2);
    } else if (shape1 instanceof CircleTable && shape2 instanceof RectangleTable) {
      return isCircleCollidingWithRectangle(shape1, shape2);
    } else if (shape1 instanceof RectangleTable && shape2 instanceof CircleTable) {
      return isCircleCollidingWithRectangle(shape2, shape1);
    }
    return false;
  };