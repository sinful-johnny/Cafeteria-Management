export class Food{
    public foodName: String;
    public amount_left: Number
    public price: Number;
    public foodTypeStatus: String;

    constructor(foodName: string, amount_left: number, price: number, foodTypeStatus: string) {
        this.foodName = foodName;
        this.amount_left = amount_left;
        this.price = price;
        this.foodTypeStatus = foodTypeStatus;
    }
}