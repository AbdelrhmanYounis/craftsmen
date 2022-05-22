import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { Observable } from 'rxjs';
import { Pagination } from 'src/app/_models/pagination';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { Country } from 'src/app/_models/country';
import { Governorate } from 'src/app/_models/governorate';
import { City } from 'src/app/_models/city';
import { Craft } from 'src/app/_models/craft';
import { CountryService } from 'src/app/_services/country.service';
import { CraftService } from 'src/app/_services/craft.service';
import { GovernorateService } from 'src/app/_services/governorate.service';
import { CityService } from 'src/app/_services/city.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members: Member[];
  pagination: Pagination;
  userParams: UserParams;
  user: User;
  Countries:Country[]=[];
  Governorates:Governorate[]=[];
  Cities:City[]=[];
  Crafts:Craft[]=[];

  constructor(private memberService: MembersService,private countryService:CountryService,
    private governorateService:GovernorateService,private cityService:CityService,
    private craftService:CraftService) {
    this.userParams = this.memberService.getUserParams();
  }

  ngOnInit(): void {
    this.loadMembers();    
   this.getCrafts();
   this.getCountries();
  }

  loadMembers() {
    this.memberService.setUserParams(this.userParams);
    this.memberService.getMembers(this.userParams).subscribe(response => {
      console.log(response);
      this.members = response.result;
      this.pagination = response.pagination;
    })
  }

  resetFilters() {
    this.userParams = this.memberService.resetUserParams();
    this.loadMembers();
    this.getCrafts();
   this.getCountries();
  }

  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.memberService.setUserParams(this.userParams);
    this.loadMembers();
  }
  getCrafts(){
    this.craftService.GetAll().subscribe(
      (data:Craft[])=>{
        this.Crafts=data;
      })
  }
  getCountries(){ 
    this.countryService.GetAll().subscribe(
      (data:Country[])=>{
        this.Countries=data;
      })
  }
  
  loadGovernorates(){
    this.Cities=[];    
    this.loadMembers(); 
    this.governorateService.GetByParentId(this.userParams.countryId).subscribe(
      (data:Governorate[])=>{
        this.Governorates=data;
        
      }
    );    
  }
  
  loadCities(){    
    this.loadMembers(); 
    this.cityService.GetByParentId(this.userParams.governorateId).subscribe(
      (data:City[])=>{
        this.Cities=data;
      })
      
  }
}
