import { Component, OnInit } from '@angular/core';
import 'assets/login-animation.js';
declare var $: any;
declare var Materialize: any;
import { LoginService } from "../../service/login-service.service";
import axios from 'axios';
import { Router } from '@angular/router';
/**
 * @ignore
 */
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})

export class LoginComponent implements OnInit {

  private email: string;
  private password: string;
  private rememberMe:boolean=false;
  private waitingResponse=false;

  cookieLaw=true;
  constructor(private loginService:LoginService, private router:Router) {
    let cookie = this.getCookie("login");
    if(cookie!=null){
      let login =JSON.parse(window.atob(cookie));
      this.email=login.username;
      this.password=login.password;
      this.login();
    }

  }

  ngOnInit() {}


  ngAfterViewInit() {
    (window as any).initialize();
  }

  
  /**
   * this function is called to log in
   */
  login(){
    this.waitingResponse=true;
    this.loginService.login(this.email,this.password).then((result)=>
    {
      this.waitingResponse=false;
      if(result==true){
        let redirectURL= this.loginService.surfUrl=="/login"? "/":this.loginService.surfUrl; 
        this.router.navigate([redirectURL]);
        if(this.rememberMe){
          let login:any = {username:this.email,password:this.password};
          login = JSON.stringify(login);
          login = window.btoa(login);
          this.setCookie("login",login,30);
        }
      }else{
        alert("Toegang verboden!");
      }
    }
    ).catch(()=>{this.waitingResponse=false});
  }

  /**
   * save cookie
   * @param name this is the key for the cookie
   * @param value this is the value you want to save
   * @param days this is the value for how long you want the cookie to be saved
   */
 setCookie(name,value,days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days*24*60*60*1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "")  + expires + "; path=/";
}
/**
 * this loads the saved value for a key
 * @param name the key to get the cookie
 */
getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1,c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
    }
    return null;
}

  /**
   * @ignore
   */
  cancelFormEvent(empForm: any, event: Event) {
    event.preventDefault();
    this.login();
    
  }
}

/*
SysAdmin    0,
Nurse       1,
User        2,
Module      3,
Guest       4
  */
