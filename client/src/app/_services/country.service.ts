import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Country } from '../_models/country';
import { RepositoryService } from './repository.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  serviceName="Country";
  

  constructor(private Repository:RepositoryService) { 
  }

  GetAll(){
   return this.Repository.GetAll(this.serviceName);
   }

   GetById(id:number){
   return this.Repository.GetById(this.serviceName,id);   
   }

   Create(model: Country) {
  return this.Repository.Create(this.serviceName,model);
}

Update(model: Country) {
  return this.Repository.Update(this.serviceName,model);
}

Delete(id: number) {
  return this.Repository.Delete(this.serviceName,id);
}
}
