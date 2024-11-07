import { FoodOnTable } from "../FoodOnTable";

export interface ITable{
    x: number;
    y: number;
    isHovered: Boolean;
    tableId: number;
    isSelected: Boolean;
    color: String;
    //tableStatus: "locked", "unlocked", "occupied"
    tableStatus: String;

    foods: FoodOnTable[];

    draw(context): void;
    isMouseInRange(x: number, y: number) : boolean;
    setIsHovered(value:boolean): void;
}