import { Injectable } from '@angular/core';
import { Craft } from '../_models/craft';
import { RepositoryService } from './repository.service';

@Injectable({
  providedIn: 'root'
})
export class CraftService {

  serviceName="Craft";

  constructor(private Repository:RepositoryService) { 
  }

  GetAll(){
   return this.Repository.GetAll(this.serviceName);
   }
   
   GetById(id:number){
   return this.Repository.GetById(this.serviceName,id);   
   }

   Create(model: Craft) {
  return this.Repository.Create(this.serviceName,model);
}

Update(model: Craft) {
  return this.Repository.Update(this.serviceName,model);
}

Delete(id: number) {
  return this.Repository.Delete(this.serviceName,id);
}
}
