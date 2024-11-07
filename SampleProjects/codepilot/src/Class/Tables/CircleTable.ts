import { Food } from "../Food";
import { FoodOnTable } from "../FoodOnTable";
import { ITable } from "../Interfaces/ITable";

export class CircleTable implements ITable{
    x: number;
    y: number;
    isHovered: Boolean;
    tableId: number;
    isSelected: Boolean;
    color: String;
    tableStatus: String;

    foods: FoodOnTable[];

    radius: number;

    constructor(tableId: number,x: number, y: number, radius: number, tableStatus: String){
        this.x = x;
        this.y = y;
        this.radius = radius;
        this.foods = [];
        this.tableStatus = tableStatus;
        this.tableId = tableId;
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

        if(this.tableStatus === "unlocked"){
            context.fillStyle = "#D9D9D9";
        }else if(this.tableStatus === "locked"){
            context.fillStyle = "#66DD56";
        }else if(this.tableStatus === "occupied"){
            context.fillStyle = "rgba(209, 150, 133, 0.69)";
        }else{
            throw Error("Not recognized behavior!");
        }

        context.fill();
        if(this.isSelected){
            context.beginPath();
            context.arc(this.x, this.y,this.radius, 0, Math.PI * 2,false);
            context.strokeStyle = "rgba(123, 166, 180, 1)";
            context.lineWidth = 5;
            context.stroke();
        }
        else if(this.isHovered){
            context.beginPath();
            context.arc(this.x, this.y,this.radius, 0, Math.PI * 2,false);
            context.strokeStyle = "rgba(173, 216, 230, 1)";
            context.lineWidth = 5;
            context.stroke();
        }
    }
}