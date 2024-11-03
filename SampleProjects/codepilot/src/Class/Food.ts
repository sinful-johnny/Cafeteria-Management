export class Food{
    public foodId: Number;
    public foodName: String;
    public amount_left: Number
    public price: Number;
    public foodTypeStatus: String;
    public imageURL: String;

    constructor(foodId: Number, foodName: string, amount_left: number, price: number, foodTypeStatus: string, imageURL: String) {
        this.foodId = foodId;
        this.foodName = foodName;
        this.amount_left = amount_left;
        this.price = price;
        this.foodTypeStatus = foodTypeStatus;
        this.imageURL = imageURL;
    }
}