import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { IProduct } from '../IProduct.interface';
import { ProductsService } from 'src/app/services/products.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  addProductForm: any ;
  nextClicked: any ;
  product:  any;

  submitted = false;


  productView: IProduct = {
    Id: 0,
    Sku: '',
    Name: '',
    Description: '',
    Price: 0,
    IsAvailable: true,
    CategoryId: 0
};
  constructor(private fb: FormBuilder,
    private router: Router,
    private productsService: ProductsService,
    private alertify: AlertifyService) {

     }

  ngOnInit(): void {

  }

  onSubmit() {
    this.nextClicked = true;

        this.productsService.addProduct(this.product).subscribe(
            () => {
                this.alertify.success('Congrats, your property listed successfully on our website');
                console.log(this.addProductForm);
                this.router.navigate(['/product-list']);
            }
        );

}

}
