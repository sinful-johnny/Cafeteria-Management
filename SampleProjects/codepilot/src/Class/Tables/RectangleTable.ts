import { Food } from "../Food";
import { FoodOnTable } from "../FoodOnTable";
import { ITable } from "../Interfaces/ITable";

export class RectangleTable implements ITable{
    x: number;
    y: number;
    isHovered: Boolean;
    tableId: number;
    isSelected: Boolean;
    color: String;
    tableStatus: String;
    shapeId: String;

    foods: FoodOnTable[];

    width: number;
    height: number;

    constructor(tableId: number, shapeId: String, x: number, y: number, width: number, height: number, tableStatus: String){
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.foods = [];
        this.tableStatus = tableStatus;
        this.tableId = tableId;
        this.shapeId = shapeId;
    }
    
    setIsHovered(value: boolean): void {
        this.isHovered = value;
        return;
    }
    
    isMouseInRange(x: number, y: number): boolean {
        return x > this.x && x < this.x + this.width && y > this.y && y < this.y + this.height
    }

    draw(context): void {
        if(this.tableStatus === "unlocked"){
            context.fillStyle = "#D9D9D9";
        }else if(this.tableStatus === "locked"){
            context.fillStyle = "#66DD56";
        }else if(this.tableStatus === "occupied"){
            context.fillStyle = "rgba(209, 150, 133, 0.69)";
        }else{
            throw Error("Not recognized behavior!");
        }

        context.fillRect(this.x, this.y, this.width, this.height);
        if(this.isSelected){
            context.strokeStyle = "rgba(123, 166, 180, 1)";
            context.lineWidth = 5;
            context.strokeRect(this.x, this.y, this.width, this.height);
        }
        else if(this.isHovered){
            context.strokeStyle = "rgba(173, 216, 230, 1)";
            context.lineWidth = 5;
            context.strokeRect(this.x, this.y, this.width, this.height);
        }
    }
}