import { Food } from "../Food";
import { ITable } from "../Interfaces/ITable";

export class RectangleTable implements ITable{
    x: number;
    y: number;
    isHovered: boolean;

    food: Food[];

    width: number;
    height: number;

    constructor(x: number, y: number, width: number, height: number){
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.food = [];
    }

    setIsHovered(value: boolean): void {
        this.isHovered = value;
        return;
    }
    
    isMouseInRange(x: number, y: number): boolean {
        return x > this.x && x < this.x + this.width && y > this.y && y < this.y + this.height
    }

    draw(context): void {
        context.fillStyle = "blue";
        context.fillRect(this.x, this.y, this.width, this.height);
        if(this.isHovered){
            context.strokeStyle = "rgba(173, 216, 230, 1)";
            context.lineWidth = 5;
            context.strokeRect(this.x, this.y, this.width, this.height);
        }
    }
}