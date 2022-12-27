import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDetailsComponent } from "./product-details/product-details.component";
import { ProductItemComponent } from './product-item/product-item.component';
import { ShopComponent } from './shop.component';
import {SharedModule} from "../shared/shared.module";



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
    CommonModule,
    SharedModule
  ]
})
export class ShopModule { }
