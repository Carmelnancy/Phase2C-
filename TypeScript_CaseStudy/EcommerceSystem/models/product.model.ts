import { Category } from "./category.enum";

export interface Product{
    id: number;
    name:string;
    price:number;
    category:Category;
    stock:number;
}