import { City } from './city';
import { Craft } from './craft';
import { Photo } from './photo';

export interface Member {
    id: number;
    username: string;
    photoUrl: string;
    age: number;
    knownAs: string;
    created: Date;
    lastActive: Date;
    phoneNumber:string;
    email:string;
    facebookUrl:string;
    workExperience:string;
    gender: string;
    address:string;
    cityId: number;
    city:City;
    craft:Craft;
    craftId:number;
    photos: Photo[];
    isLiked:boolean;
  }
  
