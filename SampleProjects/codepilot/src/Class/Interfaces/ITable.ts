export interface ITable{
    x: number;
    y: number;
    isHovered: boolean;

    draw(context): void;
    isMouseInRange(x: number, y: number) : boolean;
    setIsHovered(value:boolean): void;
}