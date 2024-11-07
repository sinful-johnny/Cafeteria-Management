import { Food } from "./Food";

export class FoodOnTable{
    public food: Food;
    public amount: number;

    constructor(food: Food, amount: number) {
        this.food = food;
        this.amount = amount; 
    }
}