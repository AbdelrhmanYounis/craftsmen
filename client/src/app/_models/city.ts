import { Governorate } from "./governorate";

export interface City{
     id:number ;
     name:string;
     governorate:Governorate;
     governorateId:number;
}