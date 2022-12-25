import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDetailsComponent } from "./product-details/product-details.component";
import { ProductItemComponent } from './product-item/product-item.component';
import { ShopComponent } from './shop.component';



@NgModule({
  declarations: [
    ProductDetailsComponent,
    ProductItemComponent,
    ShopComponent
  ],
  exports: [
    ShopComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ShopModule { }
