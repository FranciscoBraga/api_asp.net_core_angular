import { User } from './../../_models/User';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators, ɵangular_packages_forms_forms_d } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  user: User;

  

  constructor(public fb: FormBuilder, private toastr: ToastrService, private authService : AuthService, private router: Router) { }

  ngOnInit() {
    this.validation();
  }

  validation(){
    this.registerForm = this.fb.group({
      fullName : ['', Validators.required],
      email : ['', [Validators.required, Validators.email]],
      userName : ['', Validators.required],
       passwords : this.fb.group({
        password : ['', [Validators.required, Validators.minLength(4)]],
        confirmPassword : ['', Validators.required]
      },{validator: this.compararSenhas})
    });
  }

  cadastrarUsuario(){
    if(this.registerForm.valid){
      this.user = Object.assign({password: this.registerForm.get('passwords.password').value}, this.registerForm.value);
      this.authService.register(this.user).subscribe(
        () =>{
            this.router.navigate(['/user/login']);
            this.toastr.success('Cadastro Realizado com sucesso!');
      },error=>{
          const erro = error.error;
          erro.array.forEach(element => {
            switch (element.code) {
              case 'DuplicateUserName':
                  this.toastr.error('Cadastro Duplicado');
                break;
              default:
                  this.toastr.error(`Erro ao Cadastrar ${element.code}`);
                break;
            }
          });
      });
    }
  }

  compararSenhas(fb: FormGroup) {
    const confirmSenhaCtrl = fb.get('confirmPassword');
    if(confirmSenhaCtrl.errors == null || 'mismatch' in confirmSenhaCtrl.errors){
      if(fb.get('password').value !==  fb.get('confirmPassword').value){
        confirmSenhaCtrl.setErrors({mismatch: true});
        } else{
          confirmSenhaCtrl.setErrors(null);
      }
    }
  }
}
