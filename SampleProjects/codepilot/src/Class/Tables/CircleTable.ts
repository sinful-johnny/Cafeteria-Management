import { ITable } from "../Interfaces/ITable";

export class CircleTable implements ITable{
    x: number;
    y: number;
    isHovered: boolean;

    radius: number;

    constructor(x: number, y: number, radius: number){
        this.x = x;
        this.y = y;
        this.radius = radius;
    }
    setIsHovered(value: boolean): void {
        this.isHovered = value;
        return;
    }
    isMouseInRange(x: number, y: number): boolean {
        const distance = Math.sqrt((x - this.x) ** 2 + (y - this.y) ** 2);
        return distance < this.radius;
    }
    

    draw(context): void {
        context.beginPath();
        context.arc(this.x, this.y,this.radius, 0, Math.PI * 2,false);
        context.fillStyle = "red";
        context.fill();
        if(this.isHovered){
            context.beginPath();
            context.arc(this.x, this.y,this.radius, 0, Math.PI * 2,false);
            context.strokeStyle = "rgba(173, 216, 230, 1)";
            context.lineWidth = 5;
            context.stroke();
        }
    }
}