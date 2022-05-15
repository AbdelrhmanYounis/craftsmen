import { Injectable } from '@angular/core';
import { City } from '../_models/city';
import { RepositoryService } from './repository.service';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  serviceName="City";

  constructor(private Repository:RepositoryService) { 
  }

  GetAll(){
   return this.Repository.GetAll(this.serviceName);
   }
   GetByParentId(governorateId:number){
    return this.Repository.GetByParentId(this.serviceName,governorateId);   
    }
   GetById(id:number){
   return this.Repository.GetById(this.serviceName,id);   
   }

   Create(model: City) {
  return this.Repository.Create(this.serviceName,model);
}

Update(model: City) {
  return this.Repository.Update(this.serviceName,model);
}

Delete(id: number) {
  return this.Repository.Delete(this.serviceName,id);
}
}
