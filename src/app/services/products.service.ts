import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators'
import { IProduct } from '../product/IProduct.interface';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../environments/environment';



@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  baseUrl = environment.baseUrl;
  constructor(private http:HttpClient) { }

  getAllProducts():  Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.baseUrl + '/Products/');
  }

  addProduct(product: IProduct) {
      return this.http.post(this.baseUrl + '/Products/CreateProduct', product);
}
}



