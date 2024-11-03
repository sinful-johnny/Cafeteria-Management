
import { Food } from "../Food";

export interface ITable{
    x: number;
    y: number;
    isHovered: Boolean;
    tableId: number;
    isSelected: Boolean;
    color: String;
    //table status: "locked", "unlocked", "occupied"
    tableStatus: String;

    foods: Food[];

    draw(context): void;
    isMouseInRange(x: number, y: number) : boolean;
    setIsHovered(value:boolean): void;
}