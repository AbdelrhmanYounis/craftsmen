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
    gender: string;
    introduction: string;
    lookingFor: string;
    interests: string;
    cityId: number;
    city:City;
    craft:Craft;
    CraftId:number;
    photos: Photo[];
  }
  
