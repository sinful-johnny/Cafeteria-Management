import { Food } from "../Food";

export interface ITable{
    x: number;
    y: number;
    isHovered: boolean;

    food: Food[];

    draw(context): void;
    isMouseInRange(x: number, y: number) : boolean;
    setIsHovered(value:boolean): void;
}