import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  
  url = 'https://localhost:44333/api/user/';
  constructor(private http: HttpClient) { }

  getQuery( query: string){
    return this.http.get(`${this.url}${ query }`);
  }

  postQuery( url: string, data: any){
    return this.http.post<any>(url, data);
  }

  getAllUsers(){
    return this.getQuery(`GetAllUsers`);
  }

  getUsersFilter(filter){
    return this.getQuery(`GetUsersFilter/${filter.UserName}/${filter.ResponseMin}/${filter.ResponseMax}`);
  }

  saveRegister(register){
    return this.postQuery(`${this.url}SaveRegisterUser`, register);
  }

  deleteRegister(id){
    return this.getQuery(`DeleteRegister/${id}`);
  }
}
