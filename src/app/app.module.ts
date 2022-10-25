import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import {Routes, RouterModule} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';


import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ProductCardComponent } from './product/product-card/product-card.component';
import { ProductListComponent } from './product/product-list/product-list.component';
import { ProductsService } from './services/products.service';
import { UserLoginComponent } from './user-login/user-login.component';
import { UserRegisterComponent } from './user-register/user-register.component';
import { AddProductComponent } from './product/add-product/add-product.component';

const appRoutes: Routes = [
  {path: '', component: ProductListComponent},
  {path: 'products-list', component: ProductListComponent},
   {path: 'add-product', component: AddProductComponent},
  // {path: 'product-detail/:id',
  //     component: ProductDetailComponent},
   {path: 'user/login', component: UserLoginComponent},
   {path: 'user/register', component: UserRegisterComponent},
  // {path: '**', component: ProductListComponent}
];


@NgModule({
  declarations: [
    AppComponent,
    ProductCardComponent,
    ProductListComponent,
    AddProductComponent,
    NavBarComponent,
    UserLoginComponent,
    UserRegisterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),
  ],
  providers: [
    ProductsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
