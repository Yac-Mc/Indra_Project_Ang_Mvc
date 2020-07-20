import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { User } from 'src/app/Models/User';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  userArray: User[] = [];
  registerForm: FormGroup;
  submitted = false;
  idUser = 0;
  result: number;
  flagResult = false;

  constructor(private formBuilder: FormBuilder, private userService: UsersService) { }

  ngOnInit(): void {
    this.buildForm();
    this.getUsers();
  }

  buildForm(){
    this.registerForm = this.formBuilder.group({
      UserName: ['', [Validators.required, Validators.pattern('^[A-Za-z0-9\\s]+$'), Validators.maxLength(100), 
                this._validateSpaces.bind(this), this._validateUser.bind(this)]],
      Limit: ['', [Validators.required, Validators.min(1), Validators.max(99999)]]
    });
  }

  _validateSpaces(c: AbstractControl): object{
    const inputtValue: string = c.value.toString();
    if (inputtValue.trim().length > 0){
      return null;
    }else{return { spaces: {valid: false } }; }
  }

  _validateUser(c: AbstractControl): object{
    const inputtValue: string = c.value.toString().toLowerCase();
    if (this.userArray.find(user => user.UserName.toLowerCase() === inputtValue)){
      this.idUser = this.userArray.find(user => user.UserName.toLowerCase() === inputtValue).Id;
      return null;
    }else{ return { name: {valid: false } }; }
  }

  getUsers = async () => {
    this.userService.getAllUsers().subscribe( (users: any) => {
      this.userArray = users;
    });
  }

  // Obtención de campos para facil control del formulario
  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.flagResult = false;
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }

    const userRegister: any = {
      IdUser: Number(this.idUser),
      Limit: Number(this.registerForm.controls[`Limit`].value)
    };

    this.userService.saveRegister(userRegister).subscribe(result => {
      this.result = result;
      this.flagResult = true;
    });
  }

}
